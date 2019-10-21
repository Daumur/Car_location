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
    public class RegisterStaff : Staff, IValidatableObject
    {
        public RegisterStaff()
        {
        }

        public RegisterStaff(RegisterStaff register)
        {


            this.ID_Staff = register.ID_Staff;
            this.Home_Address = register.Home_Address;
            this.First_Name = register.First_Name;
            this.Last_Name = register.Last_Name;
            this.Sex = register.Sex;
            this.Phone_Number = register.Phone_Number;
            this.National_Insurance_Number = register.National_Insurance_Number;
            this.PasswordHash = register.PasswordHash;
            this.Date_Joined_Company = register.Date_Joined_Company;
            this.Job_Title = register.Job_Title;
            this.Salary = register.Salary;
            this.ID_Agency = register.ID_Agency;

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