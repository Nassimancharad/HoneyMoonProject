using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HoneymoonShop.Model.AppointmentModels
{
    public enum AppointmentType { TrouwjurkenPassen, TrouwpakkenPassen, AfspeldAfspraak}
    public class Appointment
    {
        public int AppointmentId { get; set; }
        [Required, Display( Name="Naam")]
        public string Name { get; set; }
        [Required, HiddenInput]
        public DateTime AppointmentTime { get; set; }
        [Required, Display(Name = "Trouwdatum")]
        public DateTime WeddingDate { get; set; }
        [MaxLength(15), Phone, Display( Name="Telefoonnummer")]
        public string PhoneNumber { get; set; }
        [Required, EmailAddress, Display(Name = "Emailadres")]
        public string Email { get; set; }
        [Required]
        public AppointmentType Type { get; set; }

    }
}
