using Graduation_Project.Domain.Abstraction;
using System.Text.Json.Serialization;

namespace Graduation_Project.Domain.Entity.TrainerDomain
{
    public class TrainerRating : Entity<TrainerRatingId>
    {
        public TrainerRating(TrainerRatingId id, TrainerId trainerId, Rating rating, string username) : base(id)
        {
            this.trainerId = trainerId;
            this.rating = rating;
            this.username = username;
        }

        public TrainerId trainerId { get;private set; }

        public Rating rating { get; private set; }
        public string username { get; private set; }

        public static TrainerRating Create(TrainerId trainerId, Rating rating,string username)
        {
            return new(TrainerRatingId.CreateUnique(),trainerId,rating,username);
        }

        
        



    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Rating {
        zero,
        one,
        two,
        three,
        four,
        five
    }
    
}
