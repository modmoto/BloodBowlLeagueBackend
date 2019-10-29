using System;
using Microwave.Domain;
using Microwave.Domain.Validation;

namespace Domain.Matches.Errors
{
    public class TeamsCanNotBeTheSame : DomainError
    {
        public TeamsCanNotBeTheSame(Guid teamAtHome, Guid teamAsGuest)
            : base($"Teams can not be the same {teamAtHome} and {teamAsGuest}")
        {
        }
    }
}