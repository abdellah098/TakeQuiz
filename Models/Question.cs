using Quiz_back.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz_back.Models
{
    public class Question : EntityWithId
    {
        public string Text { get; set; }
        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public virtual Quiz Quiz { get; set; }
    }
}
