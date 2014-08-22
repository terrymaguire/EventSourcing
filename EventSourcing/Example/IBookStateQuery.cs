using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Example
{
    public interface IBookStateQuery
    {
        IEnumerable<BookState> GetBookStates();
        BookState GetBookState(BookId id);
        IEnumerable<BookState> GetLentBooks();

        void AddBookState(BookId id, string title);
        void SetLent(BookId id, bool lent);
    }
}
