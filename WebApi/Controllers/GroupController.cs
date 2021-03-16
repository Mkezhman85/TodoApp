using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        IDataRepository _repository;

        public GroupController(IDataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetGroups")]
        public ActionResult Index()
        {
            return Ok(_repository.GetGroups());
        }

        [HttpPost]
        [Route("CreateGroup")]
        public ActionResult CreateGroup([FromBody] Group group)
        {
            return Ok(_repository.CreateGroup(group));
        }


        [HttpPost]
        [Route("CreateNewUser")]
        public ActionResult CreateNewUser([FromBody] User user)
        {
            return Ok(_repository.CreateNewUser(user));
        }



    }
}
