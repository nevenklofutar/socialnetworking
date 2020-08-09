using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class RepositoryPostExtensions
    {
        public static IQueryable<Post> FilterPosts(this IQueryable<Post> posts, string title, string body)
        {
            //if (title != null)
            //    posts = posts.Where(p => p.Title.Contains(title));
            //if (body != null)
            //    posts = posts.Where(p => p.Body.Contains(body));

            return posts;
        }

        public static IQueryable<Post> SearchUserPosts(this IQueryable<Post> posts, string createdByUserId) {
            if (string.IsNullOrWhiteSpace(createdByUserId))
                return posts;

            return posts.Where(p => p.CreatedById == createdByUserId);
        }

        public static IQueryable<Post> SearchPosts(this IQueryable<Post> posts, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return posts;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return posts.Where(p => p.Title.Contains(searchTerm) || p.Body.Contains(searchTerm));
        }

        public static IQueryable<Post> SortPosts(this IQueryable<Post> posts, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return posts.OrderByDescending(p => p.CreatedOn);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Post>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return posts.OrderByDescending(p => p.CreatedOn);

            return posts.OrderBy(orderQuery);
        }
    }
}
