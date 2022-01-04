using System;
using System.Xml;

namespace Fujitsu.Tools.KLPlanUpdate
{
    internal class Subject
    {
        private string mDescription = null;
        private string mEmneNr = null;
        private string mFacet = null;
        private DateTime mOprettet;
        private DateTime mUdgaaet;
        private bool mIsExpired = false;

        internal Subject(XmlNode pSubjectNode)
        {

            switch (pSubjectNode.LocalName)
            {
                case "StikordTilHovedgruppe":
                    mDescription = pSubjectNode.SelectSingleNode("HovedgruppeStikord").InnerText;
                    mEmneNr = pSubjectNode.SelectSingleNode("HovedgruppeNr").InnerText;
                    SetKLEAdm(pSubjectNode);
                    break;

                case "StikordTilGruppe":
                    mDescription = pSubjectNode.SelectSingleNode("GruppeStikord").InnerText;
                    mEmneNr = pSubjectNode.SelectSingleNode("GruppeNr").InnerText;
                    SetKLEAdm(pSubjectNode);
                    break;

                case "StikordTilEmne":
                    mDescription = pSubjectNode.SelectSingleNode("EmneStikord").InnerText;
                    mEmneNr = pSubjectNode.SelectSingleNode("EmneNr").InnerText;
                    SetKLEAdm(pSubjectNode);
                    break;

                case "StikordTilHandlingsfacet":
                    mDescription = pSubjectNode.SelectSingleNode("HandlingsfacetStikord").InnerText;
                    mFacet = pSubjectNode.SelectSingleNode("HandlingsfacetNr").InnerText;
                    SetKLEAdm(pSubjectNode);
                    break;

                case "StikordTilEmneOgHandlingsfacet":
                    mDescription = pSubjectNode.SelectSingleNode("EmneOgHandlingsfacetStikord").InnerText;
                    mEmneNr = pSubjectNode.SelectSingleNode("EmneNr").InnerText;
                    mFacet = pSubjectNode.SelectSingleNode("HandlingsfacetNr").InnerText;
                    SetKLEAdm(pSubjectNode);
                    break;
            }

        }

        private void SetKLEAdm(XmlNode pSubjectNode)
        {
            string dateString = pSubjectNode.SelectSingleNode("StikordAdministrativInfo/OprettetDato").InnerText;
            if (!DateTime.TryParse(dateString, out mOprettet))
            {
                if (dateString.Length >= 10)
                    DateTime.TryParse(dateString.Substring(0, 10), out mOprettet);
            }

            if (pSubjectNode.SelectSingleNode("StikordAdministrativInfo/Historisk/UdgaaetDato") != null)
            {
                mUdgaaet = DateTime.Parse(pSubjectNode.SelectSingleNode("StikordAdministrativInfo/Historisk/UdgaaetDato").InnerText);
                mIsExpired = true;
            }
        }

        public string Description { get { return mDescription; } }
        public string EmneNr { get { return mEmneNr; } }
        public string Facet { get { return mFacet; } }
        public DateTime Oprettet { get { return mOprettet; } }
        public bool IsExpired { get { return mIsExpired; } }
    }
}
