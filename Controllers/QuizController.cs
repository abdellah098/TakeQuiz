using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quiz_back.Dto;
using Quiz_back.models;
using Quiz_back.Models;
using Quiz_back.services;
using Quiz_back.services.interfaces;
using System.Linq;
using System.Collections.Generic;
namespace Quiz_back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizController
    {
        private readonly IQuizService _quizService;

        public QuizController( IQuizService quizService)
        {
            this._quizService = quizService;
        }

        [HttpPost]
        public QuizCardDto Create(CreateQuizDto quizDto)
        {
            var quiz = new Quiz
            {
                Name = quizDto.Name,
                Theme = (Theme)quizDto.Theme,
                Description = quizDto.Description,
                Image  = quizDto.Image,
                QuizPassword = quizDto.QuizPassword,
                Tags = quizDto.Tags,
                Status = STATUT.DRAFT
            };

            return TransformQuizToCard(_quizService.Create(quiz));
           
        }

        [HttpGet]
        public List<QuizCardDto> GetListQuizCard() => _quizService.ReadAll().Select(quiz => TransformQuizToCard(quiz)).ToList();

        [HttpGet("search")]
        public List<QuizCardDto> SearchQuiz(
            [FromQuery] int theme,
            [FromQuery] int status,
            [FromQuery] string names)
            => _quizService.SearchQuiz(theme, status, names).Select(quiz => TransformQuizToCard(quiz)).ToList();
        
        private QuizCardDto TransformQuizToCard(Quiz quiz)
        {
            return new QuizCardDto
            {
                Name = quiz.Name,
                Status = (int)quiz.Status,
                Description = quiz.Description,
                Image = quiz.Image, 
            };
        }

        private Quiz TransformCreateQuizToQuiz(CreateQuizDto quizDto)
        {
            return new Quiz
            {
                Name = quizDto.Name,
                Theme = (Theme)quizDto.Theme,
                Description = quizDto.Description,
                Image = quizDto.Image,
                QuizPassword = quizDto.QuizPassword,
                Tags = quizDto.Tags,
                Status = STATUT.DRAFT
            };
        }
    }
}

