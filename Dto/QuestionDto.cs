using System;
using System.Collections.Generic;
namespace Quiz_back.Dto
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool  isNew { get; set; }
        public List<AnswerDTO> Answers { get; set; }

        public QuestionDto()
        {
            Answers = new List<AnswerDTO>();
        }
    }
}
