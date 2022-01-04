using Quiz_back.Models;
using System;
using System.Collections.Generic;

namespace Quiz_back.repositories.interfaces
{
    public interface IAnswerRepository
    {
        public Answer Create(Answer answer);
        public IEnumerable<Answer> ReadAll();
        public Answer Read(Guid Id);
        public Answer Update(Answer answer);
        public Answer Delete(Answer answer);
    }
}
