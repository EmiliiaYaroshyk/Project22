using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project22.Domain.Entities
{
    public class ServiceItem : EntityBase
    {
        [Required(ErrorMessage = "Заповніть назву послуги")]
        [Display(Name = "Назва послуги")]
        public override string Title { get; set; }

        [Display(Name = "Короткий опис послуги")]
        public override string Subtitle { get; set; }

        [Display(Name = "Повний опис послуги")]
        public override string Text { get; set; }

    }
}
