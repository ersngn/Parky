using Parky.API.Models;
using System.Collections.Generic;

namespace Parky.API.Repository.IRepository
{
    public interface ITrialRepository
    {
        ICollection<Trial> Get();
        Trial GetById(int id);
        bool IsExistByName(string name);
        bool IsExistById(int id);
        bool Create(Trial entity);
        bool Update(Trial entity);
        bool Delete(Trial entity);
        bool Save();
    }
}
