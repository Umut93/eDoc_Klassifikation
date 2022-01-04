using Fujitsu.eDoc.Organisation.Integration.Models;
using System.Configuration;

namespace Fujitsu.eDoc.Organisation.FKIntegration.Invocation
{
    public class FKContextFetcher
    {
        public static FKContext GetSettings()
        {
            return new FKContext
            {
                CVR = System.Configuration.ConfigurationManager.AppSettings["Klassifikation.CVR"],
                ClientCertificate = ConfigurationManager.AppSettings["Klassifikation.ClientCertificate"],
                STSIssuer = ConfigurationManager.AppSettings["Klassifikation.STSIssuer"],
                STSCertificateAlias = ConfigurationManager.AppSettings["Klassifikation.STSCertificateAlias"],
                STSCertificate = ConfigurationManager.AppSettings["Klassifikation.STSCertificate"],
                ServiceEntityId = ConfigurationManager.AppSettings["Klassifikation.ServiceEntityId"],
                FKClassficationCertificateAlias = ConfigurationManager.AppSettings["Klassifikation.FKClassficationCertificateAlias"],
                FKClassficationCertificateSerialNumber = ConfigurationManager.AppSettings["Klassifikation.FKClassficationCertificateSerialNumber"],
                Endpoint = ConfigurationManager.AppSettings["Klassifikation.Endpoint"]
            };
        }
    }
}