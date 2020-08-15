using System;
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
    [Route("api/posts/{postId}/comments")]
    [ApiController]
    //[Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public CommentsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("{commentId}", Name = "GetComment")]
        public async Task<IActionResult> GetComment(int commentId)
        {
            var comment = await _repository.Comment.GetCommentAsync(commentId);
            var commentDto = _mapper.Map<CommentDto>(comment);
            return Ok(commentDto);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetComments(int postId)
        {
            var commentsForPost = await _repository.Comment.GetCommentsForPostAsync(postId);
            var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(commentsForPost);
            return Ok(commentsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentForCreationDto commentForCreationDto)
        {
            var commentToCreate = _mapper.Map<Comment>(commentForCreationDto);
            var userFromDb = await _userManager.GetUserAsync(this.User);

            if (userFromDb.Id != commentForCreationDto.CommentedById)
                return Unauthorized();

            commentToCreate.CommentedById = commentForCreationDto.CommentedById;
            commentToCreate.CreatedOn = DateTime.Now;
            commentToCreate.PostId = commentForCreationDto.PostId;

            _repository.Comment.CreateComment(commentToCreate);
            await _repository.SaveAsync();

            var commentToReturn = _mapper.Map<CommentDto>(commentToCreate);

            //return CreatedAtAction("GetComment", new { postId, commentId = commentToReturn.Id }, commentToReturn);
            return Ok(commentToReturn); 
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var commentToDelete = await _repository.Comment.GetCommentAsync(commentId);
            _repository.Comment.DeleteComment(commentToDelete);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
