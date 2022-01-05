using System;
using System.Collections.Generic;

namespace Quiz_back.Dto
{
    public class ResponseDto
    {
        public string Pseudo { get; set; }
        public List<QuestionResponses> Questions { get; set; }

        public ResponseDto()
        {
            Questions = new List<QuestionResponses>();
        }
    }

    public class QuestionResponses
    {
        // Identifiant de la question
        public Guid Id { get; set; }
        public List<AnswerResponse> Answers { get; set; }
        public QuestionResponses()
        {
            Answers = new List<AnswerResponse>();
        }
    }

    public class AnswerResponse
    {
        // identifiant de la réponse
        public Guid Id { get; set; }
    }
}
