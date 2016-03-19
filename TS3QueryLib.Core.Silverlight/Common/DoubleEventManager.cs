using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace TS3QueryLib.Core.Common
{
    public class DoubleEvent
    {
        public DateTime Added = DateTime.Now;
        public string Value;

        public DoubleEvent(string eventValue)
        {
            Value = eventValue;
        }
    }

    class DoubleEventManager
    {
        private List<DoubleEvent> eventDumpStrings = new List<DoubleEvent>();

        public bool IsFirst(string eventValue, bool tidyEvents = true)
        {
            if (tidyEvents)
                TidyEvents();

            eventDumpStrings.Add(new DoubleEvent(eventValue));
            if (eventDumpStrings.Count(item => item.Value == eventValue) == 2)
            {
                eventDumpStrings.RemoveAll(item => item.Value == eventValue);
                return false;
            }
            if (eventDumpStrings.Count(item => item.Value == eventValue && item.Added.CompareTo(DateTime.Now) < -1) == 1)
            {
                eventDumpStrings.RemoveAll(item => item.Value == eventValue);
                return false;
            }
            else
            {
                return true;
            }
        }

        public void TidyEvents(TimeSpan? threshold = null)
        {
            if (threshold == null) { threshold = TimeSpan.FromMilliseconds(500); }
            eventDumpStrings.RemoveAll(e => (DateTime.Now - e.Added) > threshold);
        }
    }
}
