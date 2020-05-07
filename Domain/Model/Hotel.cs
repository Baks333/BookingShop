using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Domain.Entities
{
    public class Hotel
    {
        [Key]
        [HiddenInput(DisplayValue=false)]
        [Display(Name = "ID")]
        public int HotelID { get; set; }

        [Display(Name="Название")]
        [Required(ErrorMessage="Пожалуйста, введите название")]
        public string Title { get; set; }

        [Display(Name = "Город")]
        [Required(ErrorMessage = "Пожалуйста, укажите город")]
        public string City { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание")]
        public string Description { get; set; }


        [Display(Name = "Количество мест")]
        [Required(ErrorMessage = "Пожалуйста, введите количество")]
        public int Size { get; set; }


        [Display(Name = "Цена (руб)")]
        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage = "Пожалуйста, введите цену на человека")]
        public decimal PriceForPerson { get; set; }

        [Display(Name = "Скидка")]
        [Range(0.01, double.MaxValue)]
        public decimal Discount { get; set; }
    }
}
