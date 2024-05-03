using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TrainerDomain;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Graduation_Project.Domain.Entity.UserDomain
{
    public class User : Entity<UserId>
    {
        public User() : base(UserId.CreateUnique())
        {
            
        }
        public User(UserId id, DateTime birthDate, string nationalId, string city, string phone, byte[] image ,Gender gender,TennisCourt tennisCourt ,TrainerId trainerId, HealthCondition healthCondition = null) : base(id)
        {
            BirthDate = birthDate;
            NationalId = nationalId;
            City = city;
            Phone = phone;
            Image = image;
            Gender = gender;
            TennisCourt = tennisCourt;
            TrainerId = trainerId;
            HealthCondition = healthCondition;
        }
        public DateTime BirthDate { get; private set; }
        public string NationalId { get; private set; }
        public string City { get; private set; }
        public string Phone { get; private set; }
        public byte[] Image { get; private set; }
        public Gender Gender { get; private set; }
        public TennisCourt TennisCourt { get; private set; }
        public TennisExp TennisExp { get; private set; }
        public TrainerId TrainerId { get; private set; }
        public TimeSessionId TimeSessionId { get; private set; }
        public HealthCondition? HealthCondition { get; private set; }

        public static User Create(Guid userId, DateTime birthDate, string nationalId, string city, string phone, byte[] image,Gender gender,TennisCourt tennisCourt, TrainerId trainerId, HealthCondition healthCondition = null)
        {
            return new(UserId.Create(userId), birthDate, nationalId, city, phone, image,gender,tennisCourt, trainerId, healthCondition);
        }
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TennisCourt {
        
        Grass,
        Clay,
        Hard
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TennisExp
    {
        Beginner,
        Intermediate,
        Advanced
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender 
    { 
        male,
        female,
        other
    }
}
