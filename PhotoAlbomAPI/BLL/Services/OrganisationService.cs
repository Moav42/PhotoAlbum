using BLL.ExtensionsForTransfer;
using BLL.Interfaces;
using BLL.Models;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrganisationService : IOrganisationService<OrganisationBLL>
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public OrganisationService(UserManager<User> userManager)
        {
            _unitOfWork = new UnitOfWork();
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterOrganisation(OrganisationBLL model)
        {
            var userIdentity = new User { Email = model.Email, UserName = model.Email };

            var result = await _userManager.CreateAsync(userIdentity, model.Password);
            

            if (result.Succeeded)
            {
                model.UserId = userIdentity.Id;
                var itemDAL = model.Transform();
                _unitOfWork.OrganisationsRepository.Create(itemDAL);
                await _unitOfWork.SaveChangesAsync();

                await _userManager.AddToRoleAsync(userIdentity, "user");

                return result;
            }
            return result;
        }

        public async Task<IEnumerable<OrganisationBLL>> GetAllAsync()
        {
            var itemsDAL = await Task.Run(() => _unitOfWork.OrganisationsRepository.ReadAll());
            var iemsBLL = new List<OrganisationBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(item.Transform());
            }

            return iemsBLL;
        }

        public async Task<OrganisationBLL> GetAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.OrganisationsRepository.Read(id));
            return item.Transform();
        }

        public async Task UpdateAsync(OrganisationBLL item)
        {
            _unitOfWork.OrganisationsRepository.Update(item.Id, item.Transform());
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.OrganisationsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
