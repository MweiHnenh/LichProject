using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public interface IEventContainer
    {
        void AddEvent(string date, string eventName);
        string GetEvent(string date);
    }

}
