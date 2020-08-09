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
    //[Authorize]
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

        [HttpPost("user")]
        public async Task<IActionResult> GetPostsForUser([FromBody] PostParameters postParameters) {
            var posts = await _repository.Post.GetPostsForUserAsync(postParameters, false);

            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(posts.MetaData));

            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

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
    }
}
