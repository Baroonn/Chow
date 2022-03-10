using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.HasData
        (
            new Store
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                Name = "Hexagon Rice",
                Email = "bolubee95@gmail.com",
                Location = "Samonda",
                Phone = "+2347069570633",
                Is_Open = true,
            },
            new Store
            {
                Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                Name = "Nana's Kitchen",
                Email = "lekeboj@gmail.com",
                Location = "Iwo Road",
                Phone = "+2349134946613",
                Is_Open = true,
            }
            
        );
    }
}