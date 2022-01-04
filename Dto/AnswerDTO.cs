using System;
using System.Collections.Generic;
namespace Quiz_back.Dto
{
    public class AnswerDTO
    {
        public Guid  Id { get; set; }
        public string Text { get; set; }
        public bool isCorrect { get; set; }
        public bool isNew { get; set; }
    }
}
