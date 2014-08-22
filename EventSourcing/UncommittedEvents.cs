using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace EventSourcing
{
    internal class UncommittedEvents : IUncommittedEvents
    {
        private readonly List<object> events = new List<object>();

        public void Append(object @event)
        {
            events.Add(@event);
        }

        public bool HasEvents
        {
            get { return events.Count != 0; }
        }

        public void Commit()
        {
            events.Clear();
        }

        public IEnumerator<object> GetEnumerator()
        {
            return events.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return events.GetEnumerator();
        }
    }
}
