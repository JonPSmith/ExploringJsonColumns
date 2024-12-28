// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.JsonBookClasses;

namespace Test.MappingCode;

public static class MapJsonBooksByDates
{
    public static List<BookTop> SelectBooksByPublishedOn(this IQueryable<BookTop> books)
    {
        return books.Where(x => 
                x.BookData.PublishedOn >= new DateOnly(2024, 12, 4) &&
                x.BookData.PublishedOn <= new DateOnly(2024, 12, 6))
            .ToList();
    }
}