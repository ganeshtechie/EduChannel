using EduChannel.Domain;
using Microsoft.EntityFrameworkCore;

namespace EduChannel.Data
{
    public class EduChannelContext : DbContext
    {

        public EduChannelContext(DbContextOptions<EduChannelContext> options) : base(options)
        { }

        public DbSet<Channel> Channels { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
