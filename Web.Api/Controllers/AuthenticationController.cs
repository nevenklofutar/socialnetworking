using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmRegisterEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
            var message = new Message(user.Email, "Confirmation email link", confirmationLink, confirmationLink);
            await _emailSender.SendEmail(message);

            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            
            return StatusCode(201);
        }

        [HttpGet("confirmregisteremail")]
        public async Task<IActionResult> ConfirmRegisterEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest();

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return Ok();
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                _logger.LogWarning($"{nameof(Login)}: Login failed. Wrong user name or password.");
                return Unauthorized();
            }

            return Ok(new { Token = await _authManager.CreateToken() });
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] UserForForgotPasswordDto userForForgotPasswordDto)
        {
            if (userForForgotPasswordDto == null)
                return Ok();

            var user = await _userManager.FindByEmailAsync(userForForgotPasswordDto.Email);

            if (user == null)
                return Ok();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            RedirectResult redirectResult = new RedirectResult($"{_frontendConfiguration.BaseUrl}{_frontendConfiguration.AuthenticationControllerName}" +
                $"{_frontendConfiguration.ForgotPasswordActionName}?email={user.Email}&token={token}");

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
