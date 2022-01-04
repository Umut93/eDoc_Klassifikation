using System;
using System.Windows.Forms;
using System.Xml;

namespace Fujitsu.Tools.KLPlanUpdate
{
    internal class FacetCategory : TreeNode
    {
        private string mHandlingsfacetKategoriKode;
        private string mHandlingsfacetKategoriTekst;
        private DateTime mOprettet;
        private DateTime mUdgaaet;
        private bool mIsExpired = false;
        private int mRecno = 0;

        private SaveStates mSaveState = SaveStates.Saved;

        internal FacetCategory(XmlNode pFacet, NoarkClasscode pClasscode)
          : base()
        {
            mHandlingsfacetKategoriKode = pFacet.SelectSingleNode("HandlingsfacetKategoriNr").InnerText;
            mHandlingsfacetKategoriTekst = pFacet.SelectSingleNode("HandlingsfacetKategoriTitel").InnerText;
            mOprettet = DateTime.Parse(pFacet.SelectSingleNode("HandlingsfacetKategoriAdministrativInfo/OprettetDato").InnerText);
            if (pFacet.SelectSingleNode("HandlingsfacetKategoriAdministrativInfo/Historisk/UdgaaetDato") != null)
            {
                mUdgaaet = DateTime.Parse(pFacet.SelectSingleNode("HandlingsfacetKategoriAdministrativInfo/Historisk/UdgaaetDato").InnerText);
                mIsExpired = true;
            }

            if (pClasscode != null)
            {
                mRecno = int.Parse(pClasscode.Recno);

                if (mHandlingsfacetKategoriTekst != pClasscode.Description)
                {
                    mSaveState = SaveStates.Updated;
                }

                if (mIsExpired && !pClasscode.IsExpired)
                {
                    mSaveState = SaveStates.Deleted;
                }
            }
            else
            {
                mSaveState = SaveStates.New;
            }

            UpdateTextAndImage();
        }

        public string HandlingsfacetKategoriKode { get { return mHandlingsfacetKategoriKode; } }
        public string HandlingsfacetKategoriTekst { get { return mHandlingsfacetKategoriTekst; } }
        public DateTime Oprettet { get { return mOprettet; } }
        public int Recno { get { return mRecno; } set { mRecno = value; } }
        public bool IsExpired { get { return mIsExpired; } }
        public SaveStates SaveState { get { return mSaveState; } }

        public void Save(DB360 pDB360)
        {
            switch (mSaveState)
            {
                case SaveStates.New:
                    pDB360.CreateFacetCategory(this);
                    break;
                case SaveStates.Updated:
                    pDB360.UpdateFacetCategory(this);
                    break;
                case SaveStates.Deleted:
                    pDB360.UpdateFacetCategory(this);
                    break;
            }

            mSaveState = SaveStates.Saved;
            UpdateTextAndImage();
        }

        private void UpdateTextAndImage()
        {
            if (mRecno > 0)
            {
                this.Text = mHandlingsfacetKategoriKode + " " + mHandlingsfacetKategoriTekst + " (" + mRecno + ")";
            }
            else
            {
                this.Text = mHandlingsfacetKategoriKode + " " + mHandlingsfacetKategoriTekst;
            }

            this.ImageIndex = SaveStateImage.GetImageIndex(mSaveState);
            this.SelectedImageIndex = this.ImageIndex;
        }
    }
}
