using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Infrastructure.Repository.Contracts;
using VideoClub.Infrastructure.Repository.Entity;
using VideoClub.Infrastructure.Repository.Extensions;
using VideoClub.Infrastructure.Repository.UnitOfWork;
using VideoClub.Infrastructure.Repository.Utils;

namespace VideoClub.Infrastructure.Repository.Implementations
{
    public abstract class CommonRepository<T> where T : class, IEntity
    {
        private readonly VideoClubContext _videoClubContext;

        protected CommonRepository(VideoClubDi videoClubDi)
        {
            _videoClubContext = videoClubDi.VideoClubContext;
        }

        public abstract bool Remove(string id);
        public abstract T Get(string id);

        public virtual bool Update(T model)
        {
            var result = false;
            try
            {
                var modelEntity = _videoClubContext.Set<T>().FirstOrDefault(entity => entity.Id.Equals(model.Id));
                var entry = _videoClubContext.Entry(modelEntity);
                entry.State = EntityState.Modified;
                entry.CurrentValues.SetValues(model);
                SaveChanges();
                Helper.Log.Info($"Has been modified {model.GetType().Name} successfully with ID: {model.Id}");
                result = true;
            }
            catch (Exception e)
            {
                e.CustomDescription();
            }

            return result;
        }

        public virtual bool Add(T model)
        {
            var result = false;
            try
            {
                _videoClubContext.Entry(model).State = EntityState.Added;
                _videoClubContext.SaveChanges();
                Helper.Log.Info($"Has been added {model.GetType().Name} successfully with ID: {model.Id}");
                result = true;
            }
            catch (Exception e)
            {
                e.CustomDescription();
            }

            return result;
        }

        public virtual List<T> All()
        {
            return _videoClubContext.Set<T>().ToList();
        }

        public virtual void SaveChanges()
        {
            _videoClubContext.SaveChanges();
        }
    }
}
