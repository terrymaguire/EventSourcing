using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public interface IAggregateRoot<out TId>
    {
        TId Id { get; }
        IUncommittedEvents UncommittedEvents { get; }
    }
}
