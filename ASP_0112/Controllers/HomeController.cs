using ASP_0112.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace ASP_0112.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		List<Result> Results = new List<Result>();
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
			string json = "";
			using (StreamReader sr = new StreamReader("result.json"))
			{
				json = sr.ReadToEnd();
			}
			try
			{
				Results = JsonSerializer.Deserialize<List<Result>>(json)!;
			}
			catch (Exception)
			{

			}
		}

		public IActionResult Index()
		{
			if (Results.Count > 0)
			{
				return View(Results);
			}
			return View();
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

		[HttpPost]
		public IActionResult Index(string number, string operation, string whole, string numer, string denom)
		{
			Fraction fr1, fr2;
			if (decimal.TryParse(number, out decimal num))
				fr1 = new Fraction(num);
			else
				return View();
			if (int.TryParse(numer, out int intNumer) && int.TryParse(denom, out int intDenom))
			{
				if (int.TryParse(whole, out int intWhole))
				{
					fr2 = new Fraction(intNumer, intDenom, intWhole);
				}
				else
				{
					fr2 = new Fraction(intNumer, intDenom, intWhole);
				}
				fr2 = fr2.writeFract(fr2);
			}
			else
			{
				return View();
			}
			var Result = new Result(fr1, operation, fr2, new Fraction());
			switch (operation)
			{
				case "+":
					Result.Res = fr1.plus(fr2);
					break;
				case "-":
					Result.Res = fr1.minus(fr2);
					break;
				case "*":
					Result.Res = fr1.mult(fr2);
					break;
				case "/":
					Result.Res = fr1.div(fr2);
					break;
				default: return View();
			}
			Results.Add(Result);
			string str = JsonSerializer.Serialize(Results);
			using (StreamWriter sw = new StreamWriter("result.json", false))
			{
				sw.WriteLine(str);
			}
			return View(Results);
		}
	}
}