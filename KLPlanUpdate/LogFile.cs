using System;
using System.IO;
using System.Reflection;

namespace Fujitsu.Tools.KLPlanUpdate
{
    internal class LogFile
    {
        private string mFilename = "";

        internal LogFile(int pUserRecno, int pArchive, int pArchivePeriod, int pFacetClassType)
        {
            mFilename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Updatelog " + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".log";
            WriteToFile("LogFile created");
            WriteToFile("UserRecno=" + pUserRecno.ToString());
            WriteToFile("Archive=" + pArchive.ToString());
            WriteToFile("ArchivePeriod=" + pArchivePeriod.ToString());
            WriteToFile("FacetClassType=" + pFacetClassType.ToString());
            WriteToFile("--------------------------------------");
        }

        internal void LogStartUpdate()
        {
            WriteToFile("Update started " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
        }

        internal void LogFinishUpdate()
        {
            WriteToFile("Update finished " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
        }

        internal void LogException(Exception pException)
        {
            WriteToFile("--------------------------------------");
            WriteToFile("Exception:");
            WriteToFile("Message: " + pException.Message);
            WriteToFile("Source: " + pException.Source);
            WriteToFile("Stacktrace: " + pException.StackTrace);
            WriteToFile("--------------------------------------");

        }

        internal void LogMainGroup(MainGroup pMainGroup, string pAction)
        {
            WriteToFile(pAction + " MainGroup Subarchive=" + pMainGroup.Recno.ToString() + " Classtype=" + pMainGroup.ClassTypeRecno.ToString() + " Code=" + pMainGroup.HovedgruppeNr);
        }

        internal void LogGroup(Group pGroup, string pAction)
        {
            WriteToFile(pAction + " Group Classcode=" + pGroup.Recno.ToString() + " Classtype=" + pGroup.SecondaryClassTypeRecno.ToString() + " PrimaryClasstype=" + pGroup.PrimaryClassTypeRecno.ToString() + " Code=" + pGroup.GruppeNr);
        }

        internal void LogSubGroup(SubGroup pSubGroup, string pAction)
        {
            WriteToFile(pAction + " SubGroup Classcode=" + pSubGroup.Recno.ToString() + " Code=" + pSubGroup.EmneNr);
        }

        internal void LogFacetCategory(FacetCategory pFacetCategory, string pAction)
        {
            WriteToFile(pAction + " FacetCategory Classcode=" + pFacetCategory.Recno.ToString() + " Code=" + pFacetCategory.HandlingsfacetKategoriKode);
        }

        internal void LogFacet(Facet pFacet, string pAction)
        {
            WriteToFile(pAction + " Facet Classcode=" + pFacet.Recno.ToString() + " Code=" + pFacet.HandlingsfacetKode);
        }

        internal void LogStartCreateSubjects(int pCount)
        {
            WriteToFile("Start creating subjects. Count=" + pCount.ToString());
        }

        internal void LogFinishCreateSubjects()
        {
            WriteToFile("Finish creating subjects.");
        }

        private void WriteToFile(string pText)
        {
            StreamWriter sr = File.AppendText(mFilename);
            sr.WriteLine(pText);
            sr.Close();
        }

    }
}
