// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.SqlBookClasses;

public class Book
{
    public int BookId { get; set; }

    [Required] //#A
    [MaxLength(256)] 
    public string Title { get; set; }

    public string Description { get; set; }

    public DateOnly PublishedOn { get; set; }

    [MaxLength(64)] 
    public string Publisher { get; set; }

    [Column(TypeName = "decimal(9,2)")]
    public decimal Price { get; set; }

    [MaxLength(512)] 
    public string ImageUrl { get; set; }

    //-----------------------------------------------
    //relationships

    public PriceOffer Promotion { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<BookAuthor> AuthorsLink { get; set; }
}