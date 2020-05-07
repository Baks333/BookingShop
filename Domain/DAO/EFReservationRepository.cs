using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFReservationRepository : IReservationRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Reservation> reservation
        {
            get { return context.Reservation; }
        }

        public void CreateReservation(Cart cart, BookingDetails bookingDetails)
        {
            Reservation reservation = new Reservation();
            foreach(var line in cart.Lines)
            {
                if(line.Hotel.Discount != 0)
                {
                    reservation.Sum += (line.Hotel.PriceForPerson * (1 - line.Hotel.Discount)) * bookingDetails.NightsAmount * bookingDetails.PersonAmount;
                } else
                reservation.Sum += line.Hotel.PriceForPerson * bookingDetails.NightsAmount * bookingDetails.PersonAmount;
            }
            reservation.FullName = bookingDetails.FullName;
            reservation.DateOfFiling = DateTime.Now;
            reservation.ArrivalDate = bookingDetails.ArrivalDate;
            reservation.Status = "ЗАЯВКА НА РАССМОТРЕНИИ";
            reservation.NightsAmount = bookingDetails.NightsAmount;
            reservation.PersonAmount = bookingDetails.PersonAmount;
            reservation.Email = bookingDetails.Email;
            context.Reservation.Add(reservation);
            context.SaveChanges();
        }


        public void AcceptReservation(Reservation reservation)
        {
            Reservation dbEntry = context.Reservation.Find(reservation.ReservationID);
            if (dbEntry != null)
            {
                dbEntry.Status = "БРОНЬ ПРИНЯТА";
            }
            context.SaveChanges();
        }

        public void DeclineReservation(Reservation reservation)
        {
            Reservation dbEntry = context.Reservation.Find(reservation.ReservationID);
            if (dbEntry != null)
            {
                dbEntry.Status = "БРОНЬ ОТКЛОНЕНА";
            }
            context.SaveChanges();
        }

    }
}
