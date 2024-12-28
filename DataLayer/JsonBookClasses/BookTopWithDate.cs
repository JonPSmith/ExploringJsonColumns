// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

namespace DataLayer.JsonBookClasses;

public class BookTopWithDate
{
    public int Id { get; set; }

    public DateOnly PublishedOn { get; set; }

    public JsonBookData BookData { get; set; }
}