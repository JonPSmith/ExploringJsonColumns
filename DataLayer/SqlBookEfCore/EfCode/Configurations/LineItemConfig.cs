﻿// Copyright (c) 2024 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.SqlBookClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.SqlBookEfCore.EfCode.Configurations
{
    public class LineItemConfig : IEntityTypeConfiguration<LineItem>
    {
        public void Configure
            (EntityTypeBuilder<LineItem> entity)
        {
            entity.HasOne(p => p.ChosenBook)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict); //#A
        }
    }
}