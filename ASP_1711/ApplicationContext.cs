﻿using Microsoft.EntityFrameworkCore;

namespace ASP_1711
{
	public class ApplicationContext: DbContext
	{
		public DbSet<User> Users { get; set; }
		public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
		{
			Database.EnsureCreated();
		}
	}
}
