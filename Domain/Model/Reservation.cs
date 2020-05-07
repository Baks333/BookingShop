using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Reservation
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public int ReservationID { get; set; }

        [Display(Name = "ФИО клиента")]
        public string FullName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Дата отправки заявки")]
        public DateTime DateOfFiling { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Дата приезда")]
        public DateTime ArrivalDate { get; set; }

        [Display(Name = "Количество ночей")]
        public int NightsAmount { get; set; }

        [Display(Name = "Количество человек")]
        public int PersonAmount { get; set; }

        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        [Display(Name = "Общая стоимость")]
        public decimal Sum { get; set; }

        [Display(Name = "Статус заявки")]
        public string Status { get; set; }

    }
}
