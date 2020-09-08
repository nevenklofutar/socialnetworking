using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Contracts;
using Entities.Configuration;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Web.Api.Controllers {
    //public class InputTest {
    //    public string PhotoName { get; set; }
    //    public string PhotoBase64String { get; set; }
    //}

    //public class InputClass {
    //    public List<InputTest> Photos { get; set; }
    //}

    [Route("api/photos")]
    [ApiController]
    [Authorize]
    public class PhotosController : ControllerBase {

        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private Cloudinary _cloudinary;
        private readonly CloudinarySettings _cloudinarySettings;

        public PhotosController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager, CloudinarySettings cloudinarySettings) {

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

        [HttpPost("filecollection")]
        public IActionResult AddPhotosForUserTest([FromForm] IFormFileCollection files) {
            files = Request.Form.Files;

            if (files == null || files.Count == 0)
                return BadRequest("No images sent.");

            return Ok();
        }

        [HttpPost("base64strings")]
        public async Task<IActionResult> AddPhotosForUser([FromBody] PhotosForCreationDto input) {

            if (input == null || input.Photos == null || input.Photos.Count == 0)
                return BadRequest("No images sent.");

            var userFromDb = await _userManager.GetUserAsync(this.User);

            foreach (var photoForCreationDto in input.Photos) {
                string imagestring = photoForCreationDto.PhotoBase64String.Substring(
                    photoForCreationDto.PhotoBase64String.IndexOf(',') + 1);
                byte[] bytes = Convert.FromBase64String(imagestring);
                //System.IO.File.WriteAllBytes(string.Format($"c:\\temp\\{image.PhotoName}"), bytes);

                var coudinaryUploadResults = new ImageUploadResult();

                if (bytes.Length > 0) {
                    using (var stream = new MemoryStream(bytes)) {
                        var uploadParams = new ImageUploadParams() {
                            File = new FileDescription(photoForCreationDto.PhotoName, stream),
                            Folder = "KlofBook/user_" + userFromDb.Id,
                            Transformation = new Transformation().Width(600).Height(600)
                            // Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                        };

                        coudinaryUploadResults = _cloudinary.Upload(uploadParams);
                    }
                }

                photoForCreationDto.Url = coudinaryUploadResults.Url.ToString();
                photoForCreationDto.PublicId = coudinaryUploadResults.PublicId;

                var photo = _mapper.Map<Photo>(photoForCreationDto);
                photo.IsMain = false;
                photo.IsApproved = true;
                photo.UserId = userFromDb.Id;

                _repository.Photo.CreatePhoto(photo);
                await _repository.SaveAsync();
            }

            return Ok();
        }



        //    var photo = _mapper.Map<Photo>(photoForCreationDto);

        //    //if (!userFromRepo.Photos.Any(u => u.IsMain))
        //    //    photo.IsMain = true;

        //    userFromRepo.Photos.Add(photo);
        //    if (postFromRepo != null)
        //        postFromRepo.Photos.Add(photo);

        //    await _repository.SaveAsync();

        //    return Ok();
        //}
    }

}
