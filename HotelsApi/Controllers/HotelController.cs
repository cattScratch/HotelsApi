using HotelsApi.Entities;
using HotelsApi.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        public HotelController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet()]
        public async Task<List<Hotel>> GetAllHotels()
        {
            var hotel = await databaseContext.Hotels.ToListAsync();
            return hotel;
        }

        [HttpGet("{id}")]
        public async Task<Hotel?> GetHotelById([FromRoute] int id)
        {
            var hotel = await databaseContext.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);
            return hotel;
        }

        [HttpPost()]
        public async Task<Hotel> CreateHotel([FromRoute] int id, [FromBody] Hotel hotel)
        {
            databaseContext.Hotels.Add(hotel);
            await databaseContext.SaveChangesAsync();
            return hotel;
        }

        [HttpPut("{id}")]
        public async Task<Hotel?> UpdateHotel([FromRoute] int id, [FromBody] Hotel hotel)
        {
            var hotelRecord = await databaseContext.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);

            if(hotelRecord == null) 
            {
                return null;
            }
            hotelRecord.HotelName = hotel.HotelName;
            hotelRecord.HotelCode = hotel.HotelCode;
            hotelRecord.HotelDescription = hotel.HotelDescription;

            await databaseContext.SaveChangesAsync();

            return hotelRecord;
        }
        [HttpDelete("{id}")]
        public async Task<bool?> DeleteHotel([FromRoute] int id) 
        {
            var hotelRecord = await databaseContext.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);

            if(hotelRecord == null)   
            {
                return null;    
            }
            databaseContext.Hotels.Remove(hotelRecord);

            await databaseContext.SaveChangesAsync();

            return true;
        }
    }
}
