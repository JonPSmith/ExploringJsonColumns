// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.SqlBookClasses;

namespace DataLayer.JsonBookClasses;

public class BookTop
{
    public int Id { get; set; }

    public JsonBookData BookData { get; set; }

    //-----------------------------------------------------
    //Useful for testing

    private string GetOfferPrice()
    {
        return BookData.PriceOffer != null ? $", but on sale at {BookData.PriceOffer.NewPrice}" : "";
    }

    public override string ToString()
    {
        string result = $"Title: {BookData.Title}, " +
                        $"Price: {BookData.Price}{GetOfferPrice()}, " +
                        $"Authors: {String.Join(", ", BookData.Authors.Select(x => x.AuthorName))}, " + 
                        $"Review stars: {
                            (BookData.Reviews == null || !BookData.Reviews.Any() ? "No reviews" 
                                : BookData.Reviews.Average(x => x.NumStars).ToString("F"))}, " +
                        $"ImageUrl {BookData.ImageUrl}";
        return result;
    }
}