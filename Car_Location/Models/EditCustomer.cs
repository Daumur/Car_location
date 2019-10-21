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
    public class EditCustomer : Customer, IValidatableObject
    {
        public EditCustomer()
        {
            this.ID_Customer = -1;
        }

        public EditCustomer(Customer customer)
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
            this.Password = null;
            this.PasswordVerif = null;
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

            if(Password != null && PasswordVerif != null)
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