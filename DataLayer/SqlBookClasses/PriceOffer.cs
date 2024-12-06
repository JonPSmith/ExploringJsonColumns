// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.SqlBookClasses
{
    public class PriceOffer
    {
        public const int PromotionalTextLength = 200;

        public int PriceOfferId { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal NewPrice { get; set; }

        [Required]
        [MaxLength(PromotionalTextLength)]
        public string PromotionalText { get; set; }

        //-----------------------------------------------
        //Relationships

        public int BookId { get; set; }
    }
}