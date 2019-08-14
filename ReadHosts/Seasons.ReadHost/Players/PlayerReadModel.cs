using Microwave.Domain.Identities;
using Microwave.Queries;
using Seasons.ReadHost.Players.Events;

namespace Seasons.ReadHost.Players
{
    public class PlayerReadModel : ReadModel<PlayerCreated>,
        IHandle<PlayerCreated>
    {
        public GuidIdentity PlayerId { get; private set; }
        public GuidIdentity TeamId { get; private set; }
        public StringIdentity PlayerTypeId{ get; private set; }

        public string Name { get; private set; }

        public void Handle(PlayerCreated domainEvent)
        {
            PlayerId = domainEvent.PlayerId;
            TeamId = domainEvent.TeamId;
            PlayerTypeId = domainEvent.PlayerTypeId;
            Name = domainEvent.Name;
        }
    }
}