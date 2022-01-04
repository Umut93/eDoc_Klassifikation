
Opsætning af det fælles kommunale klassfikation alt afhænger om det er test eller prod miljø.


TEST
    <add key="CVR" value="86631628" /> Aftalte myndighed)
    <add key="ClientCertificate" value="5e1c480e" /> (Funktionscertifikat i anvendersystemet)
    <add key="Endpoint" value="https://klassifikation.eksterntest-stoettesystemerne.dk/klassifikationsystem_6" /> 
    <add key="STSIssuer" value="https://adgangsstyring.eksterntest-stoettesystemerne.dk/" /> (URL, som udsteder token = Service token provider)
    <add key="STSCertificateAlias" value="test-ekstern-adgangsstyring (funktionscertifikat)" /> (test certifikatet for adgangsstyring i ekstern test miljø - værdien skal matche CN)
    <add key="STSCertificate" value="5e089309" />
    <add key="ServiceEntityId" value="http://entityid.kombit.dk/service/klassifikation/6" />
    <add key="FKClassficationCertificateAlias" value="Klassifikation_T (funktionscertifikat)" /> (test certifikatet for kalde webservicen i serviceplatformen. Se digitaliseringsstyrelsen hjemmesider under certifikater)
    <add key="FKClassficationCertificateSerialNumber" value="5baaef71" />

PROD
	<add key="CVR" value="86631628" /> (Aftalte myndighed)
    <add key="ClientCertificate" value="5e1c480e" /> (Funktionscertifikat i anvendersystemet)
    <add key="Endpoint" value="https://klassifikation.stoettesystemerne.dk/klassifikationsystem_6" />
    <add key="STSIssuer" value="https://adgangsstyring.stoettesystemerne.dk/" /> (URL, som udsteder token = Service token provider)
	<add key="STSCertificateAlias" value="produktion-adgangsstyring (funktionscertifikat)" /> (PROD certifikatet for adgangsstyring i PROD miljøet - værdien skal matche CN)
    <add key="STSCertificate" value="5e089309" />
	<add key="ServiceEntityId" value="http://entityid.kombit.dk/service/klassifikation/6" />
    <add key="FKCertificate" value="5e089283" />
    <add key="FKClassficationCertificateAlias" value="Klassifikation_P (funktionscertifikat)" />
    <add key="FKClassficationCertificateSerialNumber" value="5a57688c" />
  



