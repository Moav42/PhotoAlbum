﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface ITagRepository<T>
    {
        IEnumerable<T> ReadAll();
        T Read(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}