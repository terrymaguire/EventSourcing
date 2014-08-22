using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public interface IAggregateRootStorage<in TId>
    {
        void Append(TId id, IEnumerable<object> events);
        IEnumerable<object> this[TId id] { get; }
    }
}
