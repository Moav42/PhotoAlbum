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
                iemsBLL.Add(_mapper.Map<OrganisationBLL>(item));
            }

            return iemsBLL;
        }

        public async Task<OrganisationBLL> GetAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.OrganisationsRepository.Read(id));
            return _mapper.Map<OrganisationBLL>(item);
        }

        public async Task UpdateAsync(OrganisationBLL item)
        {
            _unitOfWork.OrganisationsRepository.Update(item.Id, _mapper.Map<Organisation>(item));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.OrganisationsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
