// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.JsonBookClasses;
using Test.Dtos;

namespace Test.MappingCode;

public static class MapJsonBooksByAuthor
{
    public static List<string>
        MapBooksByAuthor(this IQueryable<BookTop> books, string authorName)
    {
        return books
            .Where(x => x.BookData.Authors.Any(y => y.AuthorName == authorName))
            .Select(top => top.BookData.Title)
            .ToList();
    }
}