using Microsoft.AspNetCore.Mvc;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.Models;
using QutebaApp_Data.ViewModels;
using System;
using System.Collections.Generic;

namespace QutebaApp_API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public RoleController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAllRoles()
        {
            try
            {
                var roles = unitOfWork.RoleRepository.GetAll();

                if (roles == null)
                {
                    return new JsonResult("There are currently no roles!");
                }

                List<RoleVM> retrivedRoles = new List<RoleVM>();

                foreach (var role in roles)
                {
                    RoleVM retrivedRole = new RoleVM()
                    {
                        ID = role.Id,
                        RoleName = role.RoleName,
                        CreatedAt = role.RoleCreationTime
                    };

                    retrivedRoles.Add(retrivedRole);
                }

                return new JsonResult(retrivedRoles);
            }
            catch (Exception e) { throw e; }
        }

        [HttpGet]
        [Route("getbyid")]
        public IActionResult GetRoleById(int id)
        {
            try
            {
                var role = unitOfWork.RoleRepository.GetById(id);

                if (role == null)
                {
                    return new JsonResult("The role dosen't exist!");
                }
                RoleVM retrivedRole = new RoleVM()
                {
                    ID = role.Id,
                    RoleName = role.RoleName,
                    CreatedAt = role.RoleCreationTime
                };

                return new JsonResult(retrivedRole);
            }
            catch (Exception e) { throw e; }
        }

        [HttpPost]
        [Route("addrole")]
        public IActionResult AddRole(IEnumerable<string> roleNames)
        {
            try
            {
                int count = 0;
                foreach (var roleName in roleNames)
                {
                    Role role = new Role()
                    {
                        RoleName = roleName,
                        RoleCreationTime = DateTime.Now
                    };

                    unitOfWork.RoleRepository.Insert(role);
                    unitOfWork.Save();

                    count++;
                }

                if (count > 1)
                {
                    return new JsonResult($"The specified roles have been added! Created at {DateTime.Now}");
                }

                return new JsonResult($"Role {roleNames} has been added! Created at {DateTime.Now}");
            }
            catch (Exception e) { throw e; }
        }

        [HttpDelete]
        [Route("deleterole")]
        public IActionResult DeleteRole(int id)
        {
            try
            {
                var role = unitOfWork.RoleRepository.GetById(id);

                if (role != null)
                {
                    unitOfWork.RoleRepository.Delete(role.Id);
                    unitOfWork.Save();
                }

                return new JsonResult($"Role [{role.RoleName}] has been deleted! Deteted at [{DateTime.Now}]");
            }
            catch (Exception e) { throw e; }
        }

    }
}
