﻿using Graduation_Project.Domain.Repsitory.RefreshTokenRepo;
using Graduation_Project.Domain.Repsitory.TournamentRepo;
using Graduation_Project.Domain.Repsitory.TrainerRepo;
using Graduation_Project.Domain.Repsitory.UserRepo;

namespace Graduation_Project.Domain.Abstraction
{
    public interface IUnitOfWork
    {
        IRefreshTokenRepository RefreshTokenRepository { get; }
        IUserRepository UserRepository { get; }
        ITrainerRepository TrainerRepository { get; }
        ITrainerRatingRepository TrainerRatingRepository { get; }
        ITournamentRepository TournamentRepository { get; }

        //IRefreshTokenRepository RefreshTokenRepository { get; }

        Task<int> save();
    }
}
