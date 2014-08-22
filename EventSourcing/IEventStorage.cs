using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public interface IEventStorage : IDisposable
    {
        IAggregateRootStorage<TId>
            GetAggregateRootStorage<TAggregateRoot, TId>()
                where TAggregateRoot : AggregateRoot<TId>;
    }

   
}
