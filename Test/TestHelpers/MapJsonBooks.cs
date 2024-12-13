// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.JsonBookClasses;


namespace Test.TestHelpers;

public static class MapJsonBooks
{
    public static IEnumerable<BookListDto>
        MapBookTopToDto(this IEnumerable<BookTop> books)
    {
        foreach (var bookTop in books)
        {
            yield return new BookListDto
            {
                BookId = bookTop.Id,
                Title = bookTop.BookData.Title,
                Price = bookTop.BookData.Price,
                PublishedOn = bookTop.BookData.PublishedOn,
                ActualPrice = bookTop.BookData.Promotion?.NewPrice ?? bookTop.BookData.Price,
                PromotionPromotionalText =
                    bookTop.BookData.Promotion?.PromotionalText,
                AuthorsOrdered = string.Join(", ", bookTop.BookData.Authors
                    .Select(x => x.AuthorName)),
                ReviewsCount = bookTop.BookData.Reviews?.Count ?? 0,
                ReviewsAverageVotes = (bookTop.BookData.Reviews == null || !bookTop.BookData.Reviews.Any()
                    ? null
                    : bookTop.BookData.Reviews.Average(x => x.NumStars))
            };
        }
    }
}