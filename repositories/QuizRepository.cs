using Quiz_back.Dto;
using Quiz_back.models;
using Quiz_back.Models;
using Quiz_back.repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quiz_back.repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _context;
        public QuizRepository( AppDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Quiz> ReadAll()
        {
            return _context.Quiz;
        }
        public Quiz Create(Quiz q)
        {
            var NewQuiz = _context.Quiz.Add(q);
            _context.SaveChanges();

            return NewQuiz.Entity;
        }

        public Quiz Update(Quiz q)
        {
            var UpdatedQuiz = _context.Quiz.Update(q);
            _context.SaveChanges();

            return UpdatedQuiz.Entity;
        }

        public Quiz Delete(Quiz q)
        {
            _context.Quiz.Remove(q);
            _context.SaveChanges();

            return q;
        }

        public Quiz Read(Guid Id) => _context.Quiz.Find(Id);

        public bool SaveQuizQuestions(List<QuestionDto> questions, Guid quizId)
        {
            var quiz = _context.Quiz.Find(quizId);
            if (quiz != null)
            {
                var quizQuestions = new List<Question>();
                foreach (QuestionDto question in questions)
                {
                    var newQuestion = new Question
                    {
                        Text = question.Text,
                        Answers = question.Answers.Select(answer => new Answer { Text = answer.Text, IsCorrect = answer.isCorrect }).ToList()
                    };

                    quizQuestions.Add(newQuestion);
                }

                quiz.Questions.Clear();
                quiz.Questions = quizQuestions;

                _context.Quiz.Update(quiz);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
