using Microsoft.AspNetCore.Mvc;
using Quiz_back.Dto;
using Quiz_back.models;
using Quiz_back.Models;
using Microsoft.AspNetCore.Http;
using Quiz_back.services.interfaces;
using System.Linq;
using System.Collections.Generic;
using System;

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
                Tags = quizDto.Tag,
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

        [HttpPatch("/quiz/{id:Guid}")]
        public QuizCardDto PatchQuizInfos(PatchQuiz dto, Guid id)
        {
            var quiz = _quizService.Read(id);
            if (quiz == null)
            {
                return null;
            }

            quiz.Name = dto.Name;
            quiz.Description = dto.Description;
            quiz.Theme = (Theme)dto.Theme;

            return TransformQuizToCard(_quizService.Update(quiz));
        }

        private QuizCardDto TransformQuizToCard(Quiz quiz)
        {
            return new QuizCardDto
            {
                Id = quiz.Id,
                Name = quiz.Name,
                Status = (int)quiz.Status,
                Theme = (int)quiz.Theme,
                Description = quiz.Description,
                Image = quiz.Image, 
                Tags = quiz.Tags
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
                Tags = quizDto.Tag,
                Status = STATUT.DRAFT
            };
        }
    }
}

