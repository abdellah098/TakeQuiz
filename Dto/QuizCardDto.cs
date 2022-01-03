
using System;
namespace Quiz_back.Dto
{
    public class QuizCardDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string Image { get; set; }
        public int  Theme { get; set; }
        public string Tags { get; set; }
    }
}
