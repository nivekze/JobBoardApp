using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

using JobBoardApp_Models;

namespace JobBoardApp_DataAcces.Context
{
    public class JobBoardAppContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }

        public JobBoardAppContext(DbContextOptions<JobBoardAppContext> options) : base()
        {
            this.ChangeTracker.LazyLoadingEnabled = false;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
