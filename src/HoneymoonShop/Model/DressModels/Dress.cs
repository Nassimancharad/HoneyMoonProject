using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HoneymoonShop.Model.DressModels
{
    /// <summary>
    /// Flags: Enum value is an integer 
    /// Binary combined so 101 = 5 = Ivory, Color
    /// 111 = 7 = ivory, ivorycolor, color
    /// 100 = 1 = ivory
    /// </summary>
    [Flags]
    public enum Color
    {
        [Display(Name="Ivoor")]
        Ivory = 1,
        [Display(Name = "Ivoor met kleur")]
        IvoryColor = 2,
        [Display(Name = "Gekleurd")]
        Color = 4
    }

    public class Dress
    {
        public int DressId { get; set; }
        [MinLength(2), Required]
        public string Name { get; set; }
        [ForeignKey("Brand"), Required]
        public string BrandName { get; set; }
        public virtual Brand Brand { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int PriceIndication { get; set; }
        /// <summary>
        /// Navigation to DressProperties
        /// </summary>
        public virtual ICollection<DressProperty> Properties { get; set; }
        public virtual ICollection<DressCategory> Categories { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        [Required]
        public Color Colors { get; set; }
        public virtual ICollection<DressJewelry>  Jewelry { get; set; }
        // pictures 

        public void Update(Dress source)
        {
            this.Name = source.Name;
            this.BrandName = source.BrandName;
            this.Description = source.Description;
            this.PriceIndication = source.PriceIndication;
            this.Colors = source.Colors;
            // TODO: update many-relationships
        }

        public string GetMainImageUrl()
        {
            var imageUrl = Images.Count > 0 ? Images.First().DressURL : "https://placehold.it/350x150";
            return imageUrl;
        }
    }
}
