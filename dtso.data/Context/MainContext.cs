using dtso.data.Entities;
using dtso.data.StoredProcedures;
using dtso.data.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Context
{
    public class MainContext : DbContext
    {
        //Turn this to true when running migrations. It prevents view Tables from being added to db
        bool IsMigration = false;

        public MainContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (IsMigration)
                modelBuilder = _ignoreViews(modelBuilder);
            else
                modelBuilder = _processViews(modelBuilder);

            modelBuilder = _buildAccounts(modelBuilder);
            modelBuilder = _buildRegionalAccountCodes(modelBuilder);
            modelBuilder = _buildVendors(modelBuilder);
            modelBuilder = _buildInvoices(modelBuilder);
            modelBuilder = _buildInvoiceAccounts(modelBuilder);
            modelBuilder = _buildCityAccounts(modelBuilder);
            modelBuilder = _buildCityExpenses(modelBuilder);
            modelBuilder = _buildMaterials(modelBuilder);
            modelBuilder = _buildMaterialVendors(modelBuilder);
            modelBuilder = _buildTickets(modelBuilder);
            modelBuilder = _buildTransfers(modelBuilder);
            modelBuilder = _buildUser(modelBuilder);
        }


        public DbSet<Account> Accounts { get; set; }
        public DbSet<RegionalAccountCode> RegionalAccountCodes { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceType> InvoiceTypes { get; set; }
        public DbSet<InvoiceAccount> InvoiceAccounts { get; set; }
        public DbSet<CityAccount> CityAccounts { get; set; }
        public DbSet<CityExpense> CityExpenses { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialVendor> MaterialVendors { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<User> Users { get; set; }

        public virtual DbSet<SearchResult> SearchResults { get; set; }
        public virtual DbSet<vAccount> vAccounts { get; set; }

        /// <summary>
        /// Views are built manually, do not update db based on view;
        /// </summary>
        private ModelBuilder _ignoreViews(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<vAccount>();
            modelBuilder.Ignore<SearchResult>();

            return modelBuilder;
        }

        private ModelBuilder _processViews(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<vAccount>()
                .HasKey(account => account.AccountId);

            modelBuilder.Entity<vAccount>()
                .HasIndex(account => new
                {
                    account.RegionalAccountCodeId,
                    account.SubNo,
                    account.ShredNo
                }).IsUnique();

            return modelBuilder;
        }

        private ModelBuilder _buildAccounts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasKey(account => account.AccountId);

            modelBuilder.Entity<Account>()
                .HasIndex(account => new
                {
                    account.RegionalAccountCodeId,
                    account.SubNo,
                    account.ShredNo
                }).IsUnique();

            modelBuilder.Entity<Account>()
                .Property(account => account.SubNo);

            modelBuilder.Entity<Account>()
                .Property(account => account.ShredNo);

            modelBuilder.Entity<Account>()
                .Property(account => account.AnnualBudget)
                    .HasDefaultValue(0)
                    .HasColumnType("Money");

            modelBuilder.Entity<Account>()
                .HasOne(account => account.RegionalAccountCode);

            modelBuilder.Entity<Account>()
                .Property(account => account.RegionalAccountCodeId).IsRequired();

            return modelBuilder;
        }

        private ModelBuilder _buildRegionalAccountCodes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegionalAccountCode>()
                .Property(code => code.AccountNumber).IsRequired();

            modelBuilder.Entity<RegionalAccountCode>()
                .Property(code => code.FundNumber).IsRequired();

            modelBuilder.Entity<RegionalAccountCode>()
                .Property(code => code.DeptartmentNumber).IsRequired();

            modelBuilder.Entity<RegionalAccountCode>()
                .Property(code => code.ProjectNumber).IsRequired();

            modelBuilder.Entity<RegionalAccountCode>()
                .Property(code => code.AccountPrefix).IsRequired();

            return modelBuilder;
        }

        private ModelBuilder _buildVendors(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendor>(entity =>
                {
                    entity.HasKey(vendor => vendor.VendorId);

                    entity.Property(vendor => vendor.PhoneNumber)
                            .HasMaxLength(12);

                    entity.Property(vendor => vendor.ContractNumber)
                         .HasMaxLength(45);

                    entity.Property(vendor => vendor.Active)
                        .HasDefaultValue(true);
                }
            );


            return modelBuilder;
        }

        private ModelBuilder _buildInvoiceTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceType>()
                .HasKey(invoiceType => invoiceType.InvoiceTypeId);

            modelBuilder.Entity<InvoiceType>()
                .Property(invoiceType => invoiceType.Name)
                    .IsRequired();

            return modelBuilder;
        }

        private ModelBuilder _buildInvoices(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
                .HasKey(invoice => invoice.InvoiceId);

            modelBuilder.Entity<Invoice>()
                .HasIndex(invoice => new { invoice.VendorId, invoice.InvoiceNumber })
                .IsUnique();

            return modelBuilder;
        }

        private ModelBuilder _buildInvoiceAccounts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceAccount>()
                .HasKey(ia => ia.InvoiceAccountId);

            modelBuilder.Entity<InvoiceAccount>()
                .HasIndex(ia => new { ia.InvoiceId, ia.AccountId })
                .IsUnique();

            modelBuilder.Entity<InvoiceAccount>()
                .HasOne(ia => ia.Invoice)
                .WithMany(invoice => invoice.InvoiceAccounts)
                .HasForeignKey(ia => ia.InvoiceId);

            modelBuilder.Entity<InvoiceAccount>()
                .HasOne(ia => ia.Account)
                .WithMany(account => account.InvoiceAccounts)
                .HasForeignKey(ia => ia.AccountId);

            if (!IsMigration)
            {
                modelBuilder.Entity<InvoiceAccount>()
                    .HasOne(ia => ia.vAccount)
                    .WithMany(vAccount => vAccount.InvoiceAccounts)
                    .HasForeignKey(ia => ia.AccountId);
            }

            modelBuilder.Entity<InvoiceAccount>()
                .Property(invoiceAccount => invoiceAccount.Expense)
                    .HasDefaultValue(0)
                    .HasColumnType("Money");

            return modelBuilder;
        }

        private ModelBuilder _buildCityAccounts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityAccount>(entity =>
                {
                    entity.HasKey(cityAccount => cityAccount.CityAccountId);

                }
            );

            return modelBuilder;
        }

        private ModelBuilder _buildCityExpenses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityExpense>(entity =>
            {
                entity.HasKey(cityExpense => cityExpense.CityExpenseId);

                entity.HasIndex(cityExpense => new { cityExpense.CityAccountId, cityExpense.InvoiceAccountId })
                    .IsUnique();

                entity.HasOne(cityExpense => cityExpense.CityAccount)
                    .WithMany(account => account.CityExpenses)
                    .HasForeignKey(cityExpense => cityExpense.CityAccountId);

                entity.HasOne(cityExpense => cityExpense.InvoiceAccount)
                    .WithMany(account => account.CityExpenses)
                    .HasForeignKey(cityExpense => cityExpense.InvoiceAccountId);

                entity.Property(cityExpense => cityExpense.Expense)
                    .HasDefaultValue(0)
                    .HasColumnType("Money");
            }
            );

            return modelBuilder;
        }

        private ModelBuilder _buildMaterials(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(material => material.MaterialId);
            }
            );

            return modelBuilder;
        }

        private ModelBuilder _buildMaterialVendors(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaterialVendor>(entity =>
            {
                entity.HasKey(materialVendor => materialVendor.MaterialVendorId);

                entity.HasOne(materialVendor => materialVendor.Material)
                    .WithMany(material => material.MaterialVendors)
                    .HasForeignKey(materialVendor => materialVendor.MaterialId);

                entity.HasOne(materialVendor => materialVendor.Vendor)
                    .WithMany(vendor => vendor.MaterialVendors)
                    .HasForeignKey(materialVendor => materialVendor.VendorId);

                entity.Property(materialVnedor => materialVnedor.Cost)
                    .HasDefaultValue(0)
                    .HasColumnType("Money");
            }
            );

            return modelBuilder;
        }

        private ModelBuilder _buildTickets(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(ticket => ticket.TicketId);

                entity.HasOne(ticket => ticket.MaterialVendor)
                    .WithMany(material => material.Tickets)
                    .HasForeignKey(ticket => ticket.MaterialVendorId);

                entity.HasOne(ticket => ticket.Invoice)
                    .WithMany(invoice => invoice.Tickets)
                    .HasForeignKey(ticket => ticket.InvoiceId);

                entity.HasOne(ticket => ticket.Vendor)
                    .WithMany(vendor => vendor.Tickets)
                    .HasForeignKey(ticket => ticket.VendorId);

                entity.HasOne(ticket => ticket.Account)
                    .WithMany(account => account.Tickets)
                    .HasForeignKey(ticket => ticket.AccountId);

                entity.HasOne(ticket => ticket.vAccount)
                    .WithMany(vaccount => vaccount.Tickets)
                    .HasForeignKey(ticket => ticket.AccountId);

                entity.Property(ticket => ticket.Cost)
                    .HasDefaultValue(0)
                    .HasColumnType("Money");
            }
            );

            return modelBuilder;
        }

        private ModelBuilder _buildTransfers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.HasKey(transfer => transfer.TransferId);

                entity.HasOne(transfer => transfer.FromAccount)
                    .WithMany(account => account.TransfersFrom)
                    .HasForeignKey(transfer => transfer.FromAccountId);

                entity.HasOne(transfer => transfer.FromvAccount)
                    .WithMany(account => account.TransfersFrom)
                    .HasForeignKey(transfer => transfer.FromAccountId);

                entity.HasOne(transfer => transfer.ToAccount)
                    .WithMany(account => account.TransfersTo)
                    .HasForeignKey(transfer => transfer.ToAccountId);

                entity.HasOne(transfer => transfer.TovAccount)
                    .WithMany(account => account.TransfersTo)
                    .HasForeignKey(transfer => transfer.ToAccountId);

                entity.Property(transfer => transfer.DateCreated)
                    .HasDefaultValueSql("getdate()");
                
                entity.Property(transfer => transfer.Amount)
                    .HasDefaultValue(0)
                    .HasColumnType("Money");
            }
            );

            return modelBuilder;
        }

        private ModelBuilder _buildUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(user => user.Email)
                    .IsRequired();

                entity.Property(user => user.Password)
                    .IsRequired();

                entity.Property(user => user.Salt)
                    .IsRequired();

                entity.Property(user => user.FirstName)
                    .IsRequired();

                entity.Property(user => user.LastName)
                    .IsRequired();
            });


            return modelBuilder;
        }

        private ModelBuilder _buildSearchResult(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchResult>(entity =>
            {
                entity.HasKey(search => search.Id);

                entity.Property(search => search.Id)
                    .IsRequired();
            
            });


            return modelBuilder;
        }
    }
}
