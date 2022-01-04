using System;
using System.Windows.Forms;
using System.Xml;

namespace Fujitsu.Tools.KLPlanUpdate
{
    internal class SubGroup : TreeNode
    {
        private string mEmneNr;
        private string mEmneTekst;
        private DateTime mOprettet;
        private DateTime mUdgaaet;
        private bool mIsExpired = false;
        private int mRecno = 0;
        private int mScrapCode;
        private int mPreserveYears;
        private string mBevaringJaevnfoerArkivlovenTekst;

        private SaveStates mSaveState = SaveStates.Saved;

        internal SubGroup(XmlNode pSubGroup, NoarkClasscode pClasscode)
          : base()
        {
            mEmneNr = pSubGroup.SelectSingleNode("EmneNr").InnerText;
            mEmneTekst = pSubGroup.SelectSingleNode("EmneTitel").InnerText;
            mOprettet = DateTime.Parse(pSubGroup.SelectSingleNode("EmneAdministrativInfo/OprettetDato").InnerText);
            if (pSubGroup.SelectSingleNode("EmneAdministrativInfo/Historisk/UdgaaetDato") != null)
            {
                mUdgaaet = DateTime.Parse(pSubGroup.SelectSingleNode("EmneAdministrativInfo/Historisk/UdgaaetDato").InnerText);
                mIsExpired = true;
            }

            mBevaringJaevnfoerArkivlovenTekst = pSubGroup.SelectSingleNode("BevaringJaevnfoerArkivloven").InnerText;
            if (mBevaringJaevnfoerArkivlovenTekst == "B")
                mScrapCode = 1;
            else if (mBevaringJaevnfoerArkivlovenTekst.Substring(0, 1) == "K")
            {
                mScrapCode = 2;
                if (mBevaringJaevnfoerArkivlovenTekst.Length > 1)
                {
                    mPreserveYears = int.Parse(mBevaringJaevnfoerArkivlovenTekst.Substring(1, mBevaringJaevnfoerArkivlovenTekst.Length));
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

                if (mEmneTekst != pClasscode.Description || mIsExpired != pClasscode.IsExpired)
                {
                    mSaveState = SaveStates.Updated;
                }

                if (mIsExpired && !pClasscode.IsExpired)
                {
                    mSaveState = SaveStates.Deleted;
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

        public string EmneNr { get { return mEmneNr; } }
        public string EmneTekst { get { return mEmneTekst; } }
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
                    pDB360.CreateSubGroup(this);
                    break;
                case SaveStates.Updated:
                    pDB360.UpdateSubGroup(this);
                    break;
                case SaveStates.Deleted:
                    pDB360.UpdateSubGroup(this);
                    break;
            }

            mSaveState = SaveStates.Saved;
            UpdateTextAndImage();
        }

        private void UpdateTextAndImage()
        {
            if (mRecno > 0)
            {
                this.Text = mEmneNr + " " + mEmneTekst + " (" + mRecno + ")";
            }
            else
            {
                this.Text = mEmneNr + " " + mEmneTekst;
            }

            this.ImageIndex = SaveStateImage.GetImageIndex(mSaveState);
            this.SelectedImageIndex = this.ImageIndex;
        }
    }
}
