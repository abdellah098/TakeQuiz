using Quiz_back.Models;
using Quiz_back.repositories.interfaces;
using System;
using System.Collections.Generic;


namespace Quiz_back.repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly AppDbContext _context;
        public AnswerRepository(AppDbContext context)
        {
            this._context = context;
        }

        public Answer Create(Answer answer)
        {
            var NewAnswers = _context.Answers.Add(answer);
            _context.SaveChanges();

            return NewAnswers.Entity;
        }

        public Answer Delete(Answer answer)
        {
            _context.Answers.Remove(answer);
            _context.SaveChanges();

            return answer;
        }

        public Answer Read(Guid Id) => _context.Answers.Find(Id);
       
        public IEnumerable<Answer> ReadAll() => _context.Answers;

        public Answer Update(Answer answer)
        {
            var UpdatedAnswer = _context.Answers.Update(answer);
            _context.SaveChanges();

            return UpdatedAnswer.Entity;
        }
    }
}
