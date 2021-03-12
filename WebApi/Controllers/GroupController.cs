using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        IGroupRepository _repository;
        public GroupController(IGroupRepository repository)
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
    }
}
