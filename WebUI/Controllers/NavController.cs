using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private IHotelRepository repository;

        public NavController(IHotelRepository repo)
        {
            repository = repo;
        }
        
        public PartialViewResult Menu(string type = null)
        {
            ViewBag.SelectedType = type;

            IEnumerable<string> types = repository.Hotel
                .Select(hotel => hotel.City)
                .Distinct()
                .OrderBy(x => x);
            
            return PartialView("FlexMenu",types);
        }
    }
}