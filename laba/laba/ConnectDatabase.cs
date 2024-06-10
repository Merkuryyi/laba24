using Microsoft.EntityFrameworkCore;

namespace laba
{
    public class Users
    {
        public string Login { get; set; }
        //нужна для связи
        public ICollection<Task> Tasks { get; set; }
        public string Password { get; set; }
    }
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
       // [Column(TypeName = "Date")] второй способ для исправления ошибки связанной с датой
        public DateTime ExecuteBefore { get; set; }
        public string Login { get; set; }
        public Users User { get; set; } = null!; 
    }
        
    public class ConnectDatabase : DbContext
    {
        public ConnectDatabase() 
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Database=rkis24;Username=postgres;Password=qwerty");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasMany(e => e.Tasks)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.Login)
                .IsRequired();
            modelBuilder.Entity<Task>().Property(Task => Task.ExecuteBefore).HasColumnType("TimeStamp");   
            modelBuilder.Entity<Users>().HasKey(user => user.Login);
        }
       public DbSet<Task> Tasks { get; set; }
       public DbSet<Users> Users { get; set; }
    }
}