using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project22.Domain.Entities
{
    public class TextField : EntityBase 
    {
        [Required]
        public string CodeWord { get; set; }

        [Display(Name = "Назва сторінки (заголовок)")]
        public override string Title { get; set; } = "Інфоормаційна сторінка";

        [Display(Name = "Вміст сторінки")]
        public override string Text { get; set; } = "Вміст заповнюється адміністратором";
    }
}
