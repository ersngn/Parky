using Parky.API.Data;
using Parky.API.Models;
using Parky.API.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Parky.API.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public NationalParkRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public bool Create(NationalPark entity)
        {
            _dbContext.Add(entity);
            return Save();
        }

        public bool Delete(NationalPark entity)
        {
            _dbContext.Remove(entity);
            return Save();
        }

        public ICollection<NationalPark> Get()
        {
            return _dbContext.NationalParks.OrderBy(x => x.Name).ToList();
        }

        public NationalPark GetById(int id)
        {
            return _dbContext.NationalParks.FirstOrDefault(e => e.Id == id);
        }

        public bool IsExistById(int id)
        {
            var value = _dbContext.NationalParks.Any(e => e.Id == id);
            return value;
        }

        public bool IsExistByName(string name)
        {
            var value = _dbContext.NationalParks.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool Save()
        {
            return -_dbContext.SaveChanges() >= 0 ? true : false;
        }

        public bool Update(NationalPark entity)
        {
            _dbContext.NationalParks.Update(entity);

            return Save();
        }
    }
}
