using System;
using System.Windows.Forms;
using System.Xml;

namespace Fujitsu.Tools.KLPlanUpdate
{
    internal class Facet : TreeNode
    {
        private string mHandlingsfacetKode;
        private string mHandlingsfacetTekst;
        private DateTime mOprettet;
        private DateTime mUdgaaet;
        private bool mIsExpired = false;
        private int mRecno = 0;
        private int mScrapCode;
        private int mPreserveYears;
        private string mBevaringJaevnfoerArkivlovenTekst;

        private SaveStates mSaveState = SaveStates.Saved;

        internal Facet(XmlNode pFacet, NoarkClasscode pClasscode)
            : base()
        {
            mHandlingsfacetKode = pFacet.SelectSingleNode("HandlingsfacetNr").InnerText;
            mHandlingsfacetTekst = pFacet.SelectSingleNode("HandlingsfacetTitel").InnerText;
            mOprettet = DateTime.Parse(pFacet.SelectSingleNode("HandlingsfacetAdministrativInfo/OprettetDato").InnerText);
            if (pFacet.SelectSingleNode("HandlingsfacetAdministrativInfo/Historisk/UdgaaetDato") != null)
            {
                mUdgaaet = DateTime.Parse(pFacet.SelectSingleNode("HandlingsfacetAdministrativInfo/Historisk/UdgaaetDato").InnerText);
                mIsExpired = true;
            }

            mBevaringJaevnfoerArkivlovenTekst = pFacet.SelectSingleNode("BevaringJaevnfoerArkivloven").InnerText;
            if (mBevaringJaevnfoerArkivlovenTekst == "B")
                mScrapCode = 1;
            else if (mBevaringJaevnfoerArkivlovenTekst.Substring(0, 1) == "K")
            {
                mScrapCode = 2;
                if (mBevaringJaevnfoerArkivlovenTekst.Length > 1)
                {
                    mPreserveYears = int.Parse(mBevaringJaevnfoerArkivlovenTekst.Substring(1, mBevaringJaevnfoerArkivlovenTekst.Length - 1));
                }
            }
            else if (mBevaringJaevnfoerArkivlovenTekst == "G")
                mScrapCode = 3;
            else if (mBevaringJaevnfoerArkivlovenTekst == "U")
                mScrapCode = 4;
            else
                mScrapCode = -1;

            if (pClasscode != null)
            {
                mRecno = int.Parse(pClasscode.Recno);

                if (mHandlingsfacetTekst != pClasscode.Description || mIsExpired != pClasscode.IsExpired)
                {
                    mSaveState = SaveStates.Updated;
                }

                if (mIsExpired && !pClasscode.IsExpired)
                {
                    mSaveState = SaveStates.Deleted;
                }

                if (mScrapCode != -1)
                {
                    if ((pClasscode.ScrapCode != null) && (pClasscode.ScrapCode != ""))
                    {
                        if (mScrapCode != int.Parse(pClasscode.ScrapCode))
                        {
                            mSaveState = SaveStates.Updated;
                        }
                    }
                    else
                    {
                        mSaveState = SaveStates.Updated;
                    }
                }
            }
            else
            {
                mSaveState = SaveStates.New;
            }

            UpdateTextAndImage();
        }

        public string HandlingsfacetKode { get { return mHandlingsfacetKode; } }
        public string HandlingsfacetTekst { get { return mHandlingsfacetTekst; } }
        public DateTime Oprettet { get { return mOprettet; } }
        public int Recno { get { return mRecno; } set { mRecno = value; } }
        public bool IsExpired { get { return mIsExpired; } }
        public int ScrapCode { get { return mScrapCode; } set { mScrapCode = value; } }
        public int PreserveYears { get { return mPreserveYears; } set { mPreserveYears = value; } }
        public SaveStates SaveState { get { return mSaveState; } }

        public void Save(DB360 pDB360)
        {
            switch (mSaveState)
            {
                case SaveStates.New:
                    pDB360.CreateFacet(this);
                    break;
                case SaveStates.Updated:
                    pDB360.UpdateFacet(this);
                    break;
                case SaveStates.Deleted:
                    pDB360.UpdateFacet(this);
                    break;
            }

            mSaveState = SaveStates.Saved;
            UpdateTextAndImage();
        }

        private void UpdateTextAndImage()
        {
            if (mRecno > 0)
            {
                this.Text = mHandlingsfacetKode + " " + mHandlingsfacetTekst + " (" + mRecno + ")";
            }
            else
            {
                this.Text = mHandlingsfacetKode + " " + mHandlingsfacetTekst;
            }

            this.ImageIndex = SaveStateImage.GetImageIndex(mSaveState);
            this.SelectedImageIndex = this.ImageIndex;
        }
    }
}
