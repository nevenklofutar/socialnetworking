using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Contracts;
using EmailService;
using Entities.Configuration;
using Entities.DTOs;
using Entities.Models;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Web.Api.ActionFilters;
using Web.Api.Extensions;

namespace Web.Api.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IEmailSender _emailSender;
        private readonly FrontendConfiguration _frontendConfiguration;

        public AuthenticationController(ILoggerManager logger, IMapper mapper, UserManager<User> userManager, 
            IAuthenticationManager authManager, IEmailSender emailSender, FrontendConfiguration frontendConfiguration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
            _emailSender = emailSender;
            _frontendConfiguration = frontendConfiguration;
        }

        [HttpPost("test")]
        public async Task<IActionResult> Test() {
            return Ok(Environment.GetEnvironmentVariable("TEST_VALUE"));
        }

        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                throw ProblemDetailsErrorHelper.ProblemDetailsError(ModelState);
            }

            //https://stackoverflow.com/questions/6855624/plus-sign-in-query-string
            // we need to url encode token because token is not url safe
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            RedirectResult redirectResult = new RedirectResult($"{_frontendConfiguration.BaseUrl}{_frontendConfiguration.AuthenticationControllerName}" +
                $"{_frontendConfiguration.RegisterConfirm}?email={user.Email}&token={HttpUtility.UrlEncode(token)}");
            var message = new Message(user.Email, "Register confirm", redirectResult.Url, redirectResult.Url);
            await _emailSender.SendEmail(message);
            return StatusCode(201);
        }

        [HttpPost("confirmregisteremail")]
        public async Task<IActionResult> ConfirmRegisterEmail([FromBody]RegisterConfirmEmail parameters)
        {
            var user = await _userManager.FindByEmailAsync(parameters.Email);
            if (user == null) { 
                _logger.LogError($"AuthenticationController.ConfirmRegisterEmail email:{parameters.Email}; error:unknown user");
                throw new ProblemDetailsException(400, "Error occured");
            }

            var result = await _userManager.ConfirmEmailAsync(user, parameters.Token);
            if (result.Succeeded) {
                // https://stackoverflow.com/questions/22755700/revoke-token-generated-by-usertokenprovider-in-asp-net-identity-2-0
                // we need to reset user security stamp after he confirms email, so he will get new security token
                // so he can't confirm register over and over again with the same token
                await _userManager.UpdateSecurityStampAsync(user);
                return Ok();
            }

            _logger.LogError($"AuthenticationController.ConfirmRegisterEmail email:{parameters.Email}; error:{result.Errors}");

            throw new ProblemDetailsException(400, "Error occured");
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                _logger.LogWarning($"{nameof(Login)}: Login failed. Wrong user name or password.");
                return Unauthorized();
                //throw new ProblemDetailsException(401, "Unauthorized");
            }

            var userFromRepo = await _userManager.FindByNameAsync(user.UserName);
            var userToReturn = _mapper.Map<UserDto>(userFromRepo);
            var token = await _authManager.CreateToken();
            return Ok(new { Token = token, User = userToReturn });
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] UserForForgotPasswordDto userForForgotPasswordDto)
        {
            if (userForForgotPasswordDto == null)
                return Ok();

            var user = await _userManager.FindByEmailAsync(userForForgotPasswordDto.Email);

            if (user == null)
                return Ok();

            //https://stackoverflow.com/questions/6855624/plus-sign-in-query-string
            // we need to url encode token because token is not url safe
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            RedirectResult redirectResult = new RedirectResult($"{_frontendConfiguration.BaseUrl}{_frontendConfiguration.AuthenticationControllerName}" +
                $"{_frontendConfiguration.ForgotPasswordActionName}?email={user.Email}&token={HttpUtility.UrlEncode(token)}");

            var message = new Message(user.Email, "Reset password token", redirectResult.Url, redirectResult.Url);
            await _emailSender.SendEmail(message);

            return Ok();
        }

        [HttpPost("forgotpasswordconfirm")]
        public async Task<IActionResult> ForgotPasswordConfirm([FromBody] UserForForgotPasswordConfirmDto userForForgotPasswordConfirmDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(userForForgotPasswordConfirmDto.Email);

            if (user == null)
                return BadRequest();

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, userForForgotPasswordConfirmDto.Token, userForForgotPasswordConfirmDto.Password);

            if (!resetPasswordResult.Succeeded)
            {
                foreach (var error in resetPasswordResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
