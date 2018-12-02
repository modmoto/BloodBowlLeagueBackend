using System;
using System.Collections.Generic;
using Domain.Teams.DomainErrors;
using Microwave.Domain;

namespace Domain.Teams
{
    public class AllowedPlayer
    {
        public AllowedPlayer(Guid playerTypeId, int maximumPlayers, GoldCoins cost, string playerDescription)
        {
            PlayerTypeId = playerTypeId;
            MaximumPlayers = maximumPlayers;
            Cost = cost;
            PlayerDescription = playerDescription;
        }

        public DomainResult CanUsePlayer(int ammount)
        {
            var moreThanMax = ammount < MaximumPlayers;

            if (!moreThanMax) return DomainResult.Error(new TeamFullError(MaximumPlayers));

            return DomainResult.Ok(new List<IDomainEvent>());
        }

        public Guid PlayerTypeId { get; }
        public int MaximumPlayers { get; }
        public GoldCoins Cost { get; }
        public string PlayerDescription { get; }
    }
}