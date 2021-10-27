﻿using _02_SportsBetting.Models;
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
    public class UserController : ApiController
    {
        // C POST PostUser
        public IHttpActionResult GetAllUsers()
        {
            UserService userService = CreateUserService();
            var users = userService.GetUsers();
            return Ok(users);
        }

        // R GET GetUsers
        public IHttpActionResult Post(UserModelCreate user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateUserService();

            if (!service.CreateUser(user))
                return InternalServerError();

            return Ok();
        }

        // R GET GetUserByUserId
        public IHttpActionResult Get(int id)
        {
            UserService userService = CreateUserService();
            var user = userService.GetUserById(id);
            return Ok();
        }

        // U PUT PutUserByUserId
        public IHttpActionResult Put(UserModelEdit user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateUserService();

            if (!service.UpdateUser(user))
                return InternalServerError();

            return Ok();
        }

        // D DELETE DeleteUserByUserId
        public IHttpActionResult Delete(int id)
        {
            var service = CreateUserService();

            if (!service.DeleteUser(id))
                return InternalServerError();

            return Ok();
        }

        // Helper Method
        private UserService CreateUserService()
        {
            var userId = int.Parse(User.Identity.GetUserId());
            var userService = new UserService(userId);
            return userService;
        }
    }
}
