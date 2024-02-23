using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entity;

namespace ToDoList.Persistence.Configuration
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RefreshToken).IsRequired();
            builder.Property(x => x.RefreshTokenExpiryTime).IsRequired();

            //builder.HasData(new List<UserToken>()
            //{
            //    new UserToken()
            //    {
            //        Id = Guid.NewGuid(),
            //        RefreshToken = "alksdfj34kjge9340t89ghe",
            //        RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7),
                    
            //    }
            //});
        }
    }
}
