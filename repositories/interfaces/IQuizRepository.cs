using Quiz_back.models;
using System;
using System.Collections.Generic;

namespace Quiz_back.repositories.interfaces
{
    public interface IQuizRepository 
    {
        public Quiz Create(Quiz q); 
        public IEnumerable<Quiz> ReadAll();
        public Quiz Read(Guid Id);
        public Quiz Update(Quiz q);
        public Quiz Delete(Quiz q);
    }
}
