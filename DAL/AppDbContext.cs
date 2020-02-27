using System;
using Microsoft.EntityFrameworkCore;

//TODO: Update this using statement to include your project name
using Team_4_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

//TODO: Make this namespace match your project name
namespace Team_4_Project.DAL
{
    //NOTE: This class definition references the user class for this project.  
    //If your User class is called something other than AppUser, you will need
    //to change it in the line below
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
        public DbSet<Team_4_Project.Models.LoginViewModel> LoginViewModel { get; set; }
        public DbSet<Team_4_Project.Models.Account> Account { get; set; }
        public DbSet<Team_4_Project.Models.Transaction> Transaction { get; set; }

        public DbSet<Team_4_Project.Models.Stock> Stocks { get; set; }

        public DbSet<Team_4_Project.Models.StockPortion> StockPortions { get; set; }

        public DbSet<Team_4_Project.Models.Payee> Payees { get; set; }

        public DbSet<Team_4_Project.Models.Disputes> Disputes { get; set; }

        //TODO: Add Dbsets here.  Products is included as an example.  
        //public DbSet<Product> Products { get; set; }
    }
}
