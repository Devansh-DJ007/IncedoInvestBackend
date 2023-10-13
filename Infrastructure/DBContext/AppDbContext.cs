﻿using IncedoInvest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncedoInvest.Infrastructure.DBContext
{
    public class AppDbContextClass: DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<InvestmentInfo> InvestmentInfos { get; set; }
        public DbSet<InvestmentType> InvestmentTypes { get; set; }
        public DbSet<InvestmentStrategy> InvestmentStrategies { get; set; }
        public DbSet<ProposedInvestment> ProposedInvestments { get; set; }
    }
}
