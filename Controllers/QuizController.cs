using Microsoft.AspNetCore.Mvc;
using Quiz_back.Dto;
using Quiz_back.models;
using Quiz_back.Models;
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

        [HttpPost("/Quiz/{id:Guid}/questions")]
        public bool PostQuestionAnswers(List<QuestionDto> questions, Guid id, [FromQuery] int status)
        {
            try
            {
                return _quizService.SaveQuizQuestions(questions, id, status);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet("/Quiz/{id:Guid}/questions")]
        public List<QuestionDto> GetQuestionAnswers(Guid id)
        {
            return _quizService.getQuestionAnswers(id);
        }

        [HttpGet("/Quiz/{id:Guid}/test-questions")]
        public Object GetQuizTestQuestions(Guid id)
        {
            var questions = _quizService.getQuestionAnswers(id).Select(question => {
                return new
                {
                    Id = question.Id,
                    Text = question.Text,
                    Answers = question.Answers.Select(answer => new { Id = answer.Id, Text = answer.Text, isCorrect = false})
                };
            });
            return questions;
        }

        [HttpPost("/Quiz/{id:Guid}/evaluate")]
        public QuizScoreDto EvaluateQuiz(Guid id, ResponseDto response) => _quizService.EvaluateQuiz(id, response);

        [HttpPost("/Quiz/{id:Guid}/unlock")]
        public bool LoginToQuiz(Guid id, UnlockQuizDto password) => _quizService.LoginToQuiz(id, password);

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

