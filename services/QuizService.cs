using Quiz_back.Dto;
using Quiz_back.models;
using Quiz_back.Models;
using Quiz_back.repositories.interfaces;
using Quiz_back.services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Quiz_back.services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IPlayerRepository _playerRepository;
        public QuizService(IQuizRepository quizRepository, IPlayerRepository playerRepository)
        {
            this._quizRepository = quizRepository;
            this._playerRepository = playerRepository;
        }
        public Quiz Create(Quiz q) => _quizRepository.Create(q);
        public IEnumerable<Quiz> ReadAll() => _quizRepository.ReadAll();
        public Quiz Read(Guid Id) => _quizRepository.Read(Id);
        public Quiz Delete(Quiz q) => _quizRepository.Delete(q);
        public Quiz Update(Quiz q) => _quizRepository.Update(q);

        public IEnumerable<Quiz> SearchQuiz(int theme, int status, string names)
        {
            var searchResult = _quizRepository.ReadAll();
            if(theme != 0)
            {
                searchResult = searchResult.Where(quiz => (int)quiz.Theme == theme);
            }
            if (status != 0)
            {
                searchResult = searchResult.Where(quiz => (int)quiz.Status == status);
            }
            if(!string.IsNullOrEmpty(names))
            {
                searchResult = searchResult.Where(quiz => quiz.Name.ToLower() == names.ToLower() || quiz.Tags.ToLower().Contains(names.ToLower()));
            }
            return searchResult;
        }

        public bool SaveQuizQuestions(List<QuestionDto> questions, Guid quizId, int status) => _quizRepository.SaveQuizQuestions(questions, quizId, status);

        public List<QuestionDto> getQuestionAnswers(Guid quizId)
        {
            var quiz = Read(quizId);
            if( quiz == null)
            {
                return null;
            }

            return quiz.Questions.Select(question =>
            {
               return new QuestionDto
               {
                   Id = question.Id,
                   Text = question.Text,
                   Answers = question.Answers.Select(answer => new AnswerDTO { Id = answer.Id, Text = answer.Text, isCorrect = answer.IsCorrect }).ToList()
               };
            }).ToList();
        }

        public QuizScoreDto EvaluateQuiz(Guid quizId, ResponseDto testResponse)
        {
            var quiz = Read(quizId);
            if (quiz == null)
            {
                return new QuizScoreDto { Score = 0, NumberOfQuestion = 0 };
            }
            var score = new QuizScoreDto();
            score.NumberOfQuestion = quiz.Questions.Count;

            bool isCorrect = true;
            foreach (var question in quiz.Questions)
            {
                var questionAnswered = testResponse.Questions.Find(q => q.Id == question.Id);

                var answers = question.Answers.Where(answers => answers.IsCorrect).ToList();

                if (questionAnswered.Answers.Count != answers.Count)
                {
                    score.Score += 0;
                }
                else
                {
                    foreach (var answer in questionAnswered.Answers)
                    {
                        if (!answers.Select(answer => answer.Id).ToList().Contains(answer.Id))
                        {
                            isCorrect = false;
                        }
                    }

                    score.Score = isCorrect == false ? score.Score + 0 : score.Score + 1;
                }

            }

            // classement des players
            score.isRank =  RankPlayers(quizId, score.Score, testResponse.Pseudo);
            score.Players = _playerRepository.ReadAll().OrderByDescending(player => player.Score).Select(player =>
            {
                return new PlayerDTO { Name = player.Name, score = player.Score };
            }).ToList();

            return score;
        }

        public bool LoginToQuiz(Guid quizId, UnlockQuizDto quizPassword)
        {
            var quiz = Read(quizId);
            if(quiz == null || string.IsNullOrEmpty(quizPassword.QuizPassword))
            {
                return false;
            }
            char[] charsToTrim = { ' ' };
            return quizPassword.QuizPassword.Trim(charsToTrim) == quiz.QuizPassword.Trim(charsToTrim);
               
        }
       
        private bool RankPlayers( Guid quizId, int score, string pseudo)
        {
            var players = _playerRepository.ReadAll().ToList();
            if(players.Count <= 3)
            {
                // le premier Jouer
                _playerRepository.Create(new Player { Name = pseudo, Score = score, QuizId = quizId });

                return true;
            }
            else
            {
                players = players.OrderBy(player => player.Score).ToList();
                Player playerToReplace = null;

                foreach(var player in players)
                {
                    if(score > player.Score)
                    {
                        playerToReplace = player;
                        break;
                    }
                }
                if(playerToReplace != null)
                {
                    _playerRepository.Delete(playerToReplace);
                    _playerRepository.Create(new Player { Name = pseudo, Score = score, QuizId = quizId });

                    return true;
                }

                return false;
            }
        }
    }
}
