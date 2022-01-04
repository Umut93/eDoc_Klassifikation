using Fujitsu.eDoc.Organisation.Integration.Models;
using Fujitsu.eDoc.STS.ClassificationPlan;
using System;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;

namespace Fujitsu.eDoc.Organisation.FKIntegration
{
    public static class TokenFetcher
    {
        public static SecurityToken IssueToken(FKContext fKContext)
        {

            SecurityToken token = null;
            X509Certificate2 certificate = CertificateLoader.LoadCertificate(StoreName.My, StoreLocation.LocalMachine, fKContext.ClientCertificate);

            var absoluteUri = new Uri(fKContext.ServiceEntityId).AbsoluteUri;
            var cacheKey = new Guid(MD5.Create().ComputeHash(Encoding.Default.GetBytes(absoluteUri + "_" + fKContext.CVR))).ToString();
            var inCache = CacheHelper.IsIncache(cacheKey);
            var needNewToken = false;


            if (inCache)
            {
                token = CacheHelper.GetFromCache<GenericXmlSecurityToken>(cacheKey);

                if (token.ValidTo.CompareTo(DateTime.UtcNow) < 0)
                    needNewToken = true;
            }

            if (inCache == false || needNewToken == true)
            {
                token = SendSecurityTokenRequest(absoluteUri, fKContext, certificate);
                CacheHelper.SaveTocache(cacheKey, token, token.ValidTo);
            }

            return token;
        }


        private static SecurityToken SendSecurityTokenRequest(string absoluteUri, FKContext fKContext, X509Certificate2 clientCertificate)
        {
            var rst = new RequestSecurityToken
            {
                AppliesTo = new EndpointReference(fKContext.ServiceEntityId),
                RequestType = RequestTypes.Issue,
                TokenType = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0",
                KeyType = KeyTypes.Asymmetric,
                Issuer = new EndpointReference(fKContext.STSIssuer),
                UseKey = new UseKey(new X509SecurityToken(clientCertificate))
            };
            rst.Claims.Dialect = "http://docs.oasis-open.org/wsfed/authorization/200706/authclaims";
            rst.Claims.Add(new RequestClaim("dk:gov:saml:attribute:CvrNumberIdentifier", false, fKContext.CVR));

            var client = GenerateStsCertificateClientChannel(clientCertificate, fKContext);
            return client.Issue(rst);
        }



        private static IWSTrustChannelContract GenerateStsCertificateClientChannel(X509Certificate2 clientCertificate, FKContext fKContext)
        {
            EndpointAddress stsAddress = new EndpointAddress(new Uri($"{fKContext.STSIssuer}runtime/services/kombittrust/14/certificatemixed"), EndpointIdentity.CreateDnsIdentity(fKContext.STSCertificateAlias));
            var binding = new MutualCertificateWithMessageSecurityBinding(null);
            var factory = new WSTrustChannelFactory(binding, stsAddress);
            factory.TrustVersion = TrustVersion.WSTrust13;

            factory.Credentials.ClientCertificate.Certificate = clientCertificate;

            X509Certificate2 certificate = CertificateLoader.LoadCertificate(
                                                 System.Security.Cryptography.X509Certificates.StoreName.My,
                                                 System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine,
                                                 fKContext.STSCertificate);

            factory.Credentials.ServiceCertificate.ScopedCertificates.Add(stsAddress.Uri, certificate);
            factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

            // Disable revocation checking (do not use in production)
            // Should be uncommented if you intent to call DemoService locally.
            // factory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;


            factory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            factory.Endpoint.Contract.ProtectionLevel = ProtectionLevel.Sign;

            return factory.CreateChannel();
        }
    }
}
