using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BookingApp.Repositories
{
    public class RoomRepository : IRepository<IRoom>
    {
        private List<IRoom> roomList;

        public RoomRepository()
        {
            roomList = new List<IRoom>();
        }

        public void AddNew(IRoom model)
        {
            roomList.Add(model);  
        }

        public IReadOnlyCollection<IRoom> All()
        {
            return roomList.AsReadOnly();
        }

        public IRoom Select(string criteria)
        {
            IRoom r = roomList.FirstOrDefault(x => x.GetType().Name == criteria);
            return r;
        }
    }
}
