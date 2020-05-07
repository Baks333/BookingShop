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
    public class CartController : Controller
    {

        private IHotelRepository hotelRepository;
        private IReservationProcessor reservationProcessor;
        private IReservationRepository reservationRepository;

        public CartController(IHotelRepository repoH, IReservationProcessor processor, IReservationRepository repoR)
        {
            hotelRepository = repoH;
            reservationProcessor = processor;
            reservationRepository = repoR;
        }

        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel
                {
                    Cart = cart,
                    ReturnUrl = returnUrl
                });
        }

        public RedirectToRouteResult AddToCart(Cart cart,int hotelID, string returnUrl)
        {
            Hotel hotel = hotelRepository.Hotel
                .FirstOrDefault(h => h.HotelID == hotelID);

            if (hotel != null)
            { 
                cart.AddItem(hotel);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Checkout()
        {
            return View(new BookingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, BookingDetails bookingDetails)
        {
             if (ModelState.IsValid)
            {
                reservationProcessor.ProcessReservation(cart, bookingDetails);
                reservationRepository.CreateReservation(cart, bookingDetails);
                return View("Completed");
            } else return View(new BookingDetails());
        }
    }
}