﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechPulse.Models
{
    public class User
    {
        public int Id { get; set; } // Unique identifier for the user

        [Required(ErrorMessage = "Du måste fylla i fältet.")]
        [StringLength(50, ErrorMessage = "Namnet får inte vara längre än 50 tecken.")]
        [DisplayName("Användarnamn")]
        public string? Username { get; set; } // Username of the user

        [Required(ErrorMessage = "Du måste fylla i fältet.")]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress.")]
        [DisplayName("E-post")]
        [StringLength(100, ErrorMessage = "E-postadressen får inte vara längre än 100 tecken.")]
        public string? Email { get; set; } // Email address of the user

        [Required(ErrorMessage = "Du måste fylla i fältet.")]
        [DisplayName("Lösenord")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Lösenordet måste innehålla minst 8 tecken, inklusive stora och små bokstäver, siffror och specialtecken.")]
        public string? Password { get; set; } // Password for the user account

        [DisplayName("Profilbild")]
        public string? ProfileImageUrl { get; set; } // URL of the user's profile image

        [DisplayName("Telefonnummer")]
        [RegularExpression(@"^\+?[0-9]{1,3}[-. ]?\(?[0-9]{1,4}\)?[-. ]?[0-9]{1,4}[-. ]?[0-9]{1,9}$",
            ErrorMessage = "Ogiltigt telefonnummer, exempel: +46 70 123 54 eller 070-123 45 67.")]
        [StringLength(20, ErrorMessage = "Telefonnumret får inte vara längre än 20 tecken.")]
        public string? PhoneNumber { get; set; } // Phone number of the user

        [DisplayName("Adress")]
        public string? Address { get; set; }  // Address of the user

        [Required(ErrorMessage = "Du måste fylla i fältet.")]
        [DisplayName("Postnummer")]
        [RegularExpression(@"^\d{3} \d{2}$", ErrorMessage = "Fel format på postnummer. Ange 3 siffror, ett mellanslag och 2 siffror (xxx xx).")] 
        public string? PostalCode { get; set; } // Postal code of the user

        [Required(ErrorMessage = "Du måste fylla i fältet.")]
        [DisplayName("Stad")]
        public string? City { get; set; } // City of the user
       
        public List<PurchaseHistory>? PurchaseHistories { get; set; } // List of purchase histories associated with the user
    }
}
