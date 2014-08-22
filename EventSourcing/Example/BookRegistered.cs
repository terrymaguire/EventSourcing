using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Example
{
    public class BookRegistered
    {
        public readonly BookId Id;
        public readonly string Title;
        public readonly string Isbn;

        public BookRegistered(BookId id, string title, string isbn)
        {
            Id = id;
            Title = title;
            Isbn = isbn;
        }
    }
}
