using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public static class DomainEvents
    {
        [ThreadStatic]
        private static List<Delegate> actions;

        private static List<Handler> handlers;

        public static void Register<T>(Action<T> callback)
        {
            if (actions == null) actions = new List<Delegate>();
            actions.Add(callback);
        }

        public static void RegisterHandler<T>(Func<T> factory)
        {
            if (handlers == null) handlers = new List<Handler>();
            handlers.Add(new Handler<T>(factory));
        }

        public static void Raise<T>(T @event)
        {
            if (actions != null)
                foreach(Delegate action in actions)
                    if (action is Action<T>)
                        ((Action<T>) action)(@event);


            if (handlers != null)
                foreach (var h in handlers)
                {
                    if (h.Handles<T>())
                    {
                        var handler = h.CreateInstance<T>();
                        handler.Handle(@event);
                    }
                }
        }

        private abstract class Handler
        {
            public abstract bool Handles<E>();
            public abstract Handles<E> CreateInstance<E>();
        }

        private class Handler<T> : Handler
        {
            private readonly Func<T> factory;

            public Handler(Func<T> factory)
            {
                this.factory = factory;
            }

            public override bool Handles<E>()
            {
                return typeof(Handles<E>)
                    .IsAssignableFrom(typeof(T));
            }

            public override Handles<E> CreateInstance<E>()
            {
                return (Handles<E>)factory();
            }
        }
    }
}
