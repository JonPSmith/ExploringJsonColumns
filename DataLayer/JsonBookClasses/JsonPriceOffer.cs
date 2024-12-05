// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.JsonBookClasses;

public class JsonPriceOffer
{
    [Column(TypeName = "decimal(9,2)")]
    public decimal NewPrice { get; set; }

    public string PromotionalText { get; set; }
}