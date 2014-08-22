using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Example
{
    class LateReturnNotifier :
   Handles<BookReturned>
    {
        public void Handle(BookReturned @event)
        {
            if (@event.Late)
            {
                Console.WriteLine("{0} was late", @event.By);
            }
        }
    }
}
