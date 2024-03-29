﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using Microsoft.AspNetCore.Cors;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {


        IDataRepository _repository;

        public UserController(IDataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetUsers")]
        public ActionResult Index()
        {
            return Ok(_repository.GetUsers());
        }

        [HttpPost]
        [Route("CreateNewUser")]
        public ActionResult CreateNewUser([FromBody] User user)
        {
            return Ok(_repository.CreateNewUser(user));
        }

        [HttpGet]
        [Route("GetUsersMulti")]
        public ActionResult GetUsersMulti()
        {
            return Ok(_repository.GetUsersMulti());
        }





        //readonly TodoAppContext _context;

        //public UserController(TodoAppContext context)
        //{
        //    _context = context;
        //}


        // [EnableCors("ExposeResponseHeaders")]
        //[HttpGet]
        //[Route("GetUsers")]
        //public IActionResult GetUsers()
        //{
        //    var users = _context.user.ToList();
        //    return Ok(users);
        //}

        //[HttpPost]
        //[Route("CreateUser")]
        //public JsonResult CreateUser([FromBody] User user)
        //{

        //    try
        //    {
        //         _context.Add(user);
        //    _context.SaveChanges();
        //    }
        //    catch (System.Exception e)
        //    {

        //        throw;
        //    }


        //    return new JsonResult(user);
        //}

        //[HttpDelete("{id}")]
        //[Route("DeleteUserById")]
        //public async Task<IActionResult> DeleteUserById(int id)
        //{
        //    var userItem = await _context.user.FindAsync(id);
        //    if (userItem == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.user.Remove(userItem);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}


        //[HttpGet("{id}")]
        //[Route("GetUserById")]
        //public async Task<ActionResult<User>> GetUserById(int id)
        //{
        //    var userItem = await _context.user.FindAsync(id);

        //    if (userItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return userItem;
        //}



    }
}
