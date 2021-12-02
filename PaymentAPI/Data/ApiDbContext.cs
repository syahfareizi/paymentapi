using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;

namespace PaymentAPI.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public DbSet<PaymentDetailItem> paymentdetail { get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
    }
}