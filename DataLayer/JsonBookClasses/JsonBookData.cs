// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.SqlBookClasses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using static Azure.Core.HttpHeader;

namespace DataLayer.JsonBookClasses;

public class JsonBookData
{
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime PublishedOn { get; set; }

    public string Publisher { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Price { get; set; }

    public string ImageUrl { get; set; }

    /// <summary>
    /// PriceOffer can be null
    /// </summary>
    public JsonPriceOffer PriceOffer { get; set; }

    public List<JsonAuthor> Authors { get; set; }

    public List<JsonReview> Reviews { get; set; }

    //-----------------------------------------------------
    //Useful for testing

    private string GetOfferPrice()
    {
        return PriceOffer != null ? $", but on sale at {PriceOffer.NewPrice}" : "";
    }

    public override string ToString()
    {
        return $"Title: {Title}: " +
               $"Price: {Price} {GetOfferPrice()} " +
               $"Authors: {String.Join(", ", Authors.ToArray().SelectMany(x => x.AuthorName))}, " +
               $"Review stars: {(Reviews.Count > 0 ? Reviews.Average(x => x.NumStars) : 0)} ";
    }
}