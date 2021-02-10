using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace Fujitsu.eDoc.STS.ClassificationPlan.Model
{
    public class NoarkClassificationType
    {
        public NoarkClassificationType()
        {

        }

        public NoarkClassificationType(int recno, string code, string description)
        {
            Recno = recno;
            Code = code;
            Description = description;
        }

        public int Recno { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public static List<NoarkClassificationType> GetGetNoarkClassificationTypes()
        {
            List<NoarkClassificationType> noarkClassificationTypes = new List<NoarkClassificationType>();
            string xmlQuery = Core.Common.GetResourceXml("GetNoarkClassificationType.xml", typeof(NoarkClassificationType).Namespace.Replace("Model", "XML"), Assembly.GetExecutingAssembly());
            string result = Core.Common.ExecuteQuery(xmlQuery);

            XDocument xDoc = XDocument.Parse(result);

            foreach (XElement xe in xDoc.Descendants("RECORD"))
            {
                int recno = int.Parse(xe.Element("Recno").Value);
                string code = xe.Element("Code").Value;
                string description = xe.Element("Description").Value;

                NoarkClassificationType noarkClassificationType = new NoarkClassificationType(recno, code, description);

                noarkClassificationTypes.Add(noarkClassificationType);
            }
            return noarkClassificationTypes;
        }

    }
}