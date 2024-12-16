// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.SqlBookClasses;

namespace Test.MappingCode;

public static class MapSqlBooksByAuthor
{
    public static List<string>
        MapBooksByAuthor(this IQueryable<Book> books, string authorName)
    {
        return books
            .Where(x => x.AuthorsLink.Any(y => y.Author.Name == authorName))
            .Select(x => x.Title)
            .ToList();
    }
}