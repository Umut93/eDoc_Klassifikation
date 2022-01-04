using System;
using System.Security.Cryptography.X509Certificates;

namespace Fujitsu.eDoc.Organisation.FKIntegration
{
    public static class CertificateLoader
    {
        public static X509Certificate2 LoadCertificate(StoreName storeName, StoreLocation storeLocation, string serialNumber)
        {
            X509Store store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection result = store.Certificates.Find(X509FindType.FindBySerialNumber, serialNumber, false);

            if (result.Count == 0)
            {
                throw new ArgumentException("No certificate with the serial number " + serialNumber + " is found.");
            }

            return result[0];
        }
    }
}
