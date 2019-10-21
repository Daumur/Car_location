using Car_Location.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Car_Location.Models
{
    public class Login : IValidatableObject
    {
        public int? ID_Customer { get; set; }
        public int? ID_Staff { get; set; }

        public byte[] PasswordHash { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string First_Name { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string Last_Name { get; set; }

        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            Model1 db = new Model1();
            SHA256 sha256Hash = SHA256.Create();
            PasswordHash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Password));

            bool testCustomer = db.Customers.Any(p => p.First_Name == First_Name && p.Last_Name == Last_Name);
            bool testStaff = db.Staffs.Any(p => p.First_Name == First_Name && p.Last_Name == Last_Name);
            if (!testStaff && !testCustomer)
            {
                yield return new ValidationResult("Invalid account", new[] { nameof(First_Name) });
                yield break;
            }

            if (testCustomer)
            {
                var tmp = db.Customers.Where(p => p.First_Name == First_Name && p.Last_Name == Last_Name).Select(p => new { p.PasswordHash, p.ID_Customer }).First();
                if (!tmp.PasswordHash.SequenceEqual(PasswordHash))
                {
                    yield return new ValidationResult("Invalid password", new[] { nameof(Password) });
                    yield break;
                }
                ID_Staff = null;
                ID_Customer = tmp.ID_Customer;
            }
            else
            {
                var tmp = db.Staffs.Where(p => p.First_Name == First_Name && p.Last_Name == Last_Name).Select(p => new { p.PasswordHash, p.ID_Staff }).First();
                if (!tmp.PasswordHash.SequenceEqual(PasswordHash))
                {
                    yield return new ValidationResult("Invalid password", new[] { nameof(Password) });
                    yield break;
                }

                ID_Staff = tmp.ID_Staff;
                ID_Customer = null;
            }

         
        }
    }
}