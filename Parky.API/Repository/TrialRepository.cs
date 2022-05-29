using Microsoft.EntityFrameworkCore;
using Parky.API.Data;
using Parky.API.Models;
using Parky.API.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Parky.API.Repository
{
    public class TrialRepository : ITrialRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TrialRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Create(Trial entity)
        {
            _dbContext.Add(entity);
            return Save();
        }

        public bool Delete(Trial entity)
        {
            _dbContext.Remove(entity);
            return Save();
        }

        public ICollection<Trial> Get()
        {
            return _dbContext.Trials.Include(e=>e.NationalPark).OrderBy(x => x.Name).ToList();
        }

        public Trial GetById(int id)
        {
            return _dbContext.Trials.Include(e => e.NationalPark).FirstOrDefault(e => e.Id == id);
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

        public bool Update(Trial entity)
        {
            _dbContext.Trials.Update(entity);

            return Save();
        }
    }
}
