﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/posts/{postId}/likes")]
    [ApiController]
    [Authorize]
    public class LikesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public LikesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Like(int postId)
        {
            var userFromDb = await _userManager.GetUserAsync(this.User);
            await _repository.Like.ProcessLikeAsync(postId, userFromDb.Id);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetLikesForPost(int postId) {
            var userFromDb = await _userManager.GetUserAsync(this.User);
            var likes = await _repository.Like.GetLikesForPostAsync(postId);
            
            var likesDto = new LikeDto() { 
                LikesCount = likes.Count(),
                CurrentUserLiked = likes.Any(l => l.LikerId == userFromDb.Id)
            };
            return Ok(likesDto);
        }
    }
}
