using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using {{ cookiecutter.app_name }}.Models;
using {{ cookiecutter.app_name }}.Helpers.Debug;

namespace {{ cookiecutter.app_name }}.Controllers;

/// <summary>
/// API Doc.
/// 
/// MS this is allowed by you... Sire?
/// </summary>
public class HomeController : Controller
{
     // Constructor: Receives ILogger through dependency injection
    // ILogger is configured in Program.cs and available here automatically
    private readonly ILogger<HomeController> _logger;
    private readonly DiagnosticService _diagnosticService;

    public HomeController(ILogger<HomeController> logger, DiagnosticService diagnosticService)
    {
        _logger = logger;
        _diagnosticService = diagnosticService;
    }


    /// <summary>
    /// API Doc.
    /// 
    /// 2.  Handles: GET /. 
    ///      Calls the default View( return View(); ) using the name convention: [controller/action] which return Home/Index in this case.
    ///       Remark:  Because we define our sources in an altenative location(./Source/)  we have defined a 'Base path' in Programs.cs, see:
    ///                      AddControllersWithViews() services
    ///
    ///  Note: 1 One can also call and other alternative view like:
    ///        return View("Index2");                                // Points to ./Source/Home/Index2.cshtml
    ///        return View("~/Views/Alt/Index2");            // Points to ./Source/Alt/Index2.cshtml
    ///
    ///  Note: 2 A view can pass a Model like (for  the  default view)
    ///          return View(new MainOverviewModel() )                    //  accessible as @Model in the view
    ///          return View("Index2", new MainOverviewModel());     // For none default view    
    ///
    ///  Note:3 The View is placed inside the  'shared view' /Shared/_Layout.cshtml
    /// 
    /// See <see href="Doc/Create-API-Documentation.md">Create API Documentation</see>.
    /// </summary>
    
    public IActionResult Index()
    {

          // Print the service hierarchy
        _diagnosticService.PrintServiceHierarchy("HomeController.cs");


        return View();
    }

    // Handles: GET /Home/Privacy
    // Returns: Views/Home/Privacy.cshtml
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// API Doc.
    /// 
    /// Handles: GET /Home/Error (when exceptions occur)
    /// Passes ErrorViewModel to view with request details
    /// [ResponseCache]: Prevents browser caching of error page
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }); // pass ErrorViewModel
    }
}
