using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Car_Location.Models
{
    [NotMapped]
    public class RegisterCustomer : Customer, IValidatableObject
    {
        public RegisterCustomer()
        {
            Hire_Agreement = new HashSet<Hire_Agreement>();
        }

        public RegisterCustomer(RegisterCustomer register)
        {
            Hire_Agreement = new HashSet<Hire_Agreement>();


            this.ID_Customer = register.ID_Customer;
            this.Genre = register.Genre;
            this.First_Name = register.First_Name;
            this.Last_Name = register.Last_Name;
            this.Date_Of_Birth = register.Date_Of_Birth;
            this.Phone_Number = register.Phone_Number;
            this.Address = register.Address;
            this.PasswordHash = register.PasswordHash;
            this.Driving_Licence_Number = register.Driving_Licence_Number;
            this.Name_Of_Business = register.Name_Of_Business;
            this.Type_Of_Business = register.Type_Of_Business;
        }


        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Password verification")]
        [DataType(DataType.Password)]
        public string PasswordVerif { get; set; }

        public new IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            Model1 db = new Model1();

            if (!PasswordVerif.Equals(Password))
            {
                yield return new ValidationResult("Password must correspond", new[] { nameof(PasswordVerif) });
            }

            Regex passwordLowerCase = new Regex(@"[a-z].*[a-z]");
            Regex passwordUpperCase = new Regex(@"[A-Z].*[A-Z]");
            Regex passwordNumber = new Regex(@"[0-9].*[0-9]");
            Regex passwordOther = new Regex(@"[^a-zA-Z0-9].*[^a-zA-Z0-9]");
            if (!passwordLowerCase.IsMatch(Password) || !passwordUpperCase.IsMatch(Password) || !passwordNumber.IsMatch(Password) || !passwordOther.IsMatch(Password))
            {
                yield return new ValidationResult("Please enter a valid password, at least two lower case letter, two upper case letter, two number and two other characters", new[] { nameof(Password) });
            }
        }
    }
}