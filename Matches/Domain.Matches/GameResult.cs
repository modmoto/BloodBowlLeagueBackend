using Microwave.Domain;

namespace Domain.Matches
{
    public class GameResult
    {
        public bool IsDraw { get; }
        public TrainerGameResult WinnerId { get; }
        public TrainerGameResult LooserId { get; }

        private GameResult(bool isDraw, TrainerGameResult winnerId, TrainerGameResult looserId)
        {
            IsDraw = isDraw;
            WinnerId = winnerId;
            LooserId = looserId;
        }

        public static GameResult Draw()
        {
            return new GameResult(true, null, null);
        }

        public static GameResult WinResult(TrainerGameResult winner, TrainerGameResult looser)
        {
            return new GameResult(false, winner, looser);
        }
    }

    public class TrainerGameResult
    {
        public Identity TrainerId { get; }
        public long TouchDowns { get; }

        public TrainerGameResult(Identity trainerId, long touchDowns)
        {
            TrainerId = trainerId;
            TouchDowns = touchDowns;
        }
    }
}