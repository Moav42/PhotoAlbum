using BLL.Models;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using BLL.ExtensionsForTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.Interfaces;

using System.Threading.Tasks;

namespace BLL.Services
{
    public class TagService : ITagService<TagBLL>
    {
        private readonly UnitOfWork _unitOfWork;

        public TagService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task AddAsync(TagBLL item)
        {
            var itemDAL = item.Transform();
            _unitOfWork.TagsRepository.Create(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TagBLL>> GetAllAsync()
        {
            var itemsDAL =  await Task.Run(() =>_unitOfWork.TagsRepository.ReadAll());
            var iemsBLL = new List<TagBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(item.Transform());
            }

            return iemsBLL;
        }

        public async Task<TagBLL> GetAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.TagsRepository.Read(id));
            if (item != null)
            {
                return item.Transform();
            }
            else return null;
           
        }

        public async Task UpdateAsync(TagBLL item)
        {

            _unitOfWork.TagsRepository.Update(item.Id, item.Transform());
            await _unitOfWork.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            _unitOfWork.TagsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
