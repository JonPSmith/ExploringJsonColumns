// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace DataLayer.SqlBookClasses
{
    public class BookAuthor
    {
        [Key]
        public int BookId { get; set; }
        [Key]
        public int AuthorId { get; set; }
        public byte Order { get; set; }

        //-----------------------------
        //Relationships

        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}