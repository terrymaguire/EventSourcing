using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Example
{
    public class Book : AggregateRoot<BookId>
    {
        private readonly BookId id;
        private string title;
        private string isbn;
        private string borrower;
        private DateTime date;
        private TimeSpan expectedDuration;

        public Book(BookId id, IEnumerable<object> events)
        {
            this.id = id;
            foreach (dynamic @event in events)
                Apply(@event);
        }

        public Book(BookId id, string title, string isbn)
        {
            this.id = id;
            var @event = new BookRegistered(id, title, isbn);
            Apply(@event);
            Append(@event);
        }

        public override BookId Id { get { return id;  } }

        public void Lend(string borrower, DateTime date, TimeSpan expectedDuration)
        {
            if (this.borrower != null)
                throw new InvalidOperationException("The book is already lent.");

            var @event = new BookLent(id, borrower, date, expectedDuration);
            Apply(@event);
            Append(@event);
        }

        public void Return(DateTime returnDate)
        {
            if (borrower == null)
                throw new InvalidOperationException("The book has not been lent.");
            if (returnDate < date)
                throw new ArgumentException(
                  "The book cannot be returned before being lent.");
            var actualDuration = returnDate - date;
            var @event = new BookReturned(
                           id,
                           borrower,
                           actualDuration,
                           actualDuration > expectedDuration);
            Apply(@event);
            Append(@event);
        }

        private void Apply(BookRegistered @event)
        {
            title = @event.Title;
            isbn = @event.Isbn;
        }

        private void Apply(BookLent @event)
        {
            borrower = @event.Borrower;
            date = @event.Date;
            expectedDuration = @event.ExpectedDuration;
        }

        private void Apply(BookReturned @event)
        {
            borrower = null;
        }

    }
}
