using System;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace Fujitsu.Tools.KLPlanUpdate
{
    public partial class FormConfig : Form
    {
        private bool mSchemaValid = false;

        private string mKLEmneplanSchema = @"\Skemaer\KLE-Emneplan-version-2-0.xsd";
        private string mKLFacetterSchema = @"\Skemaer\KLE-Handlingsfacetter-version-2-0.xsd";
        private string mKLStikordSchema = @"\Skemaer\KLE-Stikord-version-2-0.xsd";

        private string mKLEmneplanFilename = "";
        private string mKLFacetterFilename = "";
        private string mKLStikordFilename = "";

        public FormConfig()
        {
            InitializeComponent();

            // -- Start TEST --
            //mKLEmneplanFilename = @"D:\KLE-KKE\KKE_XML2.0\KLE-Emneplan_2.0_kke.xml";
            //mKLFacetterFilename = @"D:\KLE-KKE\KKE_XML2.0\KLE-Handlingsfacetter_2.0_kke.xml";
            //mKLStikordFilename = @"D:\KLE-KKE\KKE_XML2.0\KLE-Stikord_2.0_kke.xml";

            textBoxKLEmneplan.Text = mKLEmneplanFilename;
            textBoxKLFacetter.Text = mKLFacetterFilename;
            textBoxKLStikord.Text = mKLStikordFilename;

            textBoxDBServer.Text = "";

            //textBoxDBUser.Text = "eDoc";
            //textBoxDBPassword.Text = "eDocPass";
            //comboBoxDBDatabase.Items.Add("MFK_eDocProd");
            //comboBoxDBDatabase.SelectedIndex = 0;
            // -- End TEST --
        }

        private void buttonBrowseEmneplan_Click(object sender, EventArgs e)
        {
            mKLEmneplanFilename = GetXmlFile();
            if (!ValidateXmlFile(mKLEmneplanFilename, mKLEmneplanSchema))
            {
                mKLEmneplanFilename = "";
                MessageBox.Show("Xml filen overholder ikke xsd skemaet", "eDoc");
            }
            textBoxKLEmneplan.Text = mKLEmneplanFilename;
        }

        private void buttonBrowseFacetter_Click(object sender, EventArgs e)
        {
            mKLFacetterFilename = GetXmlFile();
            if (!ValidateXmlFile(mKLFacetterFilename, mKLFacetterSchema))
            {
                mKLFacetterFilename = "";
                MessageBox.Show("Xml filen overholder ikke xsd skemaet", "eDoc");
            }
            textBoxKLFacetter.Text = mKLFacetterFilename;
        }

        private void buttonBrowseStikord_Click(object sender, EventArgs e)
        {
            mKLStikordFilename = GetXmlFile();
            if (!ValidateXmlFile(mKLStikordFilename, mKLStikordSchema))
            {
                mKLStikordFilename = "";
                MessageBox.Show("Xml filen overholder ikke xsd skemaet", "eDoc");
            }
            textBoxKLStikord.Text = mKLStikordFilename;
        }

        public string KLEmneplanFilename { get { return mKLEmneplanFilename; } }
        public string KLFacetterFilename { get { return mKLFacetterFilename; } }
        public string KLStikordFilename { get { return mKLStikordFilename; } }

        public string ConnectString
        {
            get
            {
                if (radioButtonWinAuth.Checked)
                    return
                      "Data Source=" + textBoxDBServer.Text +
                      ";initial catalog=" + comboBoxDBDatabase.Text +
                      ";Integrated Security=SSPI;Persist Security Info=False;";
                else
                    return
                      "Data Source=" + textBoxDBServer.Text +
                      ";initial catalog=" + comboBoxDBDatabase.Text +
                      ";User ID=" + textBoxDBUser.Text +
                      ";Password=" + textBoxDBPassword.Text + ";";
            }
        }

        private string GetXmlFile()
        {
            openFileDialogXmlFiles.DefaultExt = "xml";
            openFileDialogXmlFiles.Multiselect = false;
            DialogResult dr = openFileDialogXmlFiles.ShowDialog();
            if (dr == DialogResult.OK && openFileDialogXmlFiles.FileName.EndsWith(".xml"))
            {
                return openFileDialogXmlFiles.FileName;
            }
            return "";
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void comboBoxDBDatabase_DropDown(object sender, EventArgs e)
        {
            comboBoxDBDatabase.Items.Clear();
            string[] databases = DB360.GetDatabases(textBoxDBServer.Text, textBoxDBUser.Text, textBoxDBPassword.Text, radioButtonWinAuth.Checked);
            for (int i = 0; i < databases.Length; i++)
            {
                comboBoxDBDatabase.Items.Add(databases[i]);
            }
        }

        private bool ValidateXmlFile(string pXmlFilename, string pXmlSchema)
        {
            pXmlSchema = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + pXmlSchema;
            mSchemaValid = true;

            XmlDocument doc = new XmlDocument();
            doc.Load(pXmlFilename);

            XmlTextReader r = new XmlTextReader(doc.OuterXml, XmlNodeType.Document, null);
            //Create a validating reader.
            XmlValidatingReader vr = new XmlValidatingReader(r);

            //Validate using the schemas stored in the schema collection.
            vr.Schemas.Add("", pXmlSchema);
            vr.ValidationType = ValidationType.Schema;
            vr.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(ValidationCallBack);

            while (vr.Read()) { }
            vr.Close();

            return mSchemaValid;
        }

        private void ValidationCallBack(object sender, System.Xml.Schema.ValidationEventArgs args)
        {
            mSchemaValid = false;
        }

        private void radioButtonAuth_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonWinAuth.Checked)
            {
                textBoxDBUser.Enabled = false;
                textBoxDBPassword.Enabled = false;
            }
            else
            {
                textBoxDBUser.Enabled = true;
                textBoxDBPassword.Enabled = true;
            }
        }
    }
}
