using Microsoft.EntityFrameworkCore;

namespace DinningHall.Data
{
    public class DbContextFactory
    {
        public AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase("DinningHallDb")
                .Options;

            return new AppDbContext(options);
        }
    }
}
