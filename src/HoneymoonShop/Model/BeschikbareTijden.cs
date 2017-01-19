using System;
using System.ComponentModel.DataAnnotations;

namespace HoneyMoonDB.Models
{
    public class BeschikbareTijden
    {
        [Key]
        public int ID { get; set; }

        public TimeSpan tijd { get; set; }
    }
}




