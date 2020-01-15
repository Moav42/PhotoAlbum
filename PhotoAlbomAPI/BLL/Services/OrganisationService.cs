using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrganisationService : IOrganisationService<OrganisationBLL>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public OrganisationService(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterOrganisation(OrganisationBLL model)
        {
            var userIdentity = new User { Email = model.Email, UserName = model.Email };

            var result = await _userManager.CreateAsync(userIdentity, model.Password);            

            if (result.Succeeded)
            {
                model.UserId = userIdentity.Id;
                var itemDAL = _mapper.Map<Organisation>(model);
                _unitOfWork.OrganisationsRepository.CreateOrganisation(itemDAL);

                await _unitOfWork.SaveChangesAsync();
                await _userManager.AddToRoleAsync(userIdentity, "user");

                return result;
            }
            return result;
        }

        public async Task<IEnumerable<OrganisationBLL>> GetAllOrganisationAccountsAsync()
        {
            var itemsDAL = await Task.Run(() => _unitOfWork.OrganisationsRepository.ReadAllOrganisations());
            var iemsBLL = new List<OrganisationBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(_mapper.Map<OrganisationBLL>(item));
            }

            return iemsBLL;
        }

        public async Task<OrganisationBLL> GetOrganisationAccountAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.OrganisationsRepository.ReadOrganisation(id));
            return _mapper.Map<OrganisationBLL>(item);
        }

        public async Task UpdateOrganisationAccountAsync(OrganisationBLL item)
        {
            _unitOfWork.OrganisationsRepository.UpdateOrganisation(_mapper.Map<Organisation>(item));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteOrganisationAccountAsync(int id)
        {
            _unitOfWork.OrganisationsRepository.DeleteOrganisation(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
