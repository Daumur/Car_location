namespace Car_Location.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Hire_Agreement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hire_Agreement()
        {
            Returns = new HashSet<Return>();
        }

        [Key]
        public int ID_Hire_Agreement { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        [DisplayName("Rental start date")]
        [DataType(DataType.Date)]
        public string Rental_Start_Date { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        [DisplayName("Rental end date")]
        [DataType(DataType.Date)]
        public string Rental_End_Date { get; set; }

        [DisplayName("Rental mileage")]
        public int Rental_Mileage { get; set; }

        [NotMapped]
        public string Info { get { return this.Customer.First_Name + " " + this.Customer.Last_Name + ", from : " + this.Rental_Start_Date + " to " + this.Rental_End_Date; } }


        [DisplayName("Customer")]
        public int ID_Customer { get; set; }

        [DisplayName("Car")]
        public int ID_Car { get; set; }

        public virtual Car Car { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Return> Returns { get; set; }
    }
}
