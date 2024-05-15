using Graduation_Project.Domain.Abstraction;
using Microsoft.AspNetCore.Identity;

namespace Graduation_Project.Domain.Entity.TrainerDomain
{
    public class Trainer :Entity<TrainerId>
    {
        public Trainer(TrainerId id, string username, DateTime birthDate, int experience, string specialization, string phone, string email, byte[] image, double? price) : base(id)
        {
            Username = username;
            BirthDate = birthDate;
            Experience = experience;
            Specialization = specialization;
            Phone = phone;
            Email = email;
            this.image = image;
            this.price = price;
        }
        public string Username { get;private set; }
        public byte[] image { get; private set; }
        public DateTime BirthDate { get;private set; }
        public double? AvgRating { get; private set; }
        public double? price { get; private set; }
        public int Experience { get;private set; }
        public string Specialization { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public static Trainer Create(string username, byte[] image,DateTime birthdate,int exp,string specia,string phone,string email,double price)
        {
            return new(TrainerId.CreateUnique(),username,birthdate,exp,specia,phone,email,image,price);
        }

        public void Update(string username, byte[] image, DateTime birthdate, int exp, string specia, string phone, string email,double price)
        {
            Username = username;
            BirthDate = birthdate;
            Experience = exp;
            Specialization = specia;
            Phone = phone;
            Email = email;
            this.price = price;
            this.image = image;
        }

        public void ModifyAvgRating(double avg)
        {
            AvgRating = avg;
        }



    }


}
