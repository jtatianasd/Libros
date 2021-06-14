using CRUDLibros.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDLibros.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext()
		{

		}
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<Autor> Autor { get; set; }
		public DbSet<Editorial> Editorial { get; set; }
		public DbSet<Libro> Libro { get; set; }
	}
}
