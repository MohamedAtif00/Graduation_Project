﻿using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Entity.UserDomain;

namespace Graduation_Project.Application.CQRS.UserFeature.ValidateUser
{
    public record ValidateUserQuery(DateTime startDay, TimeSession timeSession) :IQuery<bool>;
    
    
}
