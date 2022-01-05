using System.Collections.Generic;
namespace Quiz_back.Dto
{
    public class QuizScoreDto
    {
        public int Score { get; set; }
        public int NumberOfQuestion  { get; set; }
        public bool isRank { get; set; }

        public List<PlayerDTO> Players { get; set; }
        public QuizScoreDto()
        {
            Players = new List<PlayerDTO>();
        }
    }

    public class PlayerDTO
    {
        public string Name { get; set; }
        public int score { get; set; }

    }
}
