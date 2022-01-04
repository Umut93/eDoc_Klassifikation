using Digst.OioIdws.Soap.Behaviors;
using Digst.OioIdws.Soap.Bindings;
using Fujitsu.eDoc.Organisation.Integration.Models;
using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using Fujitsu.eDoc.STS.ClassificationPlan.SF1510_V6_KlassifikationSystemService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;

namespace Fujitsu.eDoc.STS.ClassificationPlan
{
    public class KlassifikationSystemService : IKlassifikationSystemService
    {
        private int Offset = 0;
        private const int MaxRetrieval = 5000;
        List<STSKLE> STSKLEList = new List<STSKLE>();

        /// <summary>
        /// Retrieves all KLEs which either is expired or not.
        /// </summary>
        /// <param name="klasseServiceEndPoint"></param>
        /// <param name="certificateSerialNumber"></param>
        /// <param name="brugerVendtNoegle"></param>
        /// <returns></returns>
        /// 

        public List<STSKLE> Fremsoegobjekthierarki(FKContext fKContext, string brugerVendtNoegle, SecurityToken token)
        {
            var client = GetKlassifikationSystemPortTypeClient(fKContext, token);
            try
            {
                int receivedCount = MaxRetrieval;

                while (receivedCount == MaxRetrieval)
                {
                    fremsoegobjekthierarkiRequest requestType = CreateSearch(brugerVendtNoegle, Offset.ToString());
                    var respons = client.fremsoegobjekthierarki(requestType);

                    if (respons.FremsoegObjekthierarkiOutput.StandardRetur.StatusKode == "20")
                    {
                        receivedCount = respons.FremsoegObjekthierarkiOutput.Klasser.Length;
                        Offset += receivedCount;

                        FiltreretOejebliksbilledeType2[] klasser = respons.FremsoegObjekthierarkiOutput.Klasser;
                        for (int i = 0; i < klasser.Length; i++)
                        {
                            bool isSucceded = Guid.TryParse(klasser[i].ObjektType.UUIDIdentifikator, out Guid guid);

                            if (isSucceded)
                            {
                                STSKLE stsKle = new STSKLE
                                {

                                    UUID = Guid.Parse(klasser[i].ObjektType.UUIDIdentifikator),
                                    Code = klasser[i].Registrering[0].AttributListe.First().BrugervendtNoegleTekst,
                                    TitleText = klasser[i].Registrering[0].AttributListe.First().TitelTekst,
                                    Item = klasser[i].Registrering.First().TilstandListe.Last().ErPubliceretIndikator
                                };
                                STSKLEList.Add(stsKle);
                            }
                        }
                        STSKLEList.Sort(delegate (STSKLE a, STSKLE b)

                        {
                            if (a.Code == null && b.Code == null) return 0;
                            else if (a.Code == null) return -1;
                            else if (b.Code == null) return 1;
                            else return a.Code.CompareTo(b.Code);
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                EventLog.LogToEventLog("Fremsoegobjekthierarki failed" + Environment.NewLine + ex.Message + Environment.NewLine + ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }

            return STSKLEList;
        }


        private fremsoegobjekthierarkiRequest CreateSearch(string brugerVendtNoegle, string offset)
        {
            RequestHeaderType requestHeaderType = GetRequestHeaderType();

            return new fremsoegobjekthierarkiRequest
            {
                RequestHeader = requestHeaderType,
                FremsoegObjekthierarkiInput = new FremsoegObjekthierarkiInputType
                {
                    KlassifikationSoegEgenskab = new EgenskabType
                    {
                        BrugervendtNoegleTekst = brugerVendtNoegle
                    },
                    SoegVirkning = new SoegVirkningType { FraTidspunkt = new TidspunktType { Item = true }, TilTidspunkt = new TidspunktType { Item = DateTime.Now } },
                    MaksimalAntalKvantitet = MaxRetrieval.ToString(),
                    FoersteResultatReference = offset,
                }
            };
        }

        public KlassifikationSystemPortType GetKlassifikationSystemPortTypeClient(FKContext fKContext, SecurityToken token)
        {
            SoapBinding soapBinding = new SoapBinding();
            soapBinding._maxReceivedMessageSize = 2147483647;

            EndpointIdentity identity = EndpointIdentity.CreateDnsIdentity(fKContext.FKClassficationCertificateAlias);
            EndpointAddress endpointAddress = new EndpointAddress(new Uri(fKContext.Endpoint), identity);


            KlassifikationSystemPortTypeClient client = new KlassifikationSystemPortTypeClient(soapBinding, endpointAddress);

            // loads the funtioncertifikat for klassification service
            client.ChannelFactory.Credentials.ServiceCertificate.SetDefaultCertificate(System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine,
                                                                                       System.Security.Cryptography.X509Certificates.StoreName.My,
                                                                                       System.Security.Cryptography.X509Certificates.X509FindType.FindBySerialNumber,
                                                                                       fKContext.FKClassficationCertificateSerialNumber);


            // loads the functioncertificat installed on the anvendersystem
            client.ChannelFactory.Credentials.ClientCertificate.SetCertificate(System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine,
                                                                               System.Security.Cryptography.X509Certificates.StoreName.My,
                                                                               System.Security.Cryptography.X509Certificates.X509FindType.FindBySerialNumber,
                                                                               fKContext.ClientCertificate);

            //IN PROD DONT..
            client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode =
            System.ServiceModel.Security.X509CertificateValidationMode.None;


            // This sets the MINIMUM level. Since the request header should not be signed, we set it to none.
            client.Endpoint.Contract.ProtectionLevel = ProtectionLevel.None;

            // adding the custom beheviors from Digst.OioIdws.Soap
            ClientMessageInspectorBehavior c = new ClientMessageInspectorBehavior();
            SoapClientBehavior s = new SoapClientBehavior();
            client.ChannelFactory.Endpoint.EndpointBehaviors.Add(c);
            client.ChannelFactory.Endpoint.EndpointBehaviors.Add(s);

            return client.ChannelFactory.CreateChannelWithIssuedToken(token);
        }


        public RequestHeaderType GetRequestHeaderType()
        {
            RequestHeaderType requestHeaderType = new RequestHeaderType()
            {
                TransactionUUID = Guid.NewGuid().ToString()
            };

            return requestHeaderType;
        }

        public class ParagraphTitles : IComparable<ParagraphTitles>
        {
            public int[] Paragraphs { get; set; }
            public string SParagraphs { get; set; }
            public List<ParagraphTitles> Chapters { get; set; }
            public string Text { get; set; }
            public Guid UUID { get; set; }
            public bool isExpired { get; set; }


            public ParagraphTitles() { }
            public ParagraphTitles(string code, string titletext, Guid STSKLEUUID, bool Expiration)
            {
                SParagraphs = code;
                Paragraphs = code.Trim().Split(new char[] { '.' }).Select(x => int.Parse(x)).ToArray();
                Text = titletext;
                UUID = STSKLEUUID;
                isExpired = Expiration;
            }
            public int CompareTo(ParagraphTitles other)
            {
                int minSize = Math.Min(Paragraphs.Length, other.Paragraphs.Length);

                for (int i = 0; i < minSize; i++)
                {
                    if (Paragraphs[i] != other.Paragraphs[i])
                    {
                        return Paragraphs[i].CompareTo(other.Paragraphs[i]);
                    }
                }

                return Paragraphs.Length.CompareTo(other.Paragraphs.Length);
            }

            public void CreateTree(List<ParagraphTitles> chapters, int level)
            {
                var groups = chapters.GroupBy(x => x.Paragraphs[level]).ToList();

                foreach (var group in groups)
                {
                    List<ParagraphTitles> orderedChapters = group.OrderBy(x => x.Paragraphs.Length).ToList();
                    if (this.Chapters == null) this.Chapters = new List<ParagraphTitles>();
                    this.Chapters.Add(orderedChapters.First());
                    orderedChapters.First().CreateTree(orderedChapters.Skip(1).ToList(), level + 1);
                }
            }

        }
    }
}
