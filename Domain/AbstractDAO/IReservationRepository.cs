using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> reservation { get; }
        void CreateReservation(Cart cart, BookingDetails bookingDetails);
        void AcceptReservation(Reservation reservation);
        void DeclineReservation(Reservation reservation);

    }
}
