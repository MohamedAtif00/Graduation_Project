using Graduation_Project.Domain.Abstraction;
using Microsoft.AspNetCore.Identity;

namespace Graduation_Project.Domain.Entity.TrainerDomain
{
    public class Trainer :Entity<TrainerId>
    {
        public Trainer(TrainerId id, DateTime birthDate, int experience, string specialization, string phone, string email) : base(id)
        {
            BirthDate = birthDate;
            Experience = experience;
            Specialization = specialization;
            Phone = phone;
            Email = email;
        }
        public DateTime BirthDate { get;private set; }
        public int Experience { get;private set; }
        public string Specialization { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }

        public static Trainer Create(DateTime birthdate,int exp,string specia,string phone,string email)
        {
            return new(TrainerId.CreateUnique(),birthdate,exp,specia,phone,email);
        }

        public void Update()
        {
            Experience = Experience;
            Specialization = Specialization;
            Phone = Phone;
            Email = Email;
        }
    }
}
