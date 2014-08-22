using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Example
{
    public class BookLent
    {
        public readonly BookId Id;
        public readonly string Borrower;
        public readonly DateTime Date;
        public readonly TimeSpan ExpectedDuration;

        public BookLent(BookId id, string borrower, DateTime date, TimeSpan expectedDuration)
        {
            Id = id;
            Borrower = borrower;
            Date = date;
            ExpectedDuration = expectedDuration;
        }
    }
}
