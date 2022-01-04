using System.Collections.Generic;

namespace Fujitsu.Tools.KLPlanUpdate
{
    internal class NoarkSubarchive
    {
        private string mRecno;
        private string mCode;
        private string mDescription;
        private string mToPrimaryClassType;
        private bool mIsExpired;

        public NoarkSubarchive(string pRecno, string pCode, string pDescription, string pToPrimaryClassType, bool pIsExpired)
        {
            mRecno = pRecno;
            mCode = pCode;
            mDescription = pDescription;
            mToPrimaryClassType = pToPrimaryClassType;
            mIsExpired = pIsExpired;
        }

        public string Recno { get { return mRecno; } }
        public string Code { get { return mCode; } }
        public string Description { get { return mDescription; } }
        public string ToPrimaryClassType { get { return mToPrimaryClassType; } }
        public bool IsExpired { get { return mIsExpired; } }
    }

    internal class NoarkSubarchiveList : List<NoarkSubarchive>
    {
        public NoarkSubarchive FindNoarkSubarchive(string pCode)
        {
            foreach (NoarkSubarchive s in this)
            {
                if (s.Code.StartsWith(pCode + " "))
                    return s;
            }
            return null;
        }
    }
}
