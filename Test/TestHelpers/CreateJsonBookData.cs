// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.JsonBookClasses;
using DataLayer.SqlBookClasses;

namespace Test.TestHelpers;

public static class CreateJsonBookData
{
    private static readonly int startYear = 2010 ;

    /// <summary>
    /// This converts a Sql Book and its supporting links.
    /// NOTE: The <see cref="List&lt;Book&gt;"/> must have the other entities 
    /// </summary>
    /// <param name="slqBooks"></param>
    /// <returns></returns>
    public static List<BookTop> ConvertSqlBookToJsonBook(this List<Book> slqBooks)
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
                Authors =  slqBook.AuthorsLink
                    .OrderBy(ba => ba.Order)
                    .Select(ba => new JsonAuthor{AuthorName = ba.Author.Name}).ToList()
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
                bookData.Promotion = new JsonPriceOffer
                {
                    NewPrice = slqBook.Promotion.NewPrice,
                    PromotionalText = slqBook.Promotion.PromotionalText
                };
            }

            result.Add(new BookTop{BookData = bookData});
        }

        return result;
    }

    /// <summary>
    /// Creates dummy books
    /// </summary>
    /// <param name="numBooks">number of books to return</param>
    /// <param name="commonAuthorRatio">This creates a CommonAuthor every numBooks / commonAuthorRatio</param>
    /// <returns></returns>
    public static List<BookTop> CreateJsonDummyBooks(int numBooks, int commonAuthorRatio = 10)
    {
        return CreateSqlBookData.CreateSqlDummyBooks(numBooks, commonAuthorRatio).ConvertSqlBookToJsonBook();
    }

}