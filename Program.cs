using System.Configuration;
using Flash_Cards.Lawang;
using Flash_Cards.Lawang.Controller;
using Flash_Cards.Lawang.Views;

string connectionString = ConfigurationManager.ConnectionStrings["Sql"].ConnectionString;
var visualize = new Visualize();
var validation = new Validation();

var codingController  = new CodingController(connectionString);
codingController.createTables();

var manager = new ApplicationManager(visualize, validation, codingController);
manager.Start();