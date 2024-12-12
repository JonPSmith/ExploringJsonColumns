// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.JsonBookClasses;

public class JsonBookData
{
    public string Title { get; set; }

    public string Description { get; set; }

    public DateOnly PublishedOn { get; set; }

    public string Publisher { get; set; }

    [Column(TypeName = "decimal(9,2)")]
    public decimal Price { get; set; }

    public string ImageUrl { get; set; }

    public List<JsonAuthor> Authors { get; set; }

    /// <summary>
    /// PriceOffer can be null
    /// </summary>
    public JsonPriceOffer Promotion { get; set; }

    /// <summary>
    /// Reviews can be null
    /// </summary>
    public List<JsonReview> Reviews { get; set; }

    //=================================================
    //Useful methods

    public double StarsValue() => Reviews == null || Reviews.Count == 0 
        ? 0 : Reviews.Sum(x => x.NumStars) / Reviews.Count;
}