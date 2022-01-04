using System;
using System.Windows.Forms;
using System.Xml;

namespace Fujitsu.Tools.KLPlanUpdate
{
    public partial class FormMain : Form
    {
        private DB360 mDB360 = null;
        private XmlDocument mKLEEmne = new XmlDocument();
        private XmlDocument mFacet = new XmlDocument();
        private XmlDocument mStikord = new XmlDocument();
        private string mKLEmneplanFilename = "";
        private string mKLFacetterFilename = "";
        private string mKLStikordFilename = "";
        private string mudgivelsesDato = "";

        int SubjectNodeCount = 0;

        public FormMain()
        {
            InitializeComponent();
        }

        private void LoadAll()
        {
            treeViewKLPlan.Nodes.Clear();

            if (mKLEmneplanFilename != "")
                LoadKLPlan(mKLEmneplanFilename);

            if (mKLFacetterFilename != "")
                LoadFacetter(mKLFacetterFilename);

            if (mKLStikordFilename != "")
                LoadStikord(mKLStikordFilename);

            treeViewKLPlan.ExpandAll();
            treeViewKLPlan.TopNode = treeViewKLPlan.Nodes[0];
        }

        private void LoadKLPlan(string pFilename)
        {
            mKLEEmne.Load(pFilename);
            mudgivelsesDato = mKLEEmne.SelectSingleNode("KLE-Emneplan/UdgivelsesDato").InnerText;
            TreeNode klEmneNode = new TreeNode("KL Emneplan 2.0 - Udgivelsesdato: " + mudgivelsesDato, 4, 4);
            treeViewKLPlan.Nodes.Add(klEmneNode);

            XmlNodeList hovedGrupperKLE = mKLEEmne.SelectNodes("KLE-Emneplan/Hovedgruppe");
            NoarkSubarchiveList subArchives = mDB360.GetNoarkSubarchives();
            NoarkClasscodeList classcodes = mDB360.GetNoarkClasscodes();

            foreach (XmlNode n in hovedGrupperKLE)
            {
                string HovedgruppeNr = n.SelectSingleNode("HovedgruppeNr").InnerText;
                string HovedgruppeTitel = n.SelectSingleNode("HovedgruppeTitel").InnerText;

                NoarkSubarchive subArchive = subArchives.FindNoarkSubarchive(HovedgruppeNr);

                MainGroup mainGroup = new MainGroup(n, subArchive);

                XmlNodeList grupperKLE = n.SelectNodes("Gruppe");
                foreach (XmlNode ng in grupperKLE)
                {
                    string GruppeNr = ng.SelectSingleNode("GruppeNr").InnerText;

                    NoarkClasscode classcode = classcodes.FindNoarkClasscode(GruppeNr);

                    Group group = new Group(ng, classcode);

                    XmlNodeList underGrupperKLE = ng.SelectNodes("Emne");
                    foreach (XmlNode nsg in underGrupperKLE)
                    {
                        string EmneNr = nsg.SelectSingleNode("EmneNr").InnerText;

                        NoarkClasscode subClasscode = classcodes.FindNoarkClasscode(EmneNr);

                        SubGroup subGroup = new SubGroup(nsg, subClasscode);
                        if (subGroup.SaveState != SaveStates.Saved)
                        {
                            group.Nodes.Add(subGroup);
                        }
                    }
                    if (group.SaveState != SaveStates.Saved || group.Nodes.Count > 0)
                    {
                        mainGroup.Nodes.Add(group);
                    }
                }
                if (mainGroup.SaveState != SaveStates.Saved || mainGroup.Nodes.Count > 0)
                {
                    klEmneNode.Nodes.Add(mainGroup);
                }
            }

            if (klEmneNode.Nodes.Count == 0)
                klEmneNode.Text = klEmneNode.Text + " - Ingen ændringer";
        }

        private void LoadFacetter(string pFilename)
        {
            mFacet.Load(pFilename);
            string udgivelsesDato = mFacet.SelectSingleNode("KLE-Handlingsfacetter/UdgivelsesDato").InnerText;
            TreeNode facetNode = new TreeNode("Facetter - " + udgivelsesDato, 4, 4);
            treeViewKLPlan.Nodes.Add(facetNode);

            XmlNodeList facetKategorierKLE = mFacet.SelectNodes("KLE-Handlingsfacetter/HandlingsfacetKategori");
            NoarkClasscodeList classcodes = mDB360.GetNoarkClasscodes();

            foreach (XmlNode n in facetKategorierKLE)
            {
                string HandlingsfacetKategoriNr = n.SelectSingleNode("HandlingsfacetKategoriNr").InnerText;

                NoarkClasscode classcode = classcodes.FindNoarkClasscode(HandlingsfacetKategoriNr);

                FacetCategory facetCategory = new FacetCategory(n, classcode);

                XmlNodeList facetterKLE = mFacet.SelectNodes("KLE-Handlingsfacetter/HandlingsfacetKategori/Handlingsfacet[starts-with(HandlingsfacetNr, '" + HandlingsfacetKategoriNr + "')]");
                foreach (XmlNode nf in facetterKLE)
                {
                    string HandlingsfacetNr = nf.SelectSingleNode("HandlingsfacetNr").InnerText;
                    string HandlingsfacetTekst = nf.SelectSingleNode("HandlingsfacetTitel").InnerText;
                    XmlNodeList dubletter = mFacet.SelectNodes("KLE-Handlingsfacetter/Handlingsfacet[HandlingsfacetNr='" + HandlingsfacetNr + "']");

                    NoarkClasscode classcodeFacet = null;
                    if (dubletter.Count > 1)
                    {
                        classcodeFacet = classcodes.FindNoarkClasscode(HandlingsfacetNr, HandlingsfacetTekst);
                        if (classcodeFacet != null && classcodeFacet.Description != HandlingsfacetTekst)
                        {
                            NoarkClasscodeList classcodeDubletter = classcodes.FindNoarkClasscodes(HandlingsfacetNr);
                            for (int i = classcodeDubletter.Count - 1; i >= 0; i--)
                            {
                                bool thisIsAnotherNode = false;
                                foreach (XmlNode nd in dubletter)
                                {
                                    if (nd.SelectSingleNode("KLE-Handlingsfacetter/HandlingsfacetKategori/Handlingsfacet/HandlingsfacetTitel").InnerText == classcodeDubletter[i].Description)
                                    {
                                        thisIsAnotherNode = true;
                                    }
                                }

                                if (classcodeDubletter[i].IsExpired)
                                    thisIsAnotherNode = true;

                                if (thisIsAnotherNode)
                                    classcodeDubletter.RemoveAt(i);
                            }
                            if (classcodeDubletter.Count > 0)
                                classcodeFacet = classcodeDubletter[0];
                            else
                                classcodeFacet = null;
                        }
                    }
                    else
                    {
                        classcodeFacet = classcodes.FindNoarkClasscode(HandlingsfacetNr, HandlingsfacetTekst);
                    }

                    Facet facet = new Facet(nf, classcodeFacet);
                    if (facet.SaveState != SaveStates.Saved)
                    {
                        facetCategory.Nodes.Add(facet);
                    }
                }
                if (facetCategory.SaveState != SaveStates.Saved || facetCategory.Nodes.Count > 0)
                {
                    facetNode.Nodes.Add(facetCategory);
                }
            }

            if (facetNode.Nodes.Count == 0)
                facetNode.Text = facetNode.Text + " - Ingen ændringer";
        }

        private void LoadStikord(string pFilename)
        {
            mStikord.Load(pFilename);
            string udgivelsesDato = mStikord.SelectSingleNode("KLE-Stikord/UdgivelsesDato").InnerText;
            XmlNodeList subjectNodesHovedgruppe = mStikord.SelectNodes("KLE-Stikord/KLValideredeStikord/StikordTilEmneKategori/StikordTilHovedgruppe");
            XmlNodeList subjectNodesGruppe = mStikord.SelectNodes("KLE-Stikord/KLValideredeStikord/StikordTilEmneKategori/StikordTilGruppe");
            XmlNodeList subjectNodesEmne = mStikord.SelectNodes("KLE-Stikord/KLValideredeStikord/StikordTilEmneKategori/StikordTilEmne");
            XmlNodeList subjectNodesHandlingsfacet = mStikord.SelectNodes("KLE-Stikord/KLValideredeStikord/StikordTilHandlingsfacet");
            XmlNodeList subjectNodesEmneOgHandlingsfacet = mStikord.SelectNodes("KLE-Stikord/KLValideredeStikord/StikordTilEmneOgHandlingsfacet");
            SubjectNodeCount = subjectNodesHovedgruppe.Count + subjectNodesGruppe.Count + subjectNodesEmne.Count + subjectNodesHandlingsfacet.Count + subjectNodesEmneOgHandlingsfacet.Count;
            TreeNode stikordNode = new TreeNode("Stikord - " + udgivelsesDato + " (" + SubjectNodeCount.ToString() + " stikord)", 4, 4);
            treeViewKLPlan.Nodes.Add(stikordNode);
        }

        private void SaveStikord()
        {
            try
            {
                if (SubjectNodeCount > 0)
                {
                    mDB360.RemoveAllStikord();

                    mDB360.Log.LogStartCreateSubjects(SubjectNodeCount);

                    SaveStikord(mStikord.SelectNodes("KLE-Stikord/KLValideredeStikord/StikordTilEmneKategori/StikordTilHovedgruppe"));
                    SaveStikord(mStikord.SelectNodes("KLE-Stikord/KLValideredeStikord/StikordTilEmneKategori/StikordTilGruppe"));
                    SaveStikord(mStikord.SelectNodes("KLE-Stikord/KLValideredeStikord/StikordTilEmneKategori/StikordTilEmne"));
                    SaveStikord(mStikord.SelectNodes("KLE-Stikord/KLValideredeStikord/StikordTilHandlingsfacet"));
                    SaveStikord(mStikord.SelectNodes("KLE-Stikord/KLValideredeStikord/StikordTilEmneOgHandlingsfacet"));

                    mDB360.UpdateStikordReferences();
                    mDB360.Log.LogFinishCreateSubjects();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void SaveStikord(XmlNodeList subjectNodes)
        {
            try
            {

                if (subjectNodes.Count > 0)
                {

                    foreach (XmlNode n in subjectNodes)
                    {
                        Subject subject = new Subject(n);
                        if (!subject.IsExpired)
                        {
                            mDB360.CreateStikord(subject);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                mDB360.Log.LogException(ex);
                throw ex;
            }
        }

        private void SaveNode(TreeNode pNode)
        {
            MainGroup mainGroup = pNode as MainGroup;
            if (mainGroup != null)
            {
                mainGroup.Save(mDB360);
            }

            Group group = pNode as Group;
            if (group != null)
            {
                group.Save(mDB360);
            }

            SubGroup subGroup = pNode as SubGroup;
            if (subGroup != null)
            {
                subGroup.Save(mDB360);
            }

            FacetCategory facetCategory = pNode as FacetCategory;
            if (facetCategory != null)
            {
                facetCategory.Save(mDB360);
            }

            Facet facet = pNode as Facet;
            if (facet != null)
            {
                facet.Save(mDB360);
            }
        }

        private void SaveAllNodes(TreeNodeCollection pNodes)
        {
            foreach (TreeNode n in pNodes)
            {
                SaveNode(n);
                SaveAllNodes(n.Nodes);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDoUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                mDB360.Log.LogStartUpdate();
                SaveAllNodes(treeViewKLPlan.Nodes);
                SaveStikord();
                mDB360.Log.LogFinishUpdate();
                this.Cursor = Cursors.Default;

                if (mudgivelsesDato != "")
                {
                    mDB360.UpdateKLverOnTheServer(mudgivelsesDato);
                    KLversion.Text = mDB360.GetInstalledKLverOnTheServer();
                }

                MessageBox.Show("Opdatering fuldført.", "eDoc", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void buttonSelected_Click(object sender, EventArgs e)
        {
            try
            {
                SaveNode(treeViewKLPlan.SelectedNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                FormConfig frmCfg = new FormConfig();
                DialogResult dr = frmCfg.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    mKLEmneplanFilename = frmCfg.KLEmneplanFilename;
                    mKLFacetterFilename = frmCfg.KLFacetterFilename;
                    mKLStikordFilename = frmCfg.KLStikordFilename;

                    mDB360 = new DB360(frmCfg.ConnectString);
                    KLversion.Text = mDB360.GetInstalledKLverOnTheServer();
                    LoadAll();
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "eDoc");
                this.Close();
            }
        }
    }
}
