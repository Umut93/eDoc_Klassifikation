using System;
using System.Windows.Forms;
using System.Xml;

namespace Fujitsu.Tools.KLPlanUpdate
{
    internal class Group : TreeNode
    {
        private string mGruppeNr;
        private string mGruppeTitel;
        private DateTime mOprettet;
        private DateTime mUdgaaet;
        private bool mIsExpired = false;
        private int mRecno = 0;
        private int mPrimaryClassTypeRecno = 0;
        private int mSecondaryClassTypeRecno = 0;

        private SaveStates mSaveState = SaveStates.Saved;

        internal Group(XmlNode pGroup, NoarkClasscode pClasscode)
          : base()
        {
            mGruppeNr = pGroup.SelectSingleNode("GruppeNr").InnerText;
            mGruppeTitel = pGroup.SelectSingleNode("GruppeTitel").InnerText;
            mOprettet = DateTime.Parse(pGroup.SelectSingleNode("GruppeAdministrativInfo/OprettetDato").InnerText);
            if (pGroup.SelectSingleNode("GruppeAdministrativInfo/Historisk/UdgaaetDato") != null)
            {
                mUdgaaet = DateTime.Parse(pGroup.SelectSingleNode("GruppeAdministrativInfo/Historisk/UdgaaetDato").InnerText);
                mIsExpired = true;
            }
            else
                if (pGroup.SelectSingleNode("GruppeAdministrativInfo/Historisk/Flyttet/FlyttetDato") != null)
            {
                mUdgaaet = DateTime.Parse(pGroup.SelectSingleNode("GruppeAdministrativInfo/Historisk/Flyttet/FlyttetDato").InnerText);
                mIsExpired = true;
            }

            if (pClasscode != null)
            {
                mRecno = int.Parse(pClasscode.Recno);
                mPrimaryClassTypeRecno = int.Parse(pClasscode.ToClassType);
                int.TryParse(pClasscode.ToSecondaryClassType, out mSecondaryClassTypeRecno);

                if (mGruppeTitel != pClasscode.Description || mIsExpired != pClasscode.IsExpired)
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

        public string GruppeNr { get { return mGruppeNr; } }
        public string GruppeTitel { get { return mGruppeTitel; } }
        public DateTime Oprettet { get { return mOprettet; } }
        public int Recno { get { return mRecno; } set { mRecno = value; } }
        public int PrimaryClassTypeRecno { get { return mPrimaryClassTypeRecno; } set { mPrimaryClassTypeRecno = value; } }
        public int SecondaryClassTypeRecno { get { return mSecondaryClassTypeRecno; } set { mSecondaryClassTypeRecno = value; } }
        public bool IsExpired { get { return mIsExpired; } }
        public SaveStates SaveState { get { return mSaveState; } }

        public void Save(DB360 pDB360)
        {
            switch (mSaveState)
            {
                case SaveStates.New:
                    pDB360.CreateGroup(this);
                    break;
                case SaveStates.Updated:
                    pDB360.UpdateGroup(this);
                    break;
                case SaveStates.Deleted:
                    pDB360.UpdateGroup(this);
                    break;
            }

            mSaveState = SaveStates.Saved;
            UpdateTextAndImage();
        }

        private void UpdateTextAndImage()
        {
            if (mRecno > 0)
            {
                this.Text = mGruppeNr + " " + mGruppeTitel + " (" + mRecno + ")";
            }
            else
            {
                this.Text = mGruppeNr + " " + mGruppeTitel;
            }

            this.ImageIndex = SaveStateImage.GetImageIndex(mSaveState);
            this.SelectedImageIndex = this.ImageIndex;
        }
    }
}
