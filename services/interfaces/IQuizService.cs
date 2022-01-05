using Quiz_back.models;
using System.Collections.Generic;
using System;
using Quiz_back.Dto;

namespace Quiz_back.services.interfaces
{
    public interface IQuizService
    {
        public Quiz Create(Quiz q);
        public IEnumerable<Quiz> ReadAll();
        public Quiz Read(Guid Id);
        public Quiz Update(Quiz q);
        public Quiz Delete(Quiz q);
        public IEnumerable<Quiz> SearchQuiz(int theme, int status, string names);
        public bool SaveQuizQuestions(List<QuestionDto> question, Guid quizId, int status);
        public List<QuestionDto> getQuestionAnswers(Guid quizId);

        public QuizScoreDto EvaluateQuiz(Guid quizId, ResponseDto testResponse);

        public bool LoginToQuiz(Guid quizId, UnlockQuizDto quizPassword);
    }
}
