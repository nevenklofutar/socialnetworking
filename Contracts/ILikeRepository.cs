﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ILikeRepository
    {
        public Task<IEnumerable<Like>> GetLikesForPostAsync(int postId);
        public Task ProcessLikeAsync(int postId, string userId);
    }
}