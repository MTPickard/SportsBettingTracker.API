using _01_SportsBetting.Data;
using _02_SportsBetting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_SportsBetting.Services
{
    public class MemberService
    {
        //Note for repush
        private readonly Guid _userId;

        public MemberService (Guid userId)
        {
            _userId = userId;
        }

        //Throwing note in here
        // C POST CreateMember
        public bool CreateMember(MemberModelCreate model)
        {
            var entity =
                new Member()
                {
                    OwnerId = _userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Members.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // R GET ViewMembers
        public IEnumerable<MemberModelListItem> ViewMembers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Members
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new MemberModelListItem
                                {
                                    MemberId = e.MemberId,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName
                                }
                        );
                
                return query.ToArray();
            }
        }

        // R GET ViewMemberByUserId
        public MemberModelDetail ViewMemberById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Members
                        .Single(e => e.MemberId == id && e.OwnerId == _userId);
                return
                    new MemberModelDetail
                    {
                        MemberId = entity.MemberId,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName
                    };
            }
        }

        // U PUT UpdateMemberByMemberId
        public bool UpdateMemberById(MemberModelEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Members
                        .Single(e => e.MemberId == model.MemberId && e.OwnerId == _userId);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;

                return ctx.SaveChanges() == 1;
            }
        }

        // D DELETE RemoveMemberByMemberId
        public bool DeleteMemberById(int memberId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Members
                        .Single(e => e.MemberId == memberId && e.OwnerId == _userId);

                ctx.Members.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
