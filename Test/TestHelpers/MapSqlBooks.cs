// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.SqlBookClasses;

namespace Test.TestHelpers;

public static class MapSqlBooks
{
    public static IQueryable<BookListDto>
        MapBookToDto(this IQueryable<Book> books)
    {
        return books.Select(book => new BookListDto
        {
            BookId = book.BookId,
            Title = book.Title,
            Price = book.Price,
            PublishedOn = book.PublishedOn,
            ActualPrice = book.Promotion == null
                ? book.Price
                : book.Promotion.NewPrice,
            PromotionPromotionalText =
                book.Promotion == null
                    ? null
                    : book.Promotion.PromotionalText,
            AuthorsOrdered = string.Join(", ",
                book.AuthorsLink
                    .OrderBy(ba => ba.Order)
                    .Select(ba => ba.Author.Name)),
            ReviewsCount = book.Reviews.Count,
            ReviewsAverageVotes =
                book.Reviews.Select(review =>
                    (double?)review.NumStars).Average(),
        });
    }

}