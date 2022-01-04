using System;
using System.Windows.Forms;
using System.Xml;

namespace Fujitsu.Tools.KLPlanUpdate
{
    internal class MainGroup : TreeNode
    {
        private string mHovedgruppeNr;
        private string mHovedgruppeTitel;
        private DateTime mOprettet;
        private DateTime mUdgaaet;
        private bool mIsExpired = false;
        private int mRecno = 0;
        private int mClassTypeRecno = 0;

        private SaveStates mSaveState = SaveStates.Saved;

        internal MainGroup(XmlNode pMainGroup, NoarkSubarchive pSubArchive)
            : base()
        {
            mHovedgruppeNr = pMainGroup.SelectSingleNode("HovedgruppeNr").InnerText;
            mHovedgruppeTitel = pMainGroup.SelectSingleNode("HovedgruppeTitel").InnerText;
            mOprettet = DateTime.Parse(pMainGroup.SelectSingleNode("HovedgruppeAdministrativInfo/OprettetDato").InnerText);
            if (pMainGroup.SelectSingleNode("HovedgruppeAdministrativInfo/Historisk/UdgaaetDato") != null)
            {
                mUdgaaet = DateTime.Parse(pMainGroup.SelectSingleNode("HovedgruppeAdministrativInfo/Historisk/UdgaaetDato").InnerText);
                mIsExpired = true;
            }
            else
                if (pMainGroup.SelectSingleNode("HovedgruppeAdministrativInfo/Historisk/Flyttet/FlyttetDato") != null)
            {
                mUdgaaet = DateTime.Parse(pMainGroup.SelectSingleNode("HovedgruppeAdministrativInfo/Historisk/Flyttet/FlyttetDato").InnerText);
                mIsExpired = true;
            }
            if (pSubArchive != null)
            {
                mRecno = int.Parse(pSubArchive.Recno);
                mClassTypeRecno = int.Parse(pSubArchive.ToPrimaryClassType);

                if (mHovedgruppeNr + " " + mHovedgruppeTitel != pSubArchive.Description || mIsExpired != pSubArchive.IsExpired)
                {
                    mSaveState = SaveStates.Updated;
                }

                if (mIsExpired && !pSubArchive.IsExpired)
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

        public string HovedgruppeNr { get { return mHovedgruppeNr; } }
        public string HovedgruppeTitel { get { return mHovedgruppeTitel; } }
        public DateTime Oprettet { get { return mOprettet; } }
        public int Recno { get { return mRecno; } set { mRecno = value; } }
        public int ClassTypeRecno { get { return mClassTypeRecno; } set { mClassTypeRecno = value; } }
        public bool IsExpired { get { return mIsExpired; } }
        public SaveStates SaveState { get { return mSaveState; } }

        public void Save(DB360 pDB360)
        {
            switch (mSaveState)
            {
                case SaveStates.New:
                    pDB360.CreateMainGroup(this);
                    break;
                case SaveStates.Updated:
                    pDB360.UpdateMainGroup(this);
                    break;
                case SaveStates.Deleted:
                    pDB360.UpdateMainGroup(this);
                    break;
            }

            mSaveState = SaveStates.Saved;
            UpdateTextAndImage();
        }

        private void UpdateTextAndImage()
        {
            if (mRecno > 0)
            {
                this.Text = mHovedgruppeNr + " " + mHovedgruppeTitel + " (" + mRecno + ")";
            }
            else
            {
                this.Text = mHovedgruppeNr + " " + mHovedgruppeTitel;
            }

            this.ImageIndex = SaveStateImage.GetImageIndex(mSaveState);
            this.SelectedImageIndex = this.ImageIndex;
        }
    }
}
