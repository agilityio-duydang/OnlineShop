namespace Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OnlineShopDbContext : DbContext
    {
        public OnlineShopDbContext()
            : base("name=OnlineShopDbContext")
        {
        }

        public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
        public virtual DbSet<ActivityLogType> ActivityLogTypes { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<BlogComment> BlogComments { get; set; }
        public virtual DbSet<BlogPost> BlogPosts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerPassword> CustomerPasswords { get; set; }
        public virtual DbSet<CustomerRole> CustomerRoles { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<DiscountRequirement> DiscountRequirements { get; set; }
        public virtual DbSet<DiscountUsageHistory> DiscountUsageHistories { get; set; }
        public virtual DbSet<EmailAccount> EmailAccounts { get; set; }
        public virtual DbSet<GiftCard> GiftCards { get; set; }
        public virtual DbSet<GiftCardUsageHistory> GiftCardUsageHistories { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsComment> NewsComments { get; set; }
        public virtual DbSet<NewsLetterSubscription> NewsLetterSubscriptions { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<OrderNote> OrderNotes { get; set; }
        public virtual DbSet<PermissionRecord> PermissionRecords { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Poll> Polls { get; set; }
        public virtual DbSet<PollAnswer> PollAnswers { get; set; }
        public virtual DbSet<PollVotingRecord> PollVotingRecords { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Product_Category_Mapping> Product_Category_Mapping { get; set; }
        public virtual DbSet<Product_Manufacturer_Mapping> Product_Manufacturer_Mapping { get; set; }
        public virtual DbSet<Product_Picture_Mapping> Product_Picture_Mapping { get; set; }
        public virtual DbSet<ProductReview> ProductReviews { get; set; }
        public virtual DbSet<ProductTag> ProductTags { get; set; }
        public virtual DbSet<QueuedEmail> QueuedEmails { get; set; }
        public virtual DbSet<RelatedProduct> RelatedProducts { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaxCategory> TaxCategories { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<VendorNote> VendorNotes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasMany(e => e.Customers)
                .WithOptional(e => e.Address)
                .HasForeignKey(e => e.BillingAddress_Id);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.Customers1)
                .WithOptional(e => e.Address1)
                .HasForeignKey(e => e.ShippingAddress_Id);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Address)
                .HasForeignKey(e => e.BillingAddressId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.Orders1)
                .WithOptional(e => e.Address1)
                .HasForeignKey(e => e.PickupAddressId);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.Orders2)
                .WithOptional(e => e.Address2)
                .HasForeignKey(e => e.ShippingAddressId);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.CustomerRoles)
                .WithMany(e => e.Customers)
                .Map(m => m.ToTable("Customer_CustomerRole_Mapping"));

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Addresses)
                .WithMany(e => e.Customers2)
                .Map(m => m.ToTable("CustomerAddresses"));

            modelBuilder.Entity<Discount>()
                .Property(e => e.DiscountPercentage)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Discount>()
                .Property(e => e.DiscountAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Discount>()
                .Property(e => e.MaximumDiscountAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Discount>()
                .HasMany(e => e.Categories)
                .WithMany(e => e.Discounts)
                .Map(m => m.ToTable("Discount_AppliedToCategories"));

            modelBuilder.Entity<Discount>()
                .HasMany(e => e.Manufacturers)
                .WithMany(e => e.Discounts)
                .Map(m => m.ToTable("Discount_AppliedToManufacturers"));

            modelBuilder.Entity<Discount>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Discounts)
                .Map(m => m.ToTable("Discount_AppliedToProducts"));

            modelBuilder.Entity<DiscountRequirement>()
                .HasMany(e => e.DiscountRequirement1)
                .WithOptional(e => e.DiscountRequirement2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<GiftCard>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<GiftCardUsageHistory>()
                .Property(e => e.UsedValue)
                .HasPrecision(18, 4);

            modelBuilder.Entity<News>()
                .HasMany(e => e.NewsComments)
                .WithRequired(e => e.News)
                .HasForeignKey(e => e.NewsItemId);

            modelBuilder.Entity<Order>()
                .Property(e => e.CurrencyRate)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderSubtotalInclTax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderSubtotalExclTax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderSubTotalDiscountInclTax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderSubTotalDiscountExclTax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderShippingInclTax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderShippingExclTax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.PaymentMethodAdditionalFeeInclTax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.PaymentMethodAdditionalFeeExclTax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderTax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderDiscount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderTotal)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.GiftCardUsageHistories)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.UsedWithOrderId);

            modelBuilder.Entity<OrderItem>()
                .Property(e => e.UnitPriceInclTax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<OrderItem>()
                .Property(e => e.UnitPriceExclTax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<OrderItem>()
                .Property(e => e.PriceInclTax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<OrderItem>()
                .Property(e => e.PriceExclTax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<OrderItem>()
                .Property(e => e.DiscountAmountInclTax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<OrderItem>()
                .Property(e => e.DiscountAmountExclTax)
                .HasPrecision(18, 4);

            modelBuilder.Entity<OrderItem>()
                .Property(e => e.OriginalProductCost)
                .HasPrecision(18, 4);

            modelBuilder.Entity<OrderItem>()
                .Property(e => e.ItemWeight)
                .HasPrecision(18, 4);

            modelBuilder.Entity<OrderItem>()
                .HasMany(e => e.GiftCards)
                .WithOptional(e => e.OrderItem)
                .HasForeignKey(e => e.PurchasedWithOrderItemId);

            modelBuilder.Entity<PermissionRecord>()
                .HasMany(e => e.CustomerRoles)
                .WithMany(e => e.PermissionRecords)
                .Map(m => m.ToTable("PermissionRecord_Role_Mapping"));

            modelBuilder.Entity<Product>()
                .Property(e => e.AdditionalShippingCharge)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.OldPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductCost)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.MinimumCustomerEnteredPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.MaximumCustomerEnteredPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Weight)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Length)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Width)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Height)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductTags)
                .WithMany(e => e.Products)
                .Map(m => m.ToTable("Product_ProductTag_Mapping"));

            modelBuilder.Entity<ShoppingCartItem>()
                .Property(e => e.CustomerEnteredPrice)
                .HasPrecision(18, 4);
        }
    }
}
