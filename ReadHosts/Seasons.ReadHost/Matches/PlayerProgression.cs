using Microwave.Domain.Identities;

namespace Seasons.ReadHost.Matches
{
    public class PlayerProgression
    {
        public PlayerProgression(GuidIdentity playerId, ProgressionEvent progressionEvent)
        {
            PlayerId = playerId;
            ProgressionEvent = progressionEvent;
        }

        public GuidIdentity PlayerId { get; }
        public ProgressionEvent ProgressionEvent { get; }
    }

    public enum ProgressionEvent
    {
        NominatedMostValuablePlayer, PlayerPassed, PlayerMadeCasualty, PlayerMadeTouchdown
    }
}