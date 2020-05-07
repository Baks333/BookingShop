using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HotelController : Controller
    {
        private IHotelRepository repository;
        public int pageSize = 4;

        public HotelController(IHotelRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string city, int page = 1)
        {
            HotelListViewModel model = new HotelListViewModel()
            {
                Hotel = repository.Hotel
                .Where(h => city == null || h.City == city)
                .OrderBy(hotel => hotel.HotelID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = city == null ? 
                        repository.Hotel.Count() : 
                        repository.Hotel.Where(hotel => hotel.City == city).Count()
                },
                CurrentType = city
            };
            
            return View(model);
        }
    }
}