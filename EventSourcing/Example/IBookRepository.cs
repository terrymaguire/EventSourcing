using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Example
{
    public interface IBookRepository
    {
        void Add(Book book);
        Book this[BookId id] { get; }
    }
}
