using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Example
{
    public class BookReturned
    {
        public readonly BookId Id;
        public readonly string By;
        public readonly TimeSpan After;
        public readonly bool Late;

        public BookReturned(BookId id, string @by, TimeSpan after,
             bool late)
        {
            Id = id;
            By = by;
            After = after;
            Late = late;
        }
    }
}
