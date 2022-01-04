using System;

namespace Fujitsu.eDoc.STS.ClassificationPlan.Model
{
    public class STSKLE
    {
        public STSKLE()
        {

        }

        public STSKLE(Guid uUID, string recno, string code, string titleText, string description)
        {
            UUID = uUID;
            Code = code;
            TitleText = titleText;
        }

        public Guid UUID { get; set; }

        public string Code { get; set; }

        public string TitleText { get; set; }


        public bool Item { get; set; }

        public bool IsExpired
        {
            get
            {
                if (Item)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
