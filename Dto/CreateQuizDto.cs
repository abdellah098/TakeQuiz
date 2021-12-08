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
        public string Tags { get; set; }
        public string Image { get; set; }
        public string QuizPassword { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{

        //    if (Name == null)
        //    {
        //        yield return new ValidationResult();
        //    }
        //}
    }
}
