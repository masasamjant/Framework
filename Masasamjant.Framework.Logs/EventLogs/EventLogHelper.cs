using System;
using System.Collections.Generic;
using System.Text;

namespace Masasamjant.Diagnostics.EventLogs
{
    public static class EventLogHelper
    {
        public static void TryWriteEntry(this IEventLog log, EventLogEntry entry)
        {
            try
            {
                log.WriteEntry(entry);
            }
            catch (Exception)
            {
                return;
            }
        }

        public static void TryWriteEntry(this IEventLog log, string source, string message, EventLogEntryLevel level)
        {
            TryWriteEntry(log, new EventLogEntry(source, message, level));
        }
    }
}
