using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Example
{
    public class BookState
    {
        public BookId Id { get; set; }
        public string Title { get; set; }
        public bool Lent { get; set; }
    }
}
