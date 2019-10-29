using System;

namespace Seasons.ReadHost.Matches
{
    public class PlayerProgression
    {
        public PlayerProgression(Guid playerId, ProgressionEvent progressionEvent)
        {
            PlayerId = playerId;
            ProgressionEvent = progressionEvent;
        }

        public Guid PlayerId { get; }
        public ProgressionEvent ProgressionEvent { get; }
    }

    public enum ProgressionEvent
    {
        NominatedMostValuablePlayer, PlayerPassed, PlayerMadeCasualty, PlayerMadeTouchdown
    }
}