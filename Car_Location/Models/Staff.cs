namespace Car_Location.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using static Car_Location.Models.Customer;

    [Table("Staff")]
    public partial class Staff
    {

        public Staff()
        {
        }

        public Staff(Staff staff)
        {


            this.ID_Staff = staff.ID_Staff;
            this.Home_Address = staff.Home_Address;
            this.First_Name = staff.First_Name;
            this.Last_Name = staff.Last_Name;
            this.Sex = staff.Sex;
            this.Phone_Number = staff.Phone_Number;
            this.National_Insurance_Number = staff.National_Insurance_Number;
            this.PasswordHash = staff.PasswordHash;
            this.Date_Joined_Company = staff.Date_Joined_Company;
            this.Job_Title = staff.Job_Title;
            this.Salary = staff.Salary;
            this.ID_Agency = staff.ID_Agency;

        }

        [Key]
        public int ID_Staff { get; set; }

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
        [DisplayName("home address")]
        public string Home_Address { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})([0-9]{4})?$", ErrorMessage = "Not a valid phone number")]
        public string Phone_Number { get; set; }

        [DisplayName("Genre")]
        public Gender Sex { get; set; }

        [Required]
        [StringLength(11)]
        [DisplayName("National Insurance Number")]
        public string National_Insurance_Number { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        [DisplayName("Date Joined company")]
        [DataType(DataType.Date)]
        public string Date_Joined_Company { get; set; }

        [Required]
        [DisplayName("Job title")]
        [StringLength(40)]
        public string Job_Title { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [DisplayName("Agency")]
        public int ID_Agency { get; set; }

        [MaxLength(32)]
        public byte[] PasswordHash { get; set; }

        public virtual Agency Agency { get; set; }
    }
}
