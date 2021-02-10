using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public object Item { get; set; }

        public bool IsExpired
        {
            get
            {
                if (DateTime.TryParse(Item.ToString(), out DateTime result))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


    }
}
