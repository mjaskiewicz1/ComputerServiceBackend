using Data.Config;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class ComputerServiceContext : DbContext
{
    public ComputerServiceContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Models.Config> Configs { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;
    public DbSet<InvoiceItem> InvoiceItems { get; set; } = null!;
    public DbSet<Request> Requests { get; set; } = null!;
    public DbSet<RequestInvoiceDetail> RequestInvoiceDetails { get; set; } = null!;
    public DbSet<RequestShipmentDetail> RequestShipmentDetails { get; set; } = null!;
    public DbSet<RequestConversation> RequestConversations { get; set; } = null!;
    public DbSet<Service> Services{ get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
       
       
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        new ConfigConfiguration().Configure(builder.Entity<Models.Config>());
        new ProductConfiguration().Configure(builder.Entity<Product>());
        new InvoiceConfiguration().Configure(builder.Entity<Invoice>());
        new InvoiceItemConfiguration().Configure(builder.Entity<InvoiceItem>());
        new RequestConfiguration().Configure(builder.Entity<Request>());
        new RequestInvoiceDetailConfiguration().Configure(builder.Entity<RequestInvoiceDetail>());
        new RequestShipmentDetailConfiguration().Configure(builder.Entity<RequestShipmentDetail>());
        new RequestConversationConfiguration().Configure(builder.Entity<RequestConversation>());
        new ServiceConfiguration().Configure(builder.Entity<Service>());

        base.OnModelCreating(builder);
    }
}