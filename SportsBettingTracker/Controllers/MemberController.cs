using _02_SportsBetting.Models;
using _03_SportsBetting.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SportsBettingTracker.Controllers
{
    public class MemberController : ApiController
    {
        // R GET GetAllMembers
        public IHttpActionResult GetAllMembers()
        {
            MemberService memberService = CreateMemberService();
            var users = memberService.ViewMembers();
            return Ok(users);
        }

        // C POST PostNewMember
        public IHttpActionResult PostNewMember(MemberModelCreate member)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateMemberService();

            if (!service.CreateMember(member))
                return InternalServerError();

            return Ok();
        }

        // R GET GetMemberByMemberId
        public IHttpActionResult GetMemberByMemberId(int id)
        {
            MemberService memberService = CreateMemberService();
            var member = memberService.ViewMemberById(id);
            return Ok();
        }

        // U PUT PutMemberByMemberId
        public IHttpActionResult PutMemberByMemberId(MemberModelEdit member)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateMemberService();

            if (!service.UpdateMemberById(member))
                return InternalServerError();

            return Ok();
        }

        // D DELETE DeleteMemberByMemberId
        public IHttpActionResult DeleteMemberByMemberId(int id)
        {
            var service = CreateMemberService();

            if (!service.DeleteMemberById(id))
                return InternalServerError();

            return Ok();
        }

        // Helper Method
        private MemberService CreateMemberService()
        {
            var memberId = int.Parse(User.Identity.GetUserId());
            var memberService = new MemberService(memberId);
            return memberService;
        }
    }
}
