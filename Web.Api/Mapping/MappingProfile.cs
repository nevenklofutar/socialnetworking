using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostForCreationDto, Post>();
            CreateMap<PostForUpdateDto, Post>().ReverseMap();

            CreateMap<UserForRegistrationDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<CommentForCreationDto, Comment>();
            CreateMap<Comment, CommentDto>()
                .ForMember(c => c.CommentedByName, opt => opt.MapFrom(x => x.CommentedBy.FirstName + " " + x.CommentedBy.LastName));

            CreateMap<Like, LikeDto>();
        }
    }
}
