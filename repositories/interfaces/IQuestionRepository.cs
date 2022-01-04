using Quiz_back.Models;
using System;
using System.Collections.Generic;

namespace Quiz_back.repositories.interfaces
{
    public interface IQuestionRepository
    {
        public Question Create(Question question);
        public IEnumerable<Question> ReadAll();
        public Question Read(Guid Id);
        public Question Update(Question question);
        public Question Delete(Question question);
    }
}
