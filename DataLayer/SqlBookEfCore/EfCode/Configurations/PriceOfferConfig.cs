// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.BookClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.SqlBookEfCore.EfCode.Configurations
{
    public class PriceOfferConfig : IEntityTypeConfiguration<PriceOffer>
    {
        public void Configure
            (EntityTypeBuilder<PriceOffer> entity)
        {
            entity.Property(p => p.NewPrice)
                .HasColumnType("decimal(9,2)");
        }
    }
}