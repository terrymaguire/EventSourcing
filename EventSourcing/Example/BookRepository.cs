using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Example
{
    internal class BookRepository: Repository<BookId, Book>, IBookRepository
    {
        protected override Book CreateInstance(BookId id, IEnumerable<object> events)
        {
            return new Book(id, events);
        }
    }
}
