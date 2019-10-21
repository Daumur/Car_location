namespace Car_Location.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        public enum Gender
        {
            Male,
            Female
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Hire_Agreement = new HashSet<Hire_Agreement>();
        }

        public Customer(Customer customer)
        {
            Hire_Agreement = new HashSet<Hire_Agreement>();


            this.ID_Customer = customer.ID_Customer;
            this.Genre = customer.Genre;
            this.First_Name = customer.First_Name;
            this.Last_Name = customer.Last_Name;
            this.Date_Of_Birth = customer.Date_Of_Birth;
            this.Phone_Number = customer.Phone_Number;
            this.Address = customer.Address;
            this.PasswordHash = customer.PasswordHash;
            this.Driving_Licence_Number = customer.Driving_Licence_Number;
            this.Name_Of_Business = customer.Name_Of_Business;
            this.Type_Of_Business = customer.Type_Of_Business;
        }

        [Key]
        public int ID_Customer { get; set; }

        [Required]
        public Gender Genre { get; set; }

        [Required]
        [StringLength(256)]
        [DisplayName("First name")]
        public string First_Name { get; set; }

        [Required]
        [StringLength(256)]
        [DisplayName("Last name")]
        public string Last_Name { get; set; }

        [Required]
        [StringLength(1024)]
        public string Address { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})([0-9]{4})?$", ErrorMessage = "Not a valid phone number")]
        public string Phone_Number { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        [DisplayName("Date of birth")]
        [DataType(DataType.Date)]
        public string Date_Of_Birth { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Driving licence number")]
        public string Driving_Licence_Number { get; set; }

        [Required]
        [StringLength(256)]
        [DisplayName("Name of business")]
        public string Name_Of_Business { get; set; }

        [Required]
        [StringLength(256)]
        [DisplayName("Type of business")]
        public string Type_Of_Business { get; set; }

        [NotMapped]
        public string Full_Name { get { return this.First_Name + " " + this.Last_Name; } }

        [MaxLength(32)]
        public byte[] PasswordHash { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hire_Agreement> Hire_Agreement { get; set; }
    }
}
