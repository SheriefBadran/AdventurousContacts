using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdventurousContacts.Models
{
    [MetadataType(typeof(Contact_Metadata))]
    public partial class Contact
    {
        #region Auto implemented properties Now in Metadata class instead.

        //// ContactID is not set, ContactID has value 0. 0-value indicates insert new contact.
        //public int ContactID { get; set; }

        //[Required(ErrorMessage = "E-Post måste anges.")]
        //[StringLength(50)]
        //[EmailAddress(ErrorMessage = "En giltig E-Post måste anges.")]
        //public string EmailAddress { get; set; }

        //[Required(ErrorMessage = "Ett förnamn måste anges.")]
        //[StringLength(50)]
        //public string FirstName { get; set; }

        //[Required(ErrorMessage = "Ett efternamn måste anges.")]
        //[StringLength(50)]
        //public string LastName { get; set; }


        #endregion

        public class Contact_Metadata 
        {
            // ContactID is not set, ContactID has value 0. 0-value indicates insert new contact.
            public int ContactID { get; set; }

            [Required(ErrorMessage = "Email address is required.")]
            [StringLength(50)]
            [EmailAddress(ErrorMessage = "Please enter a valid email address")]
            public string EmailAddress { get; set; }

            [Required(ErrorMessage = "First name is required.")]
            [StringLength(50)]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Last name is required.")]
            [StringLength(50)]
            public string LastName { get; set; }
        }
    }
}