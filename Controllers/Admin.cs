using Dapper;
using L.I.S.A.Data;
using L.I.S.A.Models;
using Microsoft.AspNetCore.Mvc;
using NPOI.XSSF.UserModel;
using System.Data;
using System.Reflection;


namespace L.I.S.A.Controllers
{
    public class Admin : Controller
    {
        private readonly IDbConnection _connection;

        private ILogger<Admin> _logger;
        
        private readonly LISASITEContext _context;

        public Admin(IDbConnection connection, LISASITEContext context, ILogger<Admin> logger)


        
       
        {
            _connection = connection;

            _context = context;

            _logger = logger;

        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ViewDrivers()
        {
            var drivers = _connection.Query<DriverVM>("ViewDrivers", commandType: CommandType.StoredProcedure);
            return View(drivers);
        }

        [HttpGet]
        public IActionResult RegisterTruck()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(TrucksVM trucks)
        {



            //byte[] byteFileName = UploadFile(vm);
            //var truck = new Truck
            //{
            //    Make = vm.Make,
            //    VinNum = vm.Vin_Num,
            //    Trailer1Reg = vm.Trailer1_Reg,
            //    Trailer2Reg = vm.Trailer2_Reg,
            //    Company = vm.Company,
            //    TruckStatus = vm.Truck_Status,
            //    TruckCondition = vm.Truck_Condition,
            //    TruckImg = byteFileName

            //};
            //_context.Trucks.Add(truck);
            //_context.SaveChanges();

            if (ModelState.IsValid)
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Make", trucks.Make, DbType.String);
                parameters.Add("@vin", trucks.Vin_Num, DbType.String);
                parameters.Add("@trailer1", trucks.Trailer1_Reg, DbType.String);
                parameters.Add("@trailer2", trucks.Trailer2_Reg, DbType.String);
                parameters.Add("@company", trucks.Company, DbType.String);
                parameters.Add("@condition", trucks.Truck_Condition, DbType.String);
                parameters.Add("@status", trucks.Truck_Status, DbType.String);

                if (trucks.TruckImageFile != null && trucks.TruckImageFile.Length > 0)
                {
                    // Read the file into a byte array
                    using (var memoryStream = new MemoryStream())
                    {
                        trucks.TruckImageFile.CopyTo(memoryStream);
                        parameters.Add("@img", memoryStream.ToArray(), DbType.Binary);
                    }
                }

                var affectedRows = _connection.Execute("RegisterTruck", parameters, commandType: CommandType.StoredProcedure);

                if (affectedRows > 0)
                {
                    TempData["SuccessMessage"] = "Truck registered successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Truck already registered in the system.";
                }





            }
            return View("RegisterTruck");
        }
         

        public IActionResult ViewTrucks()
        {
            //var truck = _context.Trucks.ToList();
            var truck = _connection.Query<TrucksVM>("ViewTrucks", commandType: CommandType.StoredProcedure);
            return View(truck);
            
        }

        public IActionResult PendingCases()
        {
            var truck = _connection.Query<CaseVM>("ViewPendingCases", commandType: CommandType.StoredProcedure);
            return View(truck);
            
        }

        public IActionResult ClosedCases()
        {
            var truck = _connection.Query<CaseVM>("ViewClosedCases", commandType: CommandType.StoredProcedure);

            return View(truck);
        }

        public IActionResult CloseCases()
        {
            var truck = _connection.Query<CaseVM>("ViewClosedCases", commandType: CommandType.StoredProcedure);
            var dt = ToDataTable(truck.ToList()); // Convert list to datatable
            var fileName = "ClosedCases.xlsx";
            var bytes = ExportListUsingNPOI(dt, fileName);
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

       


        public byte[] ExportListUsingNPOI(DataTable dt, string fileName)
        {
            using (var workbook = new XSSFWorkbook())
            {
                var sheet = workbook.CreateSheet("Sheet1");
                int rowCount = 1;
                foreach (DataRow row in dt.Rows)
                {
                    var excelRow = sheet.CreateRow(rowCount++);
                    int columnIndex = 0;
                    foreach (var cell in row.ItemArray)
                    {
                        var excelCell = excelRow.CreateCell(columnIndex++);
                        excelCell.SetCellValue(cell.ToString());
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.Write(stream);
                    return stream.ToArray();
                }
            }
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public IActionResult AllocateTruck()
        {
            return View();
        }

        public IActionResult AddSites()
        {
            return View();
        }


        [HttpPost]
        public IActionResult OnSite(LoadingSiteVM vm)
        {
           
            

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", vm.Load_Site_Name, DbType.String);
                

                var affectedRows = _connection.Execute("AddOffLoadingSites", parameters, commandType: CommandType.StoredProcedure);

                if (affectedRows > 0)
                {
                    TempData["SuccessMessage"] = "Site added successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Site already exists in the system.";
                }

                return View("AddSites");

            
        }

        public IActionResult AddOffSites()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OffSite(OffloadingSiteVM vm)
        {



            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", vm.Offload_Site_Name, DbType.String);


            var affectedRows = _connection.Execute("AddOffLoadingSites", parameters, commandType: CommandType.StoredProcedure);

            if (affectedRows > 0)
            {
                TempData["SuccessMessage"] = "Site added successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Site already exists in the system.";
            }

            return View("AddOffSites");


        }

        public IActionResult ViewOffLoadingSites()
        {
            var site = _connection.Query<OffloadingSiteVM>("ViewOffLoadingSites", commandType: CommandType.StoredProcedure);
            return View(site);
        }

        public IActionResult ViewLoadingSites()
        {
            var site = _connection.Query<LoadingSiteVM>("ViewLoadingSites", commandType: CommandType.StoredProcedure);
            return View(site);
        }
    }
}
