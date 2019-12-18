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
    public class PostRelevantOperationsService
    {
        private UnitOfWork _unitOfWork;
        public PostRelevantOperationsService(UnitOfWork db)
        {
            _unitOfWork = db;
        }
    }
}
