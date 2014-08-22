using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public class Session : ISession
    {
        public readonly IEventStorage eventStorage;
        private readonly HashSet<ISessionItem> enlistedItems =
            new HashSet<ISessionItem>();

        [ThreadStatic]
        private static Session current;

        internal Session(IEventStorage eventStorage)
        {
            this.eventStorage = eventStorage;
            if (current != null)
                throw new InvalidOperationException("Cannot nest unit of work");

            current = this;
        }

        private static Session Current
        {
            get { return current; }
        }

        public void SubmitChanges()
        {
            foreach (var enlisted in enlistedItems)
                enlisted.SubmitChanges();

            enlistedItems.Clear();
        }

        public void Dispose()
        {
            current = null;
        }

        internal static IAggregateRootStorage<TId>
            Enlist<TId, TAggregateRoot>
            (Repository<TId, TAggregateRoot> repository)
            where TAggregateRoot : AggregateRoot<TId>
            {
                var unitOfWork = Current;
                unitOfWork.enlistedItems.Add(repository);

                return unitOfWork.eventStorage.GetAggregateRootStorage<TAggregateRoot, TId>();

            }
    }
}
