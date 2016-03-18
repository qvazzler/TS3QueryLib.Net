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
            //if (eventDumpStrings.Count(item => String.Join(" ", item) == String.Join(" ", eventDumpString)) == 2)
            eventDumpStrings.Add(new DoubleEvent(eventValue));
            if (eventDumpStrings.Count(item => item.Value == eventValue) == 2)
            {
                eventDumpStrings.RemoveAll(item => item.Value == eventValue);
                return false;
            }
            if (tidyEvents && eventDumpStrings.Count(item => item.Value == eventValue && item.Added.CompareTo(DateTime.Now) < -10) == 1)
            {
                eventDumpStrings.RemoveAll(item => item.Value == eventValue);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
