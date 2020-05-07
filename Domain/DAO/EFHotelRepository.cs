using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFHotelRepository : IHotelRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Hotel> Hotel
        {
            get { return context.Hotel; }
        }


        public void SaveHotel(Hotel hotel)
        {
            if (hotel.HotelID == 0)
            {
                context.Hotel.Add(hotel);
            }
            else
            {
                Hotel dbEntry = context.Hotel.Find(hotel.HotelID);
                if (dbEntry != null)
                {
                    dbEntry.Title = hotel.Title;
                    dbEntry.City = hotel.City;
                    dbEntry.Size = hotel.Size;
                    dbEntry.PriceForPerson = hotel.PriceForPerson;
                    dbEntry.Description = hotel.Description;
                    dbEntry.Discount = hotel.Discount;
                }
            }
            context.SaveChanges();
        }

        public void DeleteHotel(Hotel hotel)
        {
            context.Hotel.Attach(hotel);
            context.Entry(hotel).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
        }

        public void CreateHotel(Hotel hotel)
        {
            context.Hotel.Add(hotel);
            context.SaveChanges();
        }
    }
}
