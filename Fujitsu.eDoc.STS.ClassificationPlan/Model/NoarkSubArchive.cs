using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fujitsu.eDoc.STS.ClassificationPlan.Model
{
    public class NoarkSubArchive
    {
        public NoarkSubArchive()
        {

        }
        public NoarkSubArchive(Guid uUID, int recno, string code, string description, string primaryClassType)
        {
            UUID = uUID;
            Recno = recno;
            Code = code;
            Description = description;
            PrimaryClassType = primaryClassType;
        }

        public int Recno { get; set; }

        public Guid? UUID { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string PrimaryClassType { get; set; }

        public static List<NoarkSubArchive> GetNoarkSubArchives()
        {
            List<NoarkSubArchive> noarkSubArchives = new List<NoarkSubArchive>();
            string xmlQuery = Core.Common.GetResourceXml("GetNoarkSubArchives.xml", typeof(NoarkSubArchive).Namespace.Replace("Model", "XML"), Assembly.GetExecutingAssembly());
            string result = Core.Common.ExecuteQuery(xmlQuery);

            XDocument xDoc = XDocument.Parse(result);

            foreach (XElement xe in xDoc.Descendants("RECORD"))
            {
                NoarkSubArchive noarkSubArchive;

                string code = xe.Element("Code").Value;
                int recno = int.Parse(xe.Element("Recno").Value);
                string description = xe.Element("Description").Value;
                string primaryClassType = xe.Element("ToPrimaryClassType").Value;

                var isSucceded = Guid.TryParse(xe.Element("Fu_sts_KLE_GUID").Value, out Guid guid);

                if (isSucceded)
                {
                    noarkSubArchive = new NoarkSubArchive(guid, recno, code, description, primaryClassType);
                }
                else
                {
                    var bytes = new Byte[16];
                    var emptyGuid = new Guid(bytes);
                    noarkSubArchive = new NoarkSubArchive(emptyGuid, recno, code, description, primaryClassType);
                }

                noarkSubArchives.Add(noarkSubArchive);
            }
            return noarkSubArchives;
        }

    }

}
