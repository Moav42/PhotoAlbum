using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using BLL.ExtensionsForTransfer;

namespace BLL.Services
{
    public class CategoryService
    {
        private UnitOfWork _unitOfWork;
        public CategoryService()
        {
            _unitOfWork = new UnitOfWork();
        }
    }
}
