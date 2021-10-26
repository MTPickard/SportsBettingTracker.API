using _01_SportsBetting.Data;
using _02_SportsBetting.Models;
using SportsBettingTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_SportsBetting.Services
{
    public class UserService
    {
        private readonly int _userId;

        public UserService (int userId)
        {
            _userId = userId;
        }

        public bool CreateUser(UserModelCreate model)
        {
            var entity =
                new User()
                {
                    UserId = _userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Users.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<UserModelListItem> GetUsers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Users
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new UserModelListItem
                                {
                                    UserId = e.UserId,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName
                                }
                        );
                
                return query.ToArray();
            }
        }
        
        public UserModelDetail GetUserById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Users
                        .Single(e => e.UserId == id && e.UserId == _userId);
                return
                    new UserModelDetail
                    {
                        UserId = entity.UserId,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName
                    };
            }
        }

        public bool UpdateUser(UserModelEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Users
                        .Single(e => e.UserId == model.UserId && e.UserId == _userId);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteUser(int userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Users
                        .Single(e => e.UserId == userId && e.UserId == _userId);

                ctx.Users.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
