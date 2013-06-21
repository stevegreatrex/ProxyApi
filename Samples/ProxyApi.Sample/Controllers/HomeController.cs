using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProxyApi.Sample.Models;

namespace ProxyApi.Sample.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult OtherPage()
		{
			return PartialView();
		}

		public ActionResult NoAntiForgeryToken()
		{
			return View();
		}
	}
}
