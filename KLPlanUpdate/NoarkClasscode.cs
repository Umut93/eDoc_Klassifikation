using System.Collections.Generic;

namespace Fujitsu.Tools.KLPlanUpdate
{
    internal class NoarkClasscode
    {
        private string mRecno;
        private string mCode;
        private string mDescription;
        private string mToClassType;
        private string mToSecondaryClassType;
        private bool mIsExpired;
        private string mScrapCode;

        public NoarkClasscode(string pRecno, string pCode, string pDescription, string pToClassType, string pToSecondaryClassType, bool pIsExpired, string pScrapCode)
        {
            mRecno = pRecno;
            mCode = pCode;
            mDescription = pDescription;
            mToClassType = pToClassType;
            mToSecondaryClassType = pToSecondaryClassType;
            mIsExpired = pIsExpired;
            mScrapCode = pScrapCode;
        }

        public string Recno { get { return mRecno; } }
        public string Code { get { return mCode; } }
        public string Description { get { return mDescription; } }
        public string ToClassType { get { return mToClassType; } }
        public string ToSecondaryClassType { get { return mToSecondaryClassType; } }
        public bool IsExpired { get { return mIsExpired; } }
        public string ScrapCode { get { return mScrapCode; } }
    }

    internal class NoarkClasscodeList : List<NoarkClasscode>
    {
        public NoarkClasscode FindNoarkClasscode(string pCode)
        {
            foreach (NoarkClasscode s in this)
            {
                if (s.Code == pCode)
                    return s;
            }
            return null;
        }

        public NoarkClasscodeList FindNoarkClasscodes(string pCode)
        {
            NoarkClasscodeList codes = new NoarkClasscodeList();
            foreach (NoarkClasscode s in this)
            {
                if (s.Code == pCode)
                    codes.Add(s);
            }
            return codes;
        }

        public NoarkClasscode FindNoarkClasscode(string pCode, string pDescription)
        {
            NoarkClasscode classCode = null;
            foreach (NoarkClasscode s in this)
            {
                if (s.Code == pCode)
                {
                    classCode = s;
                    if (s.Description == pDescription)
                        return classCode;
                }
            }
            return classCode;
        }
    }
}
