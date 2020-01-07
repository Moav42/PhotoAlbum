﻿using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IPostRateRepository<T>
    {
        void Create(T item);
        void Update(T item);
        bool GetPostsRate(int postId, string userId);
    }
}
