// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.SqlBookClasses;

namespace Test.TestHelpers;

public static class CreateSqlBookData
{
    private static readonly DateOnly DummyBookStartDate = new DateOnly(2010, 1, 1);

    /// <summary>
    /// Creates dummy books
    /// </summary>
    /// <param name="numBooks">number of books to return</param>
    /// <param name="maxReviews">The number of reviews goes to 0 to maxReviews before starting again.
    /// If 0 then no Reviews added</param>
    /// <param name="commonAuthorRatio">This creates a CommonAuthor every numBooks / commonAuthorRatio</param>
    /// <param name="promotionEvery">A Promotion will be added every this value. If 0 then no Promotions added</param>
    /// <returns></returns>
    public static List<Book> CreateSqlDummyBooks(int numBooks, 
        int maxReviews = 5, int commonAuthorRatio = 10, int promotionEvery = 10)
    {
        var result = new List<Book>();

        for (int i = 0; i < numBooks; i++)
        {
            var reviews = new List<Review>();
            if (maxReviews != 0)
                for (int j = 0; j < (i % maxReviews) ; j++)
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
                Reviews = reviews,
            };
            //Adds a Promotion every "promotionEvery"
            if (promotionEvery != 0 && (i % promotionEvery) == 0)
                book.Promotion = new PriceOffer
                {
                    NewPrice = book.Price / ((decimal)2.0),
                    PromotionalText = "half price today!",
                };
            
            var bookAuthors = new List<BookAuthor> 
                { new BookAuthor { Book = book, Author = new Author { Name = $"Author{i:D4}" } } };
            if (commonAuthorRatio != 0)
            {
                bookAuthors.Add(new BookAuthor { Book = book, Author = 
                        new Author { Name = $"CommonAuthor{(i / commonAuthorRatio):D4}"}});
            }
            book.AuthorsLink = bookAuthors;

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
            PublishedOn = new DateOnly(1999, 7, 8),
            Price = 40,
        };
        book1.AuthorsLink = new List<BookAuthor> { new BookAuthor { Author = martinFowler, Book = book1 } };
        books.Add(book1);

        var book2 = new Book
        {
            Title = "Patterns of Enterprise Application Architecture",
            Description = "Written in direct response to the stiff challenges",
            PublishedOn = new DateOnly(2002, 11, 15),
            Price = 53,
        };
        book2.AuthorsLink = new List<BookAuthor> { new BookAuthor { Author = martinFowler, Book = book2 } };
        books.Add(book2);

        var book3 = new Book
        {
            Title = "Domain-Driven Design",
            Description = "Linking business needs to software design",
            PublishedOn = new DateOnly(2003, 8, 30),
            Price = 56,
        };
        book3.AuthorsLink = new List<BookAuthor>
                {new BookAuthor {Author = new Author {Name = "Eric Evans"}, Book = book3}};
        books.Add(book3);

        var book4 = new Book
        {
            Title = "Quantum Networking",
            Description = "Entangled quantum networking provides faster-than-light data communications",
            PublishedOn = new DateOnly(2057, 1, 1),
            Price = 220,
        };
        book4.AuthorsLink = new List<BookAuthor>
                {new BookAuthor {Author = new Author {Name = "Future Person"}, Book = book4}};
        book4.Reviews = new List<Review>
            {
                new Review
                {
                    VoterName = "Jon P Smith", NumStars = 5,
                    Comment = "I look forward to reading this book, if I am still alive!"
                },
                new Review
                {
                    VoterName = "Albert Einstein", NumStars = 5, Comment = "I write this book if I was still alive!"
                }
            };
        book4.Promotion = new PriceOffer { NewPrice = 219, PromotionalText = "Save $1 if you order 40 years ahead!" };
        books.Add(book4);

        return books;
    }
}