// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.JsonBookClasses;

namespace Test.TestHelpers;

public class CreateJsonBookData
{
    public static readonly DateTime DummyBookStartDate = new DateTime(2010, 1, 1);

    public static List<BookTop> CreateDummyBooks(int numBooks = 10)
    {
        var result = new List<BookTop>();
        var commonAuthor = "CommonAuthor";
        for (int i = 0; i < numBooks; i++)
        {
            var reviews = new List<JsonReview>();
            for (int j = 0; j < i; j++)
            {
                reviews.Add(new JsonReview { VoterName = j.ToString(), NumStars = (j % 5) + 1 });
            }

            var authors = new List<JsonAuthor>
            {
                new JsonAuthor { AuthorName = $"Author{i:D4}" },
                new JsonAuthor { AuthorName = commonAuthor }
            };
            var bookData = new JsonBookData
            {
                Title = $"Book{i:D4} Title",
                Description = $"Book{i:D4} Description",
                Price = (short)(i + 1),
                ImageUrl = $"Image{i:D4}",
                PublishedOn = DummyBookStartDate.AddYears(i),
                Reviews = reviews,
                Authors = authors
            };

            result.Add(new BookTop{BookData = bookData});
        }

        return result;
    }
}