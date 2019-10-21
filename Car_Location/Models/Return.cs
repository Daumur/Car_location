namespace Car_Location.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Return")]
    public partial class Return
    {
        [Key]
        public int ID_Return { get; set; }

        [DisplayName("New Car Location")]
        public int ID_New_Agency { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        [DisplayName("Date checked")]
        [DataType(DataType.Date)]
        public string Date_Checked { get; set; }

        [Required]
        [DisplayName("New current mileage")]
        public int New_Mileage_Current { get; set; }

        [StringLength(2000)]
        [DisplayName("New fault description")]
        public string New_Fault_Description { get; set; }

        [DisplayName("Hire agreement")]
        public int ID_Hire_Agreement { get; set; }

        public virtual Agency Agency { get; set; }

        public virtual Hire_Agreement Hire_Agreement { get; set; }
    }
}
