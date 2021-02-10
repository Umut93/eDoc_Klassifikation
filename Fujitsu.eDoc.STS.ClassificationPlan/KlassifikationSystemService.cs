using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using Fujitsu.eDoc.STS.ClassificationPlan.SF1510_KlassifikationSystemService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fujitsu.eDoc.STS.ClassificationPlan
{
    public class KlassifikationSystemService : IKlassifikationSystemService
    {
        /// <summary>
        /// Retrieves all KLEs which either is expired or not.
        /// </summary>
        /// <param name="CVR"></param>
        /// <param name="klasseServiceEndPoint"></param>
        /// <param name="certificateSerialNumber"></param>
        /// <param name="brugerVendtNoegle"></param>
        /// <returns></returns>
        public async Task<List<STSKLE>> FremsoegobjekthierarkiAsync(string CVR, string klasseServiceEndPoint, string certificateSerialNumber, string brugerVendtNoegle)
        {
            List<STSKLE> list = new List<STSKLE>();
            using (KlassifikationSystemPortTypeClient kl = GetKlassifikationSystemPortTypeClient(klasseServiceEndPoint, certificateSerialNumber))
            {
                RequestHeaderType requestHeaderType = GetRequestHeaderType();
                FremsoegobjekthierarkiRequestType requestType = new FremsoegobjekthierarkiRequestType
                {
                    AuthorityContext = new AuthorityContextType { MunicipalityCVR = CVR },
                    FremsoegObjekthierarkiInput = new FremsoegObjekthierarkiInputType
                    {
                        KlassifikationSoegEgenskab = new EgenskabType2
                        {
                            BrugervendtNoegleTekst = brugerVendtNoegle
                        },
                        SoegVirkning = new SoegVirkningType { FraTidspunkt = new TidspunktType { Item = true }, TilTidspunkt = new TidspunktType { Item = DateTime.Now } }
                        //SoegRegistrering = new SoegRegistreringType { FraTidspunkt= new TidspunktType { Item= true} } Shoes also those that is UDGÅET. Shows an registry logning on each KLE
                        //SoegRegistrering = new SoegRegistreringType { TilTidspunkt = new TidspunktType { Item = DateTime.Now }

                    }

                };

                try
                {
                    var respons = await kl.fremsoegobjekthierarkiAsync(requestHeaderType, requestType);

                    if (respons.FremsoegobjekthierarkiResponse1.FremsoegObjekthierarkiOutput.StandardRetur.StatusKode == "20")
                    {
                        Dictionary<string, string> stsKLEGUIDPair = new Dictionary<string, string>();

                        FiltreretOejebliksbilledeType[] klasser = respons.FremsoegobjekthierarkiResponse1.FremsoegObjekthierarkiOutput.Klasser;
                        for (int i = 0; i < klasser.Length; i++)
                        {
                            bool isSucceded = Guid.TryParse(klasser[i].ObjektType.UUIDIdentifikator, out Guid guid);

                            if (isSucceded)
                            {
                                STSKLE stsKle = new STSKLE
                                {

                                    UUID = Guid.Parse(klasser[i].ObjektType.UUIDIdentifikator),
                                    Code = klasser[i].Registrering[0].AttributListe.Egenskab[0].BrugervendtNoegleTekst,
                                    TitleText = klasser[i].Registrering[0].AttributListe.Egenskab[0].TitelTekst,
                                    Item = klasser[i].Registrering[0].RelationListe.Facet.Virkning.TilTidspunkt.Item
                                };
                                list.Add(stsKle);
                            }
                        }
                        list.Sort(delegate (STSKLE a, STSKLE b)

                        {
                            if (a.Code == null && b.Code == null) return 0;
                            else if (a.Code == null) return -1;
                            else if (b.Code == null) return 1;
                            else return a.Code.CompareTo(b.Code);
                        });


                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            return list;

        }

        public KlassifikationSystemPortTypeClient GetKlassifikationSystemPortTypeClient(string caseServiceEndPoint, string certificateSerialNumber)
        {
            System.Security.Cryptography.X509Certificates.X509FindType CertificateFindType =
                                        System.Security.Cryptography.X509Certificates.X509FindType.FindBySerialNumber;

            System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Certificate;
            binding.MaxReceivedMessageSize = 2147483647;


            System.ServiceModel.EndpointAddress remoteAddress =
                                new System.ServiceModel.EndpointAddress(caseServiceEndPoint);

            KlassifikationSystemPortTypeClient client = new KlassifikationSystemPortTypeClient(binding, remoteAddress);
            client.ChannelFactory.Credentials.ClientCertificate.SetCertificate(System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine,
                                                                               System.Security.Cryptography.X509Certificates.StoreName.My, CertificateFindType,
                                                                               certificateSerialNumber);
            return client;
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
