// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

namespace Test.TestHelpers;

public class BookListDto
{
    public int BookId { get; set; } 
    public string Title { get; set; }
    public DateOnly PublishedOn { get; set; }
    public decimal Price { get; set; }
    public decimal ActualPrice { get; set; }
    public string PromotionPromotionalText { get; set; }
    public string AuthorsOrdered { get; set; }
    public int ReviewsCount { get; set; }
    public double? ReviewsAverageVotes { get; set; }

    // public override string ToString()
    // {
    //     string result = $"Title: {Title}, " +
    //                     $"Price: {Price} " +
    //                     $"Authors: {String.Join(", ", BookData.Authors.Select(x => x.AuthorName))}, " +
    //                     $"Review stars: {(BookData.Reviews == null || !BookData.Reviews.Any() ? "No reviews"
    //                         : BookData.Reviews.Average(x => x.NumStars).ToString("F"))} ";
    //     return result;
    // }
}
