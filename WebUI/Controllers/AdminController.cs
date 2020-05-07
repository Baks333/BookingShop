using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        IHotelRepository repositoryH;
        IReservationRepository repositoryR;
        IReservationProcessor reservationProcessor;
        IUsersRepository repositoryU;

        public AdminController(IHotelRepository repoH, IReservationRepository repoR, IReservationProcessor processor, IUsersRepository repoU)
        {
            repositoryH = repoH;
            repositoryR = repoR;
            reservationProcessor = processor;
            repositoryU = repoU;
        }

        [Authorize]
        public ViewResult Users()
        {
            if (User.Identity.Name.Contains("Admin"))
            {
                return View(repositoryU.Users);
            }
            else return View("NotEnoughRoots");
        }

        [Authorize]
        public ViewResult AddUser()
        {
            if (User.Identity.Name.Contains("Admin"))
            {
                return View();
            }
            else return View("NotEnoughRoots");
        }

        [Authorize]
        public ViewResult UpdateUser(int userID)
        {
            if (User.Identity.Name.Contains("Admin"))
            {
                Users user = repositoryU.Users.FirstOrDefault(u => u.UserID == userID);
                return View(user);
            }
            else return View("NotEnoughRoots");
            
        }

        [HttpPost]
        public ActionResult UpdateUser(Users user)
        {
            if (ModelState.IsValid)
            {
                repositoryU.SaveUser(user);
                TempData["message"] = string.Format("Изменение информации о пользователе \"{0}\" сохранены", user.Login);
                return RedirectToAction("Users");
            }
            else
            {
                return View(user);
            }
        }

        [HttpPost]
        public ActionResult DeleteUser(Users user)
        {
            repositoryU.DeleteUser(user);
            return RedirectToAction("Users");
        }

        [HttpPost]
        public ActionResult AddUser(Users user)
        {
            repositoryU.CreateUser(user);
            return RedirectToAction("Users");
        }

        [Authorize]
        public ViewResult Reservations()
        {
            if (User.Identity.Name.Contains("Admin"))
            {
                return View(repositoryR.reservation);
            }
            else return View("NotEnoughRoots");
        }

        [HttpPost]
        public ActionResult AcceptReservation(Reservation reservation)
        {
            repositoryR.AcceptReservation(reservation);
            reservationProcessor.ProcessPositiveResponse(reservation);
            return RedirectToAction("Reservations");
        }

        [HttpPost]
        public ActionResult DeclineReservation(Reservation reservation)
        {
            repositoryR.DeclineReservation(reservation);
            reservationProcessor.ProcessNegativeResponse(reservation);
            return RedirectToAction("Reservations");
        }




        [Authorize]
        public ViewResult Edit(int hotelID)
        {
            Hotel hotel = repositoryH.Hotel.FirstOrDefault(h => h.HotelID == hotelID);
            return View(hotel);
        }


        [Authorize]
        public ViewResult Index()
        {
            return View(repositoryH.Hotel);
        }


        [HttpPost]
        public ActionResult Edit(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                repositoryH.SaveHotel(hotel);
                TempData["message"] = string.Format("Изменение информации об отеле \"{0}\" сохранены", hotel.Title);
                return RedirectToAction("Index");
            }
            else
            {
                return View(hotel);
            }
        }

        [HttpPost]
        public ActionResult Delete(Hotel hotel)
        {
                repositoryH.DeleteHotel(hotel);
                return RedirectToAction("Index");
        }

        [Authorize]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Hotel hotel)
        {
            repositoryH.CreateHotel(hotel);
            return RedirectToAction("Index");
        }
    }
}