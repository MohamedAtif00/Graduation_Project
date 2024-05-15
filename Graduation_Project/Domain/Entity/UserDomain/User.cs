using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TournamentDomain;
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
        public User(UserId id,string firstName,string secondName ,DateTime birthDate, string nationalId, string city, string phone, byte[] image ,Gender gender,TennisExp tennisExp ,DateTime startDay,TimeSession timeSession, HealthCondition healthCondition = null, TournamentId tournamentId = null) : base(id)
        {
            FirstName = firstName;
            SecondName = secondName;
            BirthDate = birthDate;
            NationalId = nationalId;
            City = city;
            Phone = phone;
            Image = image;
            Gender = gender;
            TennisExp = tennisExp;
            StartDay = startDay;
            TimeSession = timeSession;
            TournamentId = tournamentId;
            HealthCondition = healthCondition;
        }
        public string FirstName { get;private set; }
        public string SecondName { get;private set; }
        public DateTime BirthDate { get; private set; }
        public string NationalId { get; private set; }
        public string City { get; private set; }
        public string Phone { get; private set; }
        public byte[] Image { get; private set; }
        public Gender Gender { get; private set; }
        public TennisCourt? TennisCourt { get; private set; }
        public TennisExp TennisExp { get; private set; }
        public DateTime StartDay { get; private set; }
        public TimeSession TimeSession { get; private set; }
        public TrainerId? TrainerId { get; private set; }
        public TournamentId? TournamentId { get; private set; } 
        public HealthCondition? HealthCondition { get; private set; }

        public static User Create(Guid userId,string firstName,string secondName, DateTime birthDate, string nationalId, string city, string phone, byte[] image,Gender gender,TennisExp tennisExp,DateTime startDay,TimeSession timeSession,HealthCondition healthCondition = null)
        {
            return new(UserId.Create(userId),firstName,secondName, birthDate, nationalId, city, phone, image,gender,tennisExp,startDay,timeSession,healthCondition,null);
        }

        public void AddTrainerAndCourt(TennisCourt tennisCourt,TrainerId trainerId)
        {
            TennisCourt = tennisCourt;
            TrainerId = trainerId;
        }


        public void AddTournament(TournamentId tournamentId)
        { 
            this.TournamentId = tournamentId;
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
