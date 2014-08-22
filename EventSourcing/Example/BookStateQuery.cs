using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Example
{
    class BookStateQuery : IBookStateQuery
    {
        private readonly Dictionary<BookId, BookState> states =
                     new Dictionary<BookId, BookState>();

        public IEnumerable<BookState> GetBookStates()
        {
            return states.Values;
        }

        public BookState GetBookState(BookId id)
        {
            return states[id];
        }

        public IEnumerable<BookState> GetLentBooks()
        {
            return states.Values.Where(b => b.Lent);
        }

        public void AddBookState(BookId id, string title)
        {
            var state = new BookState { Id = id, Title = title };
            states.Add(id, state);
        }

        public void SetLent(BookId id, bool lent)
        {
            states[id].Lent = lent;
        }
    }
}
