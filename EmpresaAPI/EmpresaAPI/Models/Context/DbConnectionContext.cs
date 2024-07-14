using EmpresaAPI.Models.TablesTbl;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace StripeTestBakery.Models.Context
{
    public class DbConnectionContext : DbContext
    {
        public DbConnectionContext(DbContextOptions<DbConnectionContext> options) : base(options)
        {
        }
        public DbSet<ProductTbl> ProductTbl { get; set; } = null!;
        public DbSet<ClientTbl> ClientTbl { get; set; } = null!;
        public DbSet<BillTbl> BillTbl { get; set; } = null!;
        public DbSet<PhotoTbl> PhotoTbl { get; set; } = null!;
    }
}