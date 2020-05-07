using System;
using Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Concrete;

namespace Domain.Entities
{
    public class Cart
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Hotel> Hotel
        {
            get { return context.Hotel; }
        }

        private List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }

        public void AddItem(Hotel hotel)
        {
            lineCollection.Clear();
            CartLine line = lineCollection
                .Where(b => b.Hotel.HotelID == hotel.HotelID)
                .FirstOrDefault();
            Hotel dbEntry = context.Hotel.Find(hotel.HotelID);

            if (line == null)
            {
                lineCollection.Add(new CartLine { Hotel = hotel});
            }
            context.SaveChanges();
        }



        public class CartLine
        {
            public Hotel Hotel { get; set; }
        }
    }
}
