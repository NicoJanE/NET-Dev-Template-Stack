// DOCUMENTATION CONVENTIONS
// ============================================================================
// -    The word "our" indicates code from this application or its custom libraries
// -    Framework/third-party code is referenced **without** "our"
// -    Additional conventions to be documented as needed
// ============================================================================

// CODE CONVENTIONS
// ============================================================================
// -    Only `local static` functions in Program.cs are prefixed with "App_" to distinguish them from .NET framework calls
//      Note: this is an anti partern for method in a namespace/class
// -    Additional conventions to be documented as needed
// ============================================================================


// Begin Main
// -------------------------------------------------------------------------------------------------------------------------------------------------

using {{ cookiecutter.app_name }};
using {{ cookiecutter.app_name }}.Helpers.Debug;

var builder = WebApplication.CreateBuilder(args);
App_RegisterServices(builder);                                                                   // First service, define the services 
var app = builder.Build();                                 
App_InstantiateServices(app);                                                                   // Second service,  after defining and  building,  instantiate the  service(s)


App_ConfigurePipeline(app);
App_Dbg_PrintDiagnostics(app);                                                              // Third service, call a method on the instanciated service
app.Run();

// End Main
// -------------------------------------------------------------------------------------------------------------------------------------------------



// Static local methods separate the different application concerns
// =====================================================================================
//

// Service Registration and DI Container Configuration
// -------------------------------------------------------------------------------------------------------------------------------------------------
//  Purpose:
//    - Register all application services into the DI container (builder.Services)
//    - Configure service behavior, lifetimes, and dependencies
//    - Set up framework services (MVC, logging, etc.) and custom services
//
//  Execution Sequence:
//    - Runs BEFORE builder.Build() to define all services before instantiation
//
//  Core Registrations:
//    - AddControllersWithViews(): Registers MVC framework services (controllers, model binding, view rendering, tag helpers, filters)
//    - AddSingleton<DiagnosticService>(): Development utility for inspecting registered services
//    - AddSingleton<IEnumerable<ServiceDescriptor>>(): Snapshot of all registered services for debugging
//
//  Service Lifetimes (DI patterns used here):
//    - Singleton: Services created once for the application lifetime (DiagnosticService, service descriptors)
//    - Transient: Created fresh for each request (typically for stateless operations)
//    - Scoped: Created once per HTTP request (typical for database contexts)
//
//  Configuration Impact:
//    - Custom Razor view locations: Views are located in /Source/Views instead of default /Views
//    - These registrations directly affect how ASP.NET Core handles incoming requests
//
static void App_RegisterServices(WebApplicationBuilder builder)
{    
    builder.Services.AddControllersWithViews()

        // Customize the View locations, indicate where the views are located in the project (custom /Source/ path) 
        // By default the views are expected in the root folder of the project (`./Views`), we use: `./Source/Views`
        .AddRazorOptions(options =>
        {
            options.ViewLocationFormats.Add("/Source/Views/{1}/{0}.cshtml");            // {1} = controller name {0} = action name
            options.ViewLocationFormats.Add("/Source/Views/Shared/{0}.cshtml");
        });

    // Register our DiagnosticService for inspecting and displaying registered services during development
    // For more information see the class documentation
    builder.Services.AddSingleton<DiagnosticService>();

    // For debugging purposes: Capture snapshot of service descriptors for DiagnosticService to display
    // This allows DiagnosticService.PrintServiceHierarchy() to show all registered services
    var dbg_Descriptors = builder.Services.ToList();    // List of service description objects
    builder.Services.AddSingleton<IEnumerable<ServiceDescriptor>>(dbg_Descriptors);
    
}


// Service Instantiation and Initialization
// -------------------------------------------------------------------------------------------------------------------------------------------------
//  Dependency: Requires App_RegisterServices to run first
//
//  Purpose:
//    - Eagerly instantiate singleton services that need early initialization
//    - Establish dependency chains between services
//    - Prepare services before the HTTP pipeline begins handling requests
//
//  Why this step exists:
//    - app.Build() creates the service provider, but doesn't instantiate singletons
//    - Some services (like DiagnosticService) need explicit initialization
//    - Failing to instantiate here could cause first-request delays
//
//  What happens here:
//    1. Retrieves DiagnosticService from the DI container (triggers instantiation)
//    2. Refreshes its service descriptor snapshot (captures current state)
//    3. Prepares debug information for later diagnostic output
//
//  Execution Sequence:
//    - Runs AFTER builder.Build() when the DI container is fully configured
//    - Runs BEFORE App_ConfigurePipeline to ensure services are ready
//
static void App_InstantiateServices(WebApplication app)
{
    // Instantiate singletons that need to be created early
    var diagnosticService = app.Services.GetService<DiagnosticService>();
    
    // Refresh snapshot with fresh data (now includes instantiated services)
    var freshSnapshot = app.Services.GetService<IEnumerable<ServiceDescriptor>>();
    if (diagnosticService != null && freshSnapshot != null)
        diagnosticService.RefreshSnapshot(freshSnapshot);
}


// Configure HTTP Request Pipeline
// -------------------------------------------------------------------------------------------------------------------------------------------------
//  Purpose:
//    - Configure middleware components in the correct processing order
//    - Set up exception handling, security headers, and routing
//    - Define how incoming HTTP requests are processed through the pipeline
//
//  Middleware Order (CRITICAL - violations cause runtime failures):
//    1. Exception handling (catches errors before they reach the client)
//    2. HTTPS redirection (enforces secure connections)
//    3. Static file serving (serves CSS, JS, images without routing overhead)
//    4. Routing setup (establishes the routing system)
//    5. Authorization (checks permissions for protected resources)
//    6. Route mapping (maps URLs to controller actions)
//
//  Environment-Specific Behavior:
//    - Development: Skips HSTS and detailed exception handler (uses detailed error pages)
//    - Production: Enables HSTS (30 days by default) and generic exception handler
//    - HSTS (HTTP Strict-Transport-Security): Forces HTTPS for 30 days, prevents downgrade attacks
//
//  Route Pattern Explained:
//    - Pattern: "{controller=Home}/{action=Index}/{id?}"
//    - Default controller: Home (if no controller specified in URL)
//    - Default action: Index (if no action specified)
//    - Optional parameter: id (? makes it optional, useful for detail pages)
//    - Example URLs:
//      * /                          → HomeController.Index()
//      * /Products                  → ProductsController.Index()
//      * /Products/Details/5        → ProductsController.Details(5)
//
//  Extension Points for Custom Middleware:
//    - Add authentication middleware: app.UseAuthentication();
//    - Add CORS: app.UseCors();
//    - Add custom logging: app.Use(async (context, next) => { ... });
//    - These should be added BEFORE app.MapControllerRoute()
//
static void App_ConfigurePipeline(WebApplication app)
{        
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();                                                          // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.            
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    // Route configuration: Sets default controller and action
    // When a request arrives, ASP.NET Core automatically:
    //   1. Creates an instance of the matching controller (HomeController)
    //   2. Injects dependencies (ILogger) via the DI container
    //   3. Calls the matching action method (Index by default)
    //     (). Activates the controller: HomeController.cs. Which is instructed to action method: 'Index' (See: Controllers/HomeControlers.cs))
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
}


// Development Diagnostics - Print Service Configuration
// -------------------------------------------------------------------------------------------------------------------------------------------------
//  Purpose (Development Only):
//    - Display all registered services in the DI container
//    - Show service lifetimes and implementation types
//    - Help developers verify services are registered correctly
//    - Useful for debugging dependency injection issues
//
//  When it runs:
//    - Executes AFTER App_ConfigurePipeline, just before app.Run()
//    - Output appears in console immediately when application starts
//    - BEFORE the first HTTP request is processed
//
//  Console Redirection Handling:
//    - IsOutputRedirected: true when output is piped or captured (Docker, CI/CD)
//    - IsOutputRedirected: false when output goes to interactive terminal
//    - In redirected environments: Uses ANSI escape code "\x1b[2J\x1b[H" to clear 
//      the screen and us colors (ANSI works everywhere)
//
//  Expected output:
//    - Shows active singleton instances currently in use
//    - Lists all registered services with their types and implementations
//    - Color-coded output for easy reading (see DiagnosticService.cs for colors)
//    - Includes the call site ("Program.cs") for debugging multiple diagnostic calls
//
//  Safe to remove:
//    - This method is purely diagnostic and doesn't affect application behavior
//    - Remove in production if startup console noise is undesired
//
static void App_Dbg_PrintDiagnostics(WebApplication app)
{
    // Fetch the diagnostic service to print the recorded service descriptors
    var serviceProvider = app.Services;
    var diagnosticService = serviceProvider.GetService<DiagnosticService>();
    
    if (diagnosticService != null)
    {
        if (Console.IsOutputRedirected)
            diagnosticService.Clear();
        else
            Console.Clear();
        diagnosticService.PrintServiceHierarchy("Program.cs");
    }
}