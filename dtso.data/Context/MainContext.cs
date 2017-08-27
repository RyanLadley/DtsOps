using dtso.data.Entities;
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
        }


        public DbSet<Account> Accounts { get; set; }
        public DbSet<RegionalAccountCode> RegionalAccountCodes { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceType> InvoiceTypes { get; set; }
        public DbSet<InvoiceAccount> InvoiceAccounts { get; set; }

        public virtual DbSet<vAccount> vAccounts { get; set; }

        /// <summary>
        /// Views are built manually, do not update db based on view;
        /// </summary>
        private ModelBuilder _ignoreViews(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<vAccount>();

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
                .Property(account => account.SubNo)
                    .HasDefaultValue(-1);

            modelBuilder.Entity<Account>()
                .Property(account => account.ShredNo)
                .HasDefaultValue(-1);

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
            modelBuilder.Entity<Vendor>()
                .Property(vendor => vendor.PhoneNumber)
                    .HasMaxLength(12);

            modelBuilder.Entity<Vendor>()
                .Property(vendor => vendor.ZipCode)
                    .HasMaxLength(8);

            modelBuilder.Entity<Vendor>()
                .Property(vendor => vendor.ContractNumber)
                    .HasMaxLength(45);

            modelBuilder.Entity<Vendor>()
                .HasKey(vendor => vendor.VendorId);

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
                .HasKey(ia => new { ia.InvoiceId, ia.AccountId});

            modelBuilder.Entity<InvoiceAccount>()
                .HasOne(ia => ia.Invoice)
                .WithMany(invoice => invoice.InvoiceAccounts)
                .HasForeignKey(ia => ia.InvoiceId);

            modelBuilder.Entity<InvoiceAccount>()
                .HasOne(ia => ia.Account)
                .WithMany(account => account.InvoiceAccounts)
                .HasForeignKey(ia => ia.AccountId);

            if (!IsMigration){
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
    }
}
