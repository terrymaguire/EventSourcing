using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Example
{
    public struct BookId : IEquatable<BookId>
    {
        private Guid id;

        private BookId(Guid id) { this.id = id; }

        public static BookId NewBookId() { return new BookId(Guid.NewGuid()); }

        public bool Equals(BookId other) { return other.id.Equals(id); }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof(BookId)) return false;
            return Equals((BookId)obj);
        }

        public override int GetHashCode() { return id.GetHashCode(); }
    }
}
