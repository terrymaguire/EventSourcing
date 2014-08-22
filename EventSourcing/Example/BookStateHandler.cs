using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Example
{
    class BookStateHandler :
    Handles<BookRegistered>,
    Handles<BookLent>,
    Handles<BookReturned>
    {
        private readonly IBookStateQuery stateQuery;

        public BookStateHandler(IBookStateQuery stateQuery)
        {
            this.stateQuery = stateQuery;
        }

        public void Handle(BookRegistered @event)
        {
            stateQuery.AddBookState(@event.Id, @event.Title);
        }


        public void Handle(BookLent @event)
        {
            Console.WriteLine("Book lent to {0}", @event.Borrower);
            stateQuery.SetLent(@event.Id, true);
        }

        public void Handle(BookReturned @event)
        {
            Console.WriteLine("Book returned by {0}", @event.By);
            stateQuery.SetLent(@event.Id, false);
        }
    }
}
