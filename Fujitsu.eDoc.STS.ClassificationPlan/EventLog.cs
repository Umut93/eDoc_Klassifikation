namespace Fujitsu.eDoc.STS.ClassificationPlan
{
    public class EventLog
    {
        public static void LogToEventLog(string message, System.Diagnostics.EventLogEntryType type)
        {
            Core.Common.SimpleEventLogging(typeof(EventLog).Namespace, "KLE", message, type);
        }
    }
}
