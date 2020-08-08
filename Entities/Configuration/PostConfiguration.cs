using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder) 
        {
            //builder.HasData
            //(
            //    new Post
            //    {
            //        Id = 1,
            //        Title = "Post Title 1",
            //        Body = "Post body 1",
            //        CreatedOn = DateTime.Now
            //    },
            //    new Post
            //    {
            //        Id = 2,
            //        Title = "Post Title 2",
            //        Body = "Post body 2",
            //        CreatedOn = DateTime.Now
            //    },
            //    new Post
            //    {
            //        Id = 3,
            //        Title = "Post Title 3",
            //        Body = "Post body 3",
            //        CreatedOn = DateTime.Now
            //    }
            //);

            


        }
    }
}
