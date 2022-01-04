using Quiz_back.Dto;
using Quiz_back.models;
using System;
using System.Collections.Generic;

namespace Quiz_back.repositories.interfaces
{
    public interface IQuizRepository 
    {
        public Quiz Create(Quiz question); 
        public IEnumerable<Quiz> ReadAll();
        public Quiz Read(Guid Id);
        public Quiz Update(Quiz question);
        public Quiz Delete(Quiz question);
        public bool SaveQuizQuestions(List<QuestionDto> question, Guid quizId);
    }
}
