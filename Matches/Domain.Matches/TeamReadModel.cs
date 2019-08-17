using System.Collections.Generic;
using System.Linq;
using Domain.Matches.ForeignEvents;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Domain.Matches
{
    public class TeamReadModel : ReadModel<TeamCreated>,
        IHandle<PlayerBought>,
        IHandle<TeamCreated>
    {
        public GuidIdentity TeamId { get; private set; }
        public IEnumerable<GuidIdentity> Players { get; private set; } = new List<GuidIdentity>();

        public bool IsFinished { get; private set; }

        public void Handle(PlayerBought domainEvent)
        {
            Players = Players.Append(domainEvent.PlayerId);
        }

        public void Handle(TeamCreated domainEvent)
        {
            TeamId = domainEvent.TeamId;
        }

    }
}