using HoneymoonShop.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HoneyMoonDB.Models {

    [Table("Afspraak")]
    public class Afspraak {

        [Key]
        public int AfspraakId { get; set; }

        [Required(ErrorMessage = "Naam is verplicht")]
        [Display(Name = "Voor- en achternaam*")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Datum is verplicht")]
        [DataType(DataType.Date, ErrorMessage = "Datum is verplicht")]
        [Display(Name = "Trouwdatum*")]
        public DateTime TrouwDatum { get; set; }

        //[Phone]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Telefoonnummer is ongeldig")]
        [Display(Name = "Telefoonnummer")]
        public int Telefoonnummer { get; set; }

        [Required(ErrorMessage = "E-mailadres is verplicht")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Voer een geldige e-mail in")]
        [Display(Name = "E-mailadres*")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "E-mailadres herhalen is verplicht")]
        //[EmailAddress(ErrorMessage = "Voer een geldige e-mail in")]
        [Display(Name = "E-mailadres herhalen*")]
        [Compare("Email", ErrorMessage = "E-mailadres is niet hetzelfde")]
        //[NotMapped]
        public string HerhaalEmail { get; set; }

        public bool Nieuwsbrief { get; set; }
        public DateTime AfspraakDatum { get; set; }
        public int Tijd { get; set; }
        [ForeignKey("Tijd")]
        public BeschikbareTijden tijdFK { get; set; }
       
    }    

}
