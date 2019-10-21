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
    public class EditStaff : Staff, IValidatableObject
    {
        public EditStaff()
        {
        }

        public EditStaff(Staff staff)
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


        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Password verification")]
        [DataType(DataType.Password)]
        public string PasswordVerif { get; set; }

        public new IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            Model1 db = new Model1();

            if (Password != null && PasswordVerif != null && !PasswordVerif.Equals(Password))
            {
                yield return new ValidationResult("Password must correspond", new[] { nameof(PasswordVerif) });
            }


            if (Password != null && PasswordVerif != null)
            {
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
}
