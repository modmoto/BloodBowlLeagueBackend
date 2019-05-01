﻿using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Matches.Events
{
    public class MatchFinished : IDomainEvent
    {
        public MatchFinished(
            GuidIdentity matchId,
            IEnumerable<PlayerProgression> playerProgressions,
            GameResult gameResult)
        {
            MatchId = matchId;
            PlayerProgressions = playerProgressions;
            GameResult = gameResult;
        }

        public GuidIdentity MatchId { get; }
        public Identity EntityId => MatchId;
        public IEnumerable<PlayerProgression> PlayerProgressions { get; }
        public GameResult GameResult { get; }
    }

    public class PlayerProgression
    {
        public PlayerProgression(GuidIdentity playerId, IEnumerable<ProgressionEvent> progressionEvents)
        {
            PlayerId = playerId;
            ProgressionEvents = progressionEvents;
        }

        public GuidIdentity PlayerId { get; }
        public IEnumerable<ProgressionEvent> ProgressionEvents { get; }
    }

    public enum ProgressionEvent
    {
        NominatedMostValuablePlayer, PlayerPassed, PlayerMadeCasualty, PlayerMadeTouchdown
    }
}