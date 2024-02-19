using Dapper;
using L.I.S.A.Areas.Identity.Data;
using L.I.S.A.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace L.I.S.A.Controllers
{
    public class DriverController : Controller
    {
        private readonly IDbConnection _connection;
        private readonly UserManager<LISAUser> _userManager;
      

        public DriverController(IDbConnection connection, UserManager<LISAUser> userManager)
        {
            _connection = connection;
            _userManager = userManager;
           

        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult DieselOrder()
        {
            return View();
        }


        public IActionResult CreateCase()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create( CaseVM vm)
        {
            var user = await _userManager.GetUserAsync(User);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", user.Id, DbType.String);
            parameters.Add("@desc", vm.Casing_Desc, DbType.String);
            parameters.Add("@type", vm.Casing_Type, DbType.String);

            var affectedRows = await _connection.ExecuteAsync(
                "RegisterCase",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            if (affectedRows > 0)
            {
                TempData["SuccessMessage"] = "Case successfully create. ";
            }
           

            return RedirectToAction("CreateCase");
         
        }

        
    }
}
