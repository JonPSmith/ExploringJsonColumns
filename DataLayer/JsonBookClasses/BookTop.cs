// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.


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
        return $"Title: {BookData.Title}: " +
        $"Price: {BookData.Price} {GetOfferPrice()} " +
               $"Authors: {String.Join(", ", BookData.Authors.ToArray().SelectMany(x => x.AuthorName))}, " +
               $"Review stars: {(BookData.Reviews.Count > 0 ? BookData.Reviews.Average(x => x.NumStars) : 0)} ";
    }
}