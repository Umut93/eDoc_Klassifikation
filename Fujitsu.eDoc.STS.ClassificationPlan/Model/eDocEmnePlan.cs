using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fujitsu.eDoc.STS.ClassificationPlan.Model
{
    public class EdocEmnePlan
    {
        public EdocEmnePlan()
        {

        }
        public EdocEmnePlan(Guid uUID, int recno, string code, string description, int classType, int secondaryClassType)
        {
            UUID = uUID;
            Recno = recno;
            Code = code;
            Description = description;
            ClassType = classType;
            ToSecondaryClassType = secondaryClassType;
        }

        public int Recno { get; set; }

        public Guid UUID { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public int ClassType { get; set; }

        public int ToSecondaryClassType { get; set; }

        public static List<EdocEmnePlan> GetEdocKLes()
        {

            List<EdocEmnePlan> eDocKLEs = new List<EdocEmnePlan>();
            string xmlQuery = Core.Common.GetResourceXml("GetAllKLEs.xml", typeof(EdocEmnePlan).Namespace.Replace("Model", "XML"), Assembly.GetExecutingAssembly());
            string result = Core.Common.ExecuteQuery(xmlQuery);

            XDocument xDoc = XDocument.Parse(result);

            foreach (XElement xe in xDoc.Descendants("RECORD"))
            {
                var isSucceded = Guid.TryParse(xe.Element("Fu_sts_KLE_GUID").Value, out Guid guid);

                EdocEmnePlan kle;

                int recno = int.Parse(xe.Element("Recno").Value);
                string code = xe.Element("Code").Value;
                string description = xe.Element("Description").Value;
                int toClassType = int.Parse(xe.Element("ToClassType").Value);
                int toSecondaryClassType = int.Parse(xe.Element("ToSecondaryClassType").Value);



                if (isSucceded)
                {

                    kle = new EdocEmnePlan(guid, recno, code, description, toClassType, toSecondaryClassType);
                }
                else
                {
                    var bytes = new Byte[16];
                    var emptyGuid = new Guid(bytes);

                    kle = new EdocEmnePlan(emptyGuid, recno, code, description, toClassType, toSecondaryClassType);
                }
                eDocKLEs.Add(kle);
            }
            return eDocKLEs;
        }

        public static List<EdocEmnePlan> GetEdocFacets()
        {

            List<EdocEmnePlan> eDocKLEs = new List<EdocEmnePlan>();
            string xmlQuery = Core.Common.GetResourceXml("GetAllFacets.xml", typeof(EdocEmnePlan).Namespace.Replace("Model", "XML"), Assembly.GetExecutingAssembly());
            string result = Core.Common.ExecuteQuery(xmlQuery);

            XDocument xDoc = XDocument.Parse(result);

            foreach (XElement xe in xDoc.Descendants("RECORD"))
            {
                var isSucceded = Guid.TryParse(xe.Element("Fu_sts_KLE_GUID").Value, out Guid guid);

                EdocEmnePlan kle;

                int recno = int.Parse(xe.Element("Recno").Value);
                string code = xe.Element("Code").Value;
                string description = xe.Element("Description").Value;
                int toClassType = int.Parse(xe.Element("ToClassType").Value);


                if (isSucceded)
                {

                    kle = new EdocEmnePlan(guid, recno, code, description, toClassType, 0);
                }
                else
                {
                    var bytes = new Byte[16];
                    var emptyGuid = new Guid(bytes);

                    kle = new EdocEmnePlan(emptyGuid, recno, code, description, toClassType, 0);
                }
                eDocKLEs.Add(kle);
            }
            return eDocKLEs;
        }
    }

}
