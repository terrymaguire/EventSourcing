using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public class SessionFactory : ISessionFactory
    {
        private readonly IEventStorage eventStorage;

        public SessionFactory(IEventStorage eventStorage)
        {
            this.eventStorage = eventStorage;
        }

        public ISession OpenSession()
        {
            return new Session(eventStorage);
        }

        public void Dispose()
        {
            eventStorage.Dispose();
        }
    }
}
