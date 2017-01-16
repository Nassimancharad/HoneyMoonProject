using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HoneymoonShop.Model
{
    public class Admin
    {
        public int AdminId { get; set; }
        [Required]
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
