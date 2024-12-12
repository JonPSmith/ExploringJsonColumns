﻿// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using Azure;
using DataLayer.SqlBookClasses;

namespace Test.TestHelpers;

public static class CreateSqlBookData
{
    public static readonly DateOnly DummyBookStartDate = new DateOnly(2010, 1, 1);

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

            book.AuthorsLink = new List<BookAuthor>() 
            {
                new BookAuthor {Book = book, Author = new Author { Name = $"Author{i:D4}"}},
                new BookAuthor {Book = book, Author = new Author {Name = "commonAuthor"}},
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