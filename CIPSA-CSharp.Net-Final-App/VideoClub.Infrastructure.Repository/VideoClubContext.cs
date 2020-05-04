using System.Data.Entity;
using VideoClub.Infrastructure.Repository.Entity;
using VideoClub.Infrastructure.Repository.Utils;

namespace VideoClub.Infrastructure.Repository
{
    public class VideoClubContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        private static VideoClubContext _videoClubContext;

        public static VideoClubContext GetVideoClubContext()
        {
            return _videoClubContext ?? (_videoClubContext = new VideoClubContext());
        }
        public VideoClubContext() : base($"name=VideoClubConnection")
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<VideoClubContext>());

            modelBuilder.Entity<Product>()
                .Map<VideoGame>(m => m.Requires("ProductDtoType").HasValue("VideoGame"))
                .Map<Movie>(m => m.Requires("ProductDtoType").HasValue("Movie"));
        }
    }
}
