namespace Car_Location.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<Agency> Agencies { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Hire_Agreement> Hire_Agreement { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Return> Returns { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agency>()
                .HasMany(e => e.Cars)
                .WithRequired(e => e.Agency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Agency>()
                .HasMany(e => e.Returns)
                .WithRequired(e => e.Agency)
                .HasForeignKey(e => e.ID_New_Agency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Agency>()
                .HasMany(e => e.Staffs)
                .WithRequired(e => e.Agency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.Engine)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.Category);

            modelBuilder.Entity<Car>()
                .Property(e => e.Date_MOT_due)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .HasMany(e => e.Hire_Agreement)
                .WithRequired(e => e.Car)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Date_Of_Birth)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Driving_Licence_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.PasswordHash)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Hire_Agreement)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hire_Agreement>()
                .Property(e => e.Rental_Start_Date)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Hire_Agreement>()
                .Property(e => e.Rental_End_Date)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Hire_Agreement>()
                .HasMany(e => e.Returns)
                .WithRequired(e => e.Hire_Agreement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Model>()
                .Property(e => e.Brand)
                .IsUnicode(false);

            modelBuilder.Entity<Model>()
                .Property(e => e.Model1)
                .IsUnicode(false);

            modelBuilder.Entity<Model>()
                .HasMany(e => e.Cars)
                .WithRequired(e => e.Model)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Return>()
                .Property(e => e.Date_Checked)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Return>()
                .Property(e => e.New_Fault_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Phone_Number)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.National_Insurance_Number)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Date_Joined_Company)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Salary)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Staff>()
                 .Property(e => e.Job_Title)
                 .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.PasswordHash)
                .IsFixedLength();
        }
    }
}
