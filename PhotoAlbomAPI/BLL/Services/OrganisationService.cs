using BLL.ExtensionsForTransfer;
using BLL.Interfaces;
using BLL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrganisationService : IOrganisationService<OrganisationBLL>
    {
        private readonly UnitOfWork _unitOfWork;
        public OrganisationService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public async Task AddAsync(OrganisationBLL item)
        {
            var itemDAL = item.Transform();
            _unitOfWork.OrganisationsRepository.Create(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
