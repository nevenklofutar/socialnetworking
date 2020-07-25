using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
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

        public PostsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        //[HttpGet, Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery]PostParameters postParameters)
        {
            var posts = await _repository.Post.GetPostsAsync(postParameters, false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(posts.MetaData));

            var postsDto = _mapper.Map <IEnumerable<PostDto>>(posts);

            return Ok(postsDto);
        }

        [HttpPost("/user")]
        public async Task<IActionResult> GetPostsForUser([FromBody] string userId, [FromQuery] PostParameters postParameters) {

            var userFromDb = await _userManager.GetUserAsync(this.User);
            if (userFromDb.Id != userId)
                return Unauthorized();

            postParameters.SearchTerm = "userId=" + userId;
            var posts = await _repository.Post.GetPostsAsync(postParameters, false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(posts.MetaData));

            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            return Ok(postsDto);
        }

        [HttpGet("{id}", Name = "PostById")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _repository.Post.GetPostAsync(id, false);

            if (post == null)
                return NotFound();

            var postDto = _mapper.Map<PostDto>(post);

            return Ok(postDto);
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

            return CreatedAtRoute("PostById", new { id = postToReturn.Id }, postToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidatePostExistsAttribute))]
        public async Task<IActionResult> DeletePost(int id)
        {
            var postToDelete = HttpContext.Items["post"] as Post;
            var userFromDb = await _userManager.GetUserAsync(this.User);

            if (userFromDb.Id != postToDelete.CreatedById)
                return Unauthorized();

            _repository.Post.DeletePost(postToDelete);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidatePostExistsAttribute))]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] PostForUpdateDto postForUpdate)
        {
            var postEntity = HttpContext.Items["post"] as Post;
            var userFromDb = await _userManager.GetUserAsync(this.User);

            if (userFromDb.Id != postEntity.CreatedById)
                return Unauthorized();

            _mapper.Map(postForUpdate, postEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        //[HttpPatch("{id}")]
        //public async Task<IActionResult> PartialPostUpdate(int id, [FromBody] JsonPatchDocument<PostForUpdateDto> patchDocument)
        //{
        //    if (patchDocument == null)
        //        return BadRequest("Patch document is null");

        //    var postEntity = await _repository.Post.GetPostAsync(id, true);

        //    if (postEntity == null)
        //        return NotFound();

        //    var postToPatch = _mapper.Map<PostForUpdateDto>(postEntity);

        //    patchDocument.ApplyTo(postToPatch, ModelState);
        //    TryValidateModel(postToPatch);
        //    if (!ModelState.IsValid)
        //        return UnprocessableEntity(ModelState);

        //    _mapper.Map(postToPatch, postEntity);

        //    await _repository.SaveAsync();

        //    return NoContent();
        //}
    }
}
