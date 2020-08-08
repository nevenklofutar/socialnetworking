using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers {
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public UsersController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetUsers([FromBody]UserParameters userParameters) {
            // TODO: switch this query because of paging
            var usersFromRepo = await _repository.User.GetUsersAsync(userParameters, false);
            return Ok(_mapper.Map<List<UserDto>>(usersFromRepo));
        }

    }
}
