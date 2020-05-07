using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookingDetails
    {
        [Required(ErrorMessage = "Укажите Ваше ФИО")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Дата приезда")]
        [Display(Name="Дата приезда")]
        public DateTime ArrivalDate { get; set; }

        [Required(ErrorMessage = "Укажите количество ночей")]
        [Display(Name = "Количество ночей")]
        public int NightsAmount{ get; set; }

        [Required(ErrorMessage = "Укажите количество человек")]
        [Display(Name = "Количество человек")]
        public int PersonAmount { get; set; }

        [Required(ErrorMessage = "Укажите электронную почту")]
        [Display(Name = "Почта")]
        public string Email { get; set; }
    }
}
