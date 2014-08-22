﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public abstract class Repository<TId, TAggregateRoot> : 
        ISessionItem where TAggregateRoot : AggregateRoot<TId>
    {
        private readonly Dictionary<TId, TAggregateRoot> users =
            new Dictionary<TId, TAggregateRoot>();

        private readonly IAggregateRootStorage<TId>
            aggregateRootStorage;

        protected Repository()
        {
            aggregateRootStorage = Session.Enlist(this);
        }

        public void Add(TAggregateRoot user)
        {
            users.Add(user.Id, user);
        }

        public TAggregateRoot this[TId id]
        {
            get { return Find(id) ?? Load(id); }
        }

        private TAggregateRoot Find(TId id)
        {
            TAggregateRoot user;
            return users.TryGetValue(id, out user) ? user : null;
        }

        private TAggregateRoot Load(TId id)
        {
            var events = aggregateRootStorage[id];
            var user = CreateInstance(id, events);

            users.Add(id, user);

            return user;
        }

        protected abstract TAggregateRoot CreateInstance(TId id,
            IEnumerable<object> events);



        public void SubmitChanges()
        {
            foreach (IAggregateRoot<TId> user in users.Values)
            {
                var uncommittedEvents = user.UncommittedEvents;
                if (uncommittedEvents.HasEvents)
                {
                    aggregateRootStorage.Append(user.Id, uncommittedEvents);
                    PublishEvents(uncommittedEvents);
                    uncommittedEvents.Commit();
                }
            }
            users.Clear();
        }

        protected void PublishEvents(IUncommittedEvents uncommittedEvents)
        {
            foreach (dynamic @event in uncommittedEvents)
                DomainEvents.Raise(@event);
        }
    }
}
