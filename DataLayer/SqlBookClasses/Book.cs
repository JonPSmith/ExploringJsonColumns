﻿// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace DataLayer.SqlBookClasses
{
    public class Book
    {
        public int BookId { get; set; }

        [Required] //#A
        [MaxLength(256)] //#B
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }

        [MaxLength(64)] //#B
        public string Publisher { get; set; }

        public decimal Price { get; set; }

        [MaxLength(512)] //#B
        public string ImageUrl { get; set; }

        public bool SoftDeleted { get; set; }

        //-----------------------------------------------
        //relationships

        public PriceOffer Promotion { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public ICollection<BookAuthor>
            AuthorsLink
        { get; set; }
    }
    /****************************************************
    #A This tells EF Core that the string is non-nullable.
    #B The [MaxLength] attibute defines the the size of the string column in the database
     * **************************************************/
}