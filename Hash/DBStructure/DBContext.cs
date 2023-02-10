using Hash.DBStructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Var21.DBStructure
{
    public class DataContext100 : DbContext
    {
        public DbSet<BaseInfo> Info { get; set; }

        public string DbPath { get; }

        public DataContext100()
        {
            DbPath = DbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB\\DataBase100.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }

    public class DataContext1000 : DbContext
    {
        public DbSet<BaseInfo> Info { get; set; }

        public string DbPath { get; }

        public DataContext1000()
        {
            DbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB\\DataBase1000.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }

    public class DataContext10000 : DbContext
    {
        public DbSet<BaseInfo> Info { get; set; }

        public string DbPath { get; }

        public DataContext10000()
        {
            DbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB\\DataBase10000.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
    public class DataContext20000 : DbContext
    {
        public DbSet<BaseInfo> Info { get; set; }

        public string DbPath { get; }

        public DataContext20000()
        {
            DbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB\\DataBase20000.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }

    public class DataContext40000 : DbContext
    {
        public DbSet<BaseInfo> Info { get; set; }

        public string DbPath { get; }

        public DataContext40000()
        {
            DbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB\\DataBase40000.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }

    public class DataContext60000 : DbContext
    {
        public DbSet<BaseInfo> Info { get; set; }

        public string DbPath { get; }

        public DataContext60000()
        {
            DbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB\\DataBase60000.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }

    public class DataContext80000 : DbContext
    {
        public DbSet<BaseInfo> Info { get; set; }

        public string DbPath { get; }

        public DataContext80000()
        {
            DbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB\\DataBase80000.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }

    public class DataContext100000 : DbContext
    {
        public DbSet<BaseInfo> Info { get; set; }

        public string DbPath { get; }

        public DataContext100000()
        {
            DbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB\\DataBase100000.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }

    public class DataContext300000 : DbContext
    {
        public DbSet<BaseInfo> Info { get; set; }

        public string DbPath { get; }

        public DataContext300000()
        {
            DbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB\\DataBase300000.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
}
