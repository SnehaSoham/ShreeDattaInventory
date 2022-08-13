using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShreeDattaInventory.Models;
using System.Data.OleDb;
using Microsoft.Extensions.Configuration;
using System.Data.Odbc;

namespace ShreeDattaInventory.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration iconfig)
        {
            _logger = logger;
            _configuration = iconfig;
        }

        public IActionResult Index()
        {
            return View("SaveReelData");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult SaveReelData(ReelMaster reel)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _configuration.GetValue<string>("DbInfo:FileLocation") + ";Persist Security Info=False;";
            OleDbConnection dbConnect = new OleDbConnection(connectionString);
            dbConnect.Open();
            string command = "insert into ReelMaster(GSM,MillName,MillSerialNumber,ReelNumber,Weight,EntryDate,UpdatedDate) values (" + reel.GSM + ",'" + reel.MillName + "',"
                + reel.MillSerialNumber + "," + reel.ReelNumber + "," + reel.Weight + "," + DateTime.Now + "," + DateTime.Now + ")";
            OleDbCommand Insertcommand = new OleDbCommand(command, dbConnect);
            Insertcommand.ExecuteNonQuery();
            dbConnect.Close();
            return View("ShowReelData");
        }
        [Route("/ShreeDattaInventory")]
        public ActionResult ShowReelData()
        {
            List<ReelMaster> Reels = new List<ReelMaster>();
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _configuration.GetValue<string>("DbInfo:FileLocation") + ";Persist Security Info=False;";
            OleDbConnection dbConnect = new OleDbConnection(connectionString);
            dbConnect.Open();
            string command = "SELECT GSM,MillSerialNumber,MillName,ReelNumber,Weight,Entrydate from ReelMaster;";
            OleDbCommand Retrievecommand = new OleDbCommand(command, dbConnect);
            try
            {
                using (OleDbDataReader reader = Retrievecommand.ExecuteReader())
                {
                    while (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ReelMaster Reel = new ReelMaster();
                            Reel.GSM = Convert.ToInt64(reader["GSM"].ToString());
                            Reel.MillName = reader["MillName"].ToString();
                            Reel.MillSerialNumber = Convert.ToInt32(reader["MillSerialNumber"].ToString());
                            Reel.ReelNumber = Convert.ToInt32(reader["ReelNumber"].ToString());
                            Reel.Weight = Convert.ToInt64(reader["Weight"].ToString());
                            Reel.EntryDate = Convert.ToDateTime(reader["EntryDate"].ToString());
                            Reels.Add(Reel);
                        }
                        reader.NextResult();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            dbConnect.Close();
            return View(Reels);
        }
        public ActionResult ViewReel(int id,int gsm)
        {
            ReelCalculation reel = new ReelCalculation();
            reel.GSM = gsm;
            reel.ReelNumber = id;
            reel.Height = 0;
            reel.Width = 0;
            return View(reel);
        }
    }

}
