using Quiz_back.Dto;
using Quiz_back.models;
using Quiz_back.repositories.interfaces;
using Quiz_back.services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Quiz_back.services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        public QuizService(IQuizRepository quizRepository)
        {
            this._quizRepository = quizRepository;
        }
        public Quiz Create(Quiz q) => _quizRepository.Create(q);
        public IEnumerable<Quiz> ReadAll() => _quizRepository.ReadAll();
        public Quiz Read(Guid Id) => _quizRepository.Read(Id);
        public Quiz Delete(Quiz q) => _quizRepository.Delete(q);
        public Quiz Update(Quiz q) => _quizRepository.Update(q);

        public IEnumerable<Quiz> SearchQuiz(int theme, int status, string names)
        {
            var searchResult = _quizRepository.ReadAll();
            if(theme != 0)
            {
                searchResult = searchResult.Where(quiz => (int)quiz.Theme == theme);
            }
            if (status != 0)
            {
                searchResult = searchResult.Where(quiz => (int)quiz.Status == status);
            }
            if(!string.IsNullOrEmpty(names))
            {
                searchResult = searchResult.Where(quiz => quiz.Name.ToLower() == names.ToLower() || quiz.Tags.ToLower().Contains(names.ToLower()));
            }
            return searchResult;
        }

        public bool SaveQuizQuestions(List<QuestionDto> questions, Guid quizId) => _quizRepository.SaveQuizQuestions(questions, quizId);
    }
}
