using Parky.API.Models;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Parky.API.Repository.IRepository
{
    public interface INationalParkRepository
    {
        ICollection<NationalPark> Get();
        NationalPark GetById(int id);
        bool IsExistByName(string name);
        bool IsExistById(int id);
        bool Create(NationalPark entity);
        bool Update(NationalPark entity);
        bool Delete(NationalPark entity);
        bool Save();
    }
}
