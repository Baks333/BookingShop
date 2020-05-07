using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IReservationProcessor
    {
        void ProcessReservation(Cart cart, BookingDetails bookingDetails);
        void ProcessPositiveResponse(Reservation reservation);
        void ProcessNegativeResponse(Reservation reservation);
    }
}
