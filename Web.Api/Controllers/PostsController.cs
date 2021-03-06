﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Contracts;
using Entities.Configuration;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeatures;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Web.Api.ActionFilters;
using Web.Api.ModelBinders;

namespace Web.Api.Controllers
{
    [Route("api/posts")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private Cloudinary _cloudinary;
        private readonly CloudinarySettings _cloudinarySettings;

        public PostsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, 
            UserManager<User> userManager, CloudinarySettings cloudinarySettings)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _cloudinarySettings = cloudinarySettings;

            Account acc = new Account(
                _cloudinarySettings.CloudName,
                _cloudinarySettings.ApiKey,
                _cloudinarySettings.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpPost("user")]
        public async Task<IActionResult> GetPostsForUser([FromBody] PostParameters postParameters) {
            var posts = await _repository.Post.GetPostsForUserAsync(postParameters, false);

            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(posts.MetaData));

            // TODO: fix this problem with CurrentUserLiked inside Likes, so you can use automapper
            //var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            var postsDto = new List<PostDto>();
            var userFromDb = await _userManager.GetUserAsync(this.User);
            foreach (var post in posts) {
                var postDto = new PostDto() {
                    Id = post.Id,
                    Body = post.Body,
                    CreatedById = post.CreatedById,
                    Title = post.Title,
                    Likes = new LikeDto() {
                        LikesCount = post.Likes.Count,
                        CurrentUserLiked = post.Likes.Any(l => l.LikerId == userFromDb.Id)
                    },
                    Photos = _mapper.Map<IEnumerable<PhotoDto>>(post.Photos)
                };

                IEnumerable<CommentDto> commentsDto = _mapper.Map<IEnumerable<CommentDto>>(post.Comments);
                postDto.Comments = commentsDto;

                postsDto.Add(postDto);
            }

            return Ok(postsDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePost([FromBody] PostForCreationDto post)
        {
            var postEntity = _mapper.Map<Post>(post);
            var userFromDb = await _userManager.GetUserAsync(this.User);

            postEntity.CreatedById = userFromDb.Id;
            _repository.Post.CreatePost(postEntity);
            await _repository.SaveAsync();

            var postToReturn = _mapper.Map<PostDto>(postEntity);

            return Ok(postToReturn);
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId) {
            var postToDelete = await _repository.Post.GetPostAsync(postId, false);

            if (postToDelete == null)
                return BadRequest();

            var commentsToDelete = await _repository.Comment.GetCommentsForPostAsync(postId);
            var photosToDelete = await _repository.Photo.GetPhotosForPostAsync(postId);
            var likesToDelete = await _repository.Like.GetLikesForPostAsync(postId);

            // delete cloudinary images
            foreach (var photo in photosToDelete) { 
                var deleteParams = new DeletionParams(photo.PublicId);
                var deleteResult = _cloudinary.Destroy(deleteParams);
            }

            _repository.Comment.DeletePostComments(commentsToDelete);
            _repository.Like.DeletePostLikes(likesToDelete);
            _repository.Photo.DeletePostPhotos(photosToDelete);

            _repository.Post.DeletePost(postToDelete);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdatePost(int postId, [FromBody] PostForUpdateDto post) {
            var userFromDb = await _userManager.GetUserAsync(this.User);
            var postToUpdate = await _repository.Post.GetPostAsync(postId, false);

            if (postToUpdate == null)
                return BadRequest();

            if (userFromDb.Id != postToUpdate.CreatedById)
                return Unauthorized();

            postToUpdate.Body = post.Body;

            _repository.Post.UpdatePost(postToUpdate);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
