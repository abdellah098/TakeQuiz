using Quiz_back.Models;
using Quiz_back.repositories.interfaces;
using System;
using System.Collections.Generic;

namespace Quiz_back.repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _context;
        public QuestionRepository(AppDbContext context)
        {
            this._context = context;
        }
        public Question Create(Question question)
        {
            var NewQuestion= _context.Questions.Add(question);
            _context.SaveChanges();

            return NewQuestion.Entity;
        }

        public Question Delete(Question question)
        {
            _context.Questions.Remove(question);
            _context.SaveChanges();

            return question;
        }

        public Question Read(Guid Id) => _context.Questions.Find(Id);

        public IEnumerable<Question> ReadAll() => _context.Questions;
       
        public Question Update(Question question)
        {
             var UpdatedQuestion = _context.Questions.Update(question);
            _context.SaveChanges();

            return UpdatedQuestion.Entity;
        }
    }
}
