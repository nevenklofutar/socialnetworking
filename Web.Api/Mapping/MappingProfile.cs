using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Web.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDto>()
                .ForPath(p => p.Likes.LikesCount, opt => opt.MapFrom(x => x.Likes.Count));
                //.ForMember(p => p.Like.LikesCount, opt => opt.MapFrom(x => x.Likes.Count));

            CreateMap<PostForCreationDto, Post>();
            CreateMap<PostForUpdateDto, Post>().ReverseMap();

            CreateMap<UserForRegistrationDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<CommentForCreationDto, Comment>();
            CreateMap<Comment, CommentDto>()
                .ForMember(c => c.CommentedByName, opt => opt.MapFrom(x => x.CommentedBy.FirstName + " " + x.CommentedBy.LastName));

            CreateMap<Like, LikeDto>();

            CreateMap<PhotoForCreationDto, Photo>();

        }
    }
}
