using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public  class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseMySql("server=localhost;port=3306;database=turtleDB;user=root;password=1292Eness", new MySqlServerVersion(new Version(8, 0, 31)));
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
    }
}
