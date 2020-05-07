using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IHotelRepository
    {
        IEnumerable<Hotel> Hotel { get; }
        void SaveHotel(Hotel hotel);
        void DeleteHotel(Hotel hotel);
        void CreateHotel(Hotel hotel);
    }
}
