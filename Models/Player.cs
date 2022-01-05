using Quiz_back.models;
using System;

namespace Quiz_back.Models
{
    public class Player : EntityWithId
    {
        public string Name{ get; set; }
        public Guid QuizId { get; set; }
        public int Score  { get; set; }
    }
}
