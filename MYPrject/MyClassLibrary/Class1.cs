using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary
{
    public class Users
    {
        [Key]
        public int User_ID { get; set; }
        [Required]
        [MaxLength(250)]
        public string Firstname { get; set; }
        [Required]
        [MaxLength(250)]
        public string Lastname { get; set; }
        [Required]
        public DateTime Bithday { get; set; }
        public decimal Balance {get;set;}
    }
    public class Booking
    {
        [Key]
        public int ID { get; set; }
        public int Room_ID { get; set; }
        public int User_ID { get; set; }
        public int Furn_ID { get; set; }
        public DateTime Starting { get; set; }
        public DateTime Ending { get; set; }
        [ForeignKey("Room_ID")]
        public Room room { get; set; }
        [ForeignKey("User_ID")]
        public Users User { get; set; }
        [ForeignKey("Furn_ID")]
        public Furnitur Furn { get; set; }
    }
    public class Room
    {
        [Key]
        public int Room_ID { get; set; }
        public int Furn_ID { get; set; }
        [Required]
        public decimal price { get; set; }
        [MaxLength(250)]
        public string County { get; set; }
        [MaxLength(250)]
        public string City { get; set; }
        public int floor { get; set; }
        [ForeignKey("Furn_ID")]
        public Furnitur Furnitur { get; set; }
    }
    public class Furnitur
    {
        [Key]
        public int Furn_ID { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal HourlyCost { get; set; }
    }
    public class Room_Furn
    {
        [Key, Column(Order = 1)]
        public int Room_ID { get; set; }
        [Key, Column(Order = 2)]
        public int Furn_ID { get; set; }
        [Required]
        public int Count { get; set; }
        [ForeignKey("Room_ID")]
        public Room Room { get; set; }

        [ForeignKey("Furn_ID")]
        public Furnitur Furnitur { get; set; }
    }
    public class User_Book
    {
        [Key, Column(Order = 1)]

        public int User_ID { get; set; }
        [Key, Column(Order = 2)]
        public int ID { get; set; }
        [Required]
        public int Count { get; set; }
        [ForeignKey("User_ID")]
        public Users User { get; set; }

        [ForeignKey("ID")]
        public Booking Booking { get; set; }
    }
   public class MyContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Furnitur> Furnitur { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Room_Furn> R_F { get; set; }
        public DbSet<User_Book> U_R { get; set; }

        public MyContext() : base("MyContext") { }
    }

}
