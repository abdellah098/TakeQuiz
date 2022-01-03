using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz_back.Dto
{
    public class CreateQuizDto //: IValidatableObject
    {
        public string Name { get; set; }
        public int Theme { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public string Image { get; set; }
        public string QuizPassword { get; set; }

    }
}
