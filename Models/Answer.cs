using Quiz_back.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz_back.Models
{
    public class Answer : EntityWithId
    {
        public string Text { get; set; }
        public Boolean IsCorrect { get; set; }
        public virtual Question Question { get; set; }
    }
}
