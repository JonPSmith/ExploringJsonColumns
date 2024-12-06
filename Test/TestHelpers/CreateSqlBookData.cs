// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.SqlBookClasses;
using static System.Reflection.Metadata.BlobBuilder;

namespace Test.TestHelpers;

public class CreateSqlBookData
{
    public static readonly DateTime DummyBookStartDate = new DateTime(2010, 1, 1);

    public static List<Book> CreateDummyBooks(int numBooks = 10)
    {
        var result = new List<Book>();
        
        for (int i = 0; i < numBooks; i++)
        {
            var reviews = new List<Review>();
            for (int j = 0; j < i; j++)
            {
                reviews.Add(new Review { VoterName = j.ToString(), NumStars = (j % 5) + 1 });
            }
            var book = new Book
            {
                Title = $"Book{i:D4} Title",
                Description = $"Book{i:D4} Description",
                Price = (short)(i + 1),
                ImageUrl = $"Image{i:D4}",
                PublishedOn = DummyBookStartDate.AddYears(i),
                Reviews = reviews
            };

            book.Authors = new List<Author>
            {
                new Author {Name = $"Author{i:D4}", Books = new List<Book>{ book }},
                new Author {Name = "commonAuthor", Books = new List<Book>{ book }}
            };

            result.Add(book);
        }

        return result;
    }

    public static List<Book> CreateFourBooks()
    {
        var martinFowler = new Author
        {
            Name = "Martin Fowler"
        };

        var books = new List<Book>();

        var book1 = new Book
        {
            Title = "Refactoring",
            Description = "Improving the design of existing code",
            PublishedOn = new DateTime(1999, 7, 8),
            Price = 40
        };

        //Now provide the "Martin Fowler" Author entity to the two books 

        var book2 = new Book
        {
            Title = "Patterns of Enterprise Application Architecture",
            Description = "Written in direct response to the stiff challenges",
            PublishedOn = new DateTime(2002, 11, 15),
            Price = 53
        };

        book1.Authors = new List<Author>
        {
            new Author { Name = "Martin Fowler", Books = new List<Book> { book1, book2 } }
        };
        book2.Authors = book1.Authors;

        books.Add(book1);
        books.Add(book2);

        var book3 = new Book
        {
            Title = "Domain-Driven Design",
            Description = "Linking business needs to software design",
            PublishedOn = new DateTime(2003, 8, 30),
            Price = 56
        };
        book3.Authors = new List<Author>
        {
            new Author { Name = "Eric Evans", Books = new List<Book> { book3 } },
        };
        books.Add(book3);

        var book4 = new Book
        {
            Title = "Quantum Networking",
            Description = "Entangled quantum networking provides faster-than-light data communications",
            PublishedOn = new DateTime(2057, 1, 1),
            Price = 220
        };
        book4.Authors = new List<Author>
        {
            new Author { Name = "Future Person", Books = new List<Book> { book3 } },
        };
        book4.Reviews = new List<Review>
            {
                new Review { VoterName = "Jon P Smith", NumStars = 5, Comment = "I look forward to reading this book, if I am still alive!"},
                new Review { VoterName = "Albert Einstein", NumStars = 5, Comment = "I would write this book if I was still alive!"}
            };
        book4.Promotion = new PriceOffer { NewPrice = 219, PromotionalText = "Save $1 if you order 40 years ahead!" };
        books.Add(book4);

        return books;
    }
}