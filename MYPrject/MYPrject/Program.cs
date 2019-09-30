using MyClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClassLibrary;

namespace MYPrject
{
    class Program
    {
        public static void Delete(int id)
        {
            using (MyContext context = new MyContext())
            {
                Users user = context.Set<Users>().FirstOrDefault(i=>i.User_ID==id);
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
            }
        }
        public static void ChangeName(string fName, string lName, int id)
        {
            using (MyContext context = new MyContext())
            {
                var resaut = context.Set<Users>().SingleOrDefault(i => i.User_ID == id);
                if (resaut != null)
                {
                    resaut.Firstname = fName;
                    resaut.Lastname = lName;
                    context.SaveChanges();
                    var users = context.Users.ToList();
                    foreach (var u in users)
                        Console.WriteLine("{0} - {1} - {2}", u.User_ID, u.Firstname, u.Lastname);
                }
            }
        }
        public static void Add(Users user)
        {
            using (MyContext context = new MyContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
                var users = context.Users.ToList();
                foreach (var u in users )
                    Console.WriteLine("{0} - {1} - {2} - {3}", u.User_ID, u.Firstname, u.Lastname,u.Bithday);
            }
        }
        public IEnumerable<Users> GetUsersWithChar(char firstInitial)
        {
            using (MyContext context = new MyContext())
            {
                string initial = firstInitial.ToString();
                IEnumerable<Users> result = context.Set<Users>().Select(i => i).Where(i => i.Firstname.StartsWith(initial));
                var users = context.Users.ToList();
                foreach (var u in users)
                    Console.WriteLine("{0} - {1} - {2} - {3}", u.User_ID, u.Firstname, u.Lastname, u.Bithday);
                return result;
                
            }
        }

        public decimal GetCost(int RoomId)
        {
            using (MyContext context = new MyContext())
            {
                var myresult = from room in context.Room
                                join Roomfurnitur in context.R_F on room.Room_ID equals Roomfurnitur.Room_ID
                                group Roomfurnitur by room into gr
                                select gr.Key.price+gr.Sum(i=>i.Count * i.Furnitur.HourlyCost)
                                ;
               return myresult.FirstOrDefault();
                
            }
        }


        public MyReservationsModel[] GetUserReservations()
        {
            using(MyContext context=new MyContext())
            {
                    var myresault = from users in context.Users
                                    join booking in context.Booking on users.User_ID equals booking.User_ID
                                    group booking by users into gr1
                                    select new MyReservationsModel
                                    {
                                        Name = gr1.Key.Firstname.ToString(),
                                        MyBalance=gr1.Key.Balance,
                                        MyBookings=gr1.Select(i=>new ReservationsModel
                                        {
                                            Room = i.Room_ID,
                                            StartTime = i.Starting,
                                            EndTime=i.Ending,
                                        }),
                                    };
                var result = myresault.ToArray();
                return result;
            }
        }
        
        public static void Bookme(int id)
        {
            using (MyContext context = new MyContext())
            {
                var result = from booking in context.Booking
                             join userbook in context.U_R on booking.ID equals userbook.ID
                             group userbook by booking into gr1
                             join user in context.Users on gr1.Key.User_ID equals user.User_ID
                             join room in context.Room on gr1.Key.Room_ID equals room.Room_ID
                             group new {u=user, r=room} by gr1 into gr2
                             join roomfurn in context.R_F on gr2.Key.Key.Room_ID equals roomfurn.Room_ID
                             join furnitur in context.Furnitur on roomfurn.Furn_ID equals furnitur.Furn_ID 
                             group new {rf=roomfurn,f=furnitur} by gr2 into gr
                             select new
                             {
                                 Name=gr.Key.Key.Key.User.Firstname,
                                 Surname = gr.Key.Key.Key.User.Lastname,
                                 Room_number = gr.Key.Select(i=>i.r.Room_ID),
                                 Furniturpr=gr.Key.Key.Key.Furn,
                                 Start=gr.Key.Key.Key.Starting,
                                 End=gr.Key.Key.Key.Ending,
                                 User_Count= gr.Key.Key.Count(),
                             };
                context.SaveChanges();
            }

        }
        static void Main(string[] args)
        {
            DateTime time = new DateTime();
            Users user = new Users();
            user.User_ID = 1;
            user.Firstname = "sargis";
            user.Lastname = "pet";
            user.Bithday = time;
            Add(user);
        }
    }
}
