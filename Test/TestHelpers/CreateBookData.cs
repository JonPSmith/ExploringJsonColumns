// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.SqlBookClasses;

namespace Test.TestHelpers;

public class CreateBookData
{
    public static readonly DateTime DummyBookStartDate = new DateTime(2010, 1, 1);

    public static List<Book> CreateDummyBooks(int numBooks = 10)
    {
        var result = new List<Book>();
        var commonAuthor = new Author { Name = "CommonAuthor" };
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

            var author = new Author { Name = $"Author{i:D4}" };
            book.AuthorsLink = new List<BookAuthor>
            {
                new BookAuthor {Book = book, Author = author, Order = 0},
                new BookAuthor {Book = book, Author = commonAuthor, Order = 1}
            };

            result.Add(book);
        }

        return result;
    }
}