using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class TagService
    {
        private UnitOfWork _unitOfWork;
        public TagService(UnitOfWork db)
        {
            _unitOfWork = db;
        }
    }
}
