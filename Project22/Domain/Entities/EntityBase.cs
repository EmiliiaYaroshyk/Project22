using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project22.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase() => DataAdded = DateTime.UtcNow;

        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Назва (заголовок)")]
        public virtual string Title { get; set; }
      
        [Display(Name = "Короткий опис")]
        public virtual string Subtitle { get; set; }

        [Display(Name = "Повний опис")]
        public virtual string Text { get; set; }
      
        [Display(Name = "Титульна картинка")]
        public virtual string TitleImagePath { get; set; }

        [Display(Name = "SEO мататег Title")]
        public virtual string MetaTitle { get; set; }

        [Display(Name = "SEO мататег Description")]
        public virtual string MetaDescription { get; set; }

        [Display(Name = "SEO мататег Keywords")]
        public virtual string MetaKeywords { get; set; }

        [DataType(DataType.Time)]
        public DateTime DataAdded { get; set; }

    }
}
