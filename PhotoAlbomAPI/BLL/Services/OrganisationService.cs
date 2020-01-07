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
    /// <summary>
    /// A service containing business logic that is responsible for a specific resource. Configurable by the UoF, implemented through the DI
    /// </summary>
    public class OrganisationService : IOrganisationService<OrganisationBLL>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        /// <summary>
        /// Configures the service with the parameters provided by the dependency injection system
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public OrganisationService(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new orgonisations account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets all orgonistions accounts
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets orgonisation account by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OrganisationBLL> GetOrganisationAccountAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.OrganisationsRepository.ReadOrganisation(id));
            return _mapper.Map<OrganisationBLL>(item);
        }

        /// <summary>
        /// Updates orgonisation account 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task UpdateOrganisationAccountAsync(OrganisationBLL item)
        {
            _unitOfWork.OrganisationsRepository.UpdateOrganisation(_mapper.Map<Organisation>(item));
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Delets orgonisation account 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteOrganisationAccountAsync(int id)
        {
            _unitOfWork.OrganisationsRepository.DeleteOrganisation(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
