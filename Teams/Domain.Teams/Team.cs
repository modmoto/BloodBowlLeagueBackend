using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Teams.DomainErrors;
using Domain.Teams.DomainEvents;
using Microwave.Domain;

namespace Domain.Teams
{
    [SnapShotAfter(3)]
    public class Team : Entity, IApply<TeamCreated>, IApply<PlayerBought>
    {
        public GuidIdentity Id { get; private set; }

        public GoldCoins TeamMoney { get; private set; }
        public IEnumerable<StringIdentity> PlayerTypes { get; private set; } = new List<StringIdentity>();
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; private set; } = new List<AllowedPlayer>();

        public static DomainResult Create(StringIdentity raceId, string teamName, string trainerName, IEnumerable<AllowedPlayer>
        allowedPlayers)
        {
            return DomainResult.Ok(new TeamCreated(GuidIdentity.Create(Guid.NewGuid()), raceId, teamName,
            trainerName, allowedPlayers, new GoldCoins(1000000)));
        }

        public DomainResult BuyPlayer(StringIdentity playerTypeId)
        {
            var player = AllowedPlayers.FirstOrDefault(ap => ap.PlayerTypeId.Equals(playerTypeId));
            if (player == null) return DomainResult.Error(new CanNotUsePlayerInThisRaceError(playerTypeId));
            int ammount = PlayerTypes.Count(p => p.Equals(playerTypeId));

            var canUsePlayer = player.CanUsePlayer(ammount);
            if (canUsePlayer.Failed) return DomainResult.Error(canUsePlayer.DomainErrors);

            if (!player.Cost.LessThan(TeamMoney))
                return DomainResult.Error(new FewMoneyInTeamChestError(player.Cost.Value, TeamMoney.Value));

            PlayerTypes = PlayerTypes.Append(playerTypeId);
            TeamMoney = TeamMoney.Minus(player.Cost);
            var playerBought = new PlayerBought(Id, playerTypeId, GuidIdentity.Create(),  TeamMoney);
            return DomainResult.Ok(playerBought);
        }

        public void Apply(TeamCreated teamCreated)
        {
            Id = (GuidIdentity) teamCreated.EntityId;
            AllowedPlayers = teamCreated.AllowedPlayers;
            TeamMoney = teamCreated.StartingMoney;
        }

        public void Apply(PlayerBought playerBought)
        {
            TeamMoney = playerBought.NewTeamChestBalance;
            PlayerTypes = PlayerTypes.Append(playerBought.PlayerTypeId);
        }
    }
}