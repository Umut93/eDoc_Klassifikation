namespace Fujitsu.eDoc.Organisation.Integration.Models
{
    public class FKContext
    {
        public string CVR { get; set; }

        public string Endpoint { get; set; }

        public string ClientCertificate { get; set; }

        public string STSIssuer { get; set; }

        public string STSCertificateAlias { get; set; }

        public string STSCertificate { get; set; }

        public string ServiceEntityId { get; set; }

        public string FKClassficationCertificateAlias { get; set; }

        public string FKClassficationCertificateSerialNumber { get; set; }
    }
}
