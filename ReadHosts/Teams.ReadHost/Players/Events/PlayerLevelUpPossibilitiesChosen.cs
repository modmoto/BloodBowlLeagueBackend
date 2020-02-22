using System;
using Microwave.Queries;

namespace Teams.ReadHost.Players.Events
{
    public class PlayerLevelUpPossibilitiesChosen : ISubscribedDomainEvent
    {
        public PlayerLevelUpPossibilitiesChosen(
            Guid playerId,
            FreeSkillPoint newFreeSkillPoint)
        {
            NewFreeSkillPoint = newFreeSkillPoint;
            PlayerId = playerId;
        }

        public string EntityId => PlayerId.ToString();
        public FreeSkillPoint NewFreeSkillPoint { get; }
        public Guid PlayerId { get; }
    }
}