namespace Car_Location.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Car")]
    public partial class Car
    {
        public enum Categories
        {
            Economic,
            Prestige,
            Road,
            SUV

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Car()
        {
            Hire_Agreement = new HashSet<Hire_Agreement>();
        }

        [Key]
        public int ID_Car { get; set; }

        [Required]
        [StringLength(64)]
        public string Engine { get; set; }

        [Required]
        public Categories Category { get; set; }

        [StringLength(64)]
        [DisplayName("Image car")]
        public string ImgPath { get; set; }


        public int Capacity { get; set; }

        [DisplayName("Current Mileage")]
        public int Mileage_Current { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        [DisplayName("Date MOT due")]
        [DataType(DataType.Date)]
        public string Date_MOT_due { get; set; }


        [DisplayName("Agency")]
        public int ID_Agency { get; set; }

        [DisplayName("Model")]
        public int ID_Model { get; set; }

        public virtual Agency Agency { get; set; }

        public virtual Model Model { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hire_Agreement> Hire_Agreement { get; set; }
    }
}
