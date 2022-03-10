using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class MealComponentConfiguration : IEntityTypeConfiguration<MealComponent>
{
    public void Configure(EntityTypeBuilder<MealComponent> builder)
    {
        builder.HasData
        (
            new MealComponent
            {
                Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                Name = "Ofada Rice",
                Type = "main",
                Price = 1500,
                StoreId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            },
            new MealComponent
            {
                Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                Name = "Fish Stew",
                Type = "soup",
                StoreId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            },
            new MealComponent
            {
                Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                Name = "Beef",
                Type = "topping",
                Price = 500,
                StoreId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            }
            
        );
    }
}