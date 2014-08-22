using EventSourcing.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    class Program
    {
        static void Main(string[] args)
        {
            ISessionFactory factory = new SessionFactory(new EventStorage()); 
            IBookStateQuery query = new BookStateQuery();

            DomainEvents.RegisterHandler(() => new BookStateHandler(query));
            DomainEvents.RegisterHandler(() => new LateReturnNotifier());

            var bookId = BookId.NewBookId();
            using (var session = factory.OpenSession())
            {
                var books = new BookRepository();
                books.Add(new Book(bookId,
                   "The Lord of the Rings",
                   "0-618-15396-9"));
                session.SubmitChanges();
            }

            ShowBooks(query);

            using (var session = factory.OpenSession())
            {
                var books = new BookRepository();
                var book = books[bookId];
                book.Lend("Alice",
                     new DateTime(2009, 11, 2),
                     TimeSpan.FromDays(14));

                session.SubmitChanges();
            }

            ShowBooks(query);


            using (var session = factory.OpenSession())
            {
                var books = new BookRepository();
                var book = books[bookId];
                book.Return(new DateTime(2009, 11, 8));

                session.SubmitChanges();
            }

            ShowBooks(query);


            using (var session = factory.OpenSession())
            {
                var books = new BookRepository();
                var book = books[bookId];
                book.Lend("Bob",
                      new DateTime(2009, 11, 9),
                      TimeSpan.FromDays(14));

                session.SubmitChanges();
            }

            ShowBooks(query);


            using (var session = factory.OpenSession())
            {
                var books = new BookRepository();
                var book = books[bookId];
                book.Return(new DateTime(2010, 03, 1));
                session.SubmitChanges();
            }

            ShowBooks(query);

            Console.ReadLine();
        }

        private static void ShowBooks(IBookStateQuery query)
        {
            foreach (var state in query.GetBookStates())
                Console.WriteLine("{0} is {1}.",
                       state.Title,
                       state.Lent ? "lent" : "home");
        }
    }
}
