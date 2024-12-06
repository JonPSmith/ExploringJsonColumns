// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.JsonBookClasses;
using DataLayer.SqlBookClasses;

namespace Test.TestHelpers;

public class CreateJsonBookData
{
    private static readonly int startYear = 2010 ;

    /// <summary>
    /// This converts a Sql Book and its supporting links.
    /// NOTE: The <see cref="List&lt;Book&gt;"/> must have the other entities 
    /// </summary>
    /// <param name="slqBooks"></param>
    /// <returns></returns>
    public static List<BookTop> ConvertSqlBookToJsonBook(List<Book> slqBooks)
    {
        var result = new List<BookTop>();
        foreach (var slqBook in slqBooks)
        {
            //Copy over the simple properties
            var bookData = new JsonBookData
            {
                Title = slqBook.Title,
                Description = slqBook.Description,
                Price = slqBook.Price,
                ImageUrl = slqBook.ImageUrl,
                PublishedOn = slqBook.PublishedOn,
                Authors = slqBook.Authors.Select(x => new JsonAuthor{AuthorName = x.Name}).ToList(),
                

            };
            if (slqBook.Reviews != null)
            {
                bookData.Reviews = slqBook.Reviews.Select(x => new JsonReview
                    {
                        VoterName = x.VoterName,
                        NumStars = x.NumStars,
                        Comment = x.Comment
                    }
                ).ToList();
            }
            if (slqBook.Promotion != null)
            {
                bookData.PriceOffer = new JsonPriceOffer
                {
                    NewPrice = slqBook.Promotion.NewPrice,
                    PromotionalText = slqBook.Promotion.PromotionalText
                };
            }

            result.Add(new BookTop{BookData = bookData});
        }

        return result;
    }


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
                PublishedOn = new DateOnly(startYear + i, 1,1),
                Reviews = reviews,
                Authors = authors
            };

            result.Add(new BookTop{BookData = bookData});
        }

        return result;
    }
}