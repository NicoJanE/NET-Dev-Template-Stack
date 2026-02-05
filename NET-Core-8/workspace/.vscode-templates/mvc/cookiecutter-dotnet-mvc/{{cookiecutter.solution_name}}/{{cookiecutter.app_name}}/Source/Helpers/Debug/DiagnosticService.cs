

namespace {{ cookiecutter.app_name }}.Helpers.Debug      // Omit Soure in namespace
{
    // ❌⚡→ ⟹ ⇒ 

    // ANSI colors and background colors for terminal output
    public struct Color
    {
        // MEMBERS
        private readonly string _name;                     
        //      - ANSI Text Color Codes (RGB 24-bit)
        public static Color Black => new Color("\x1b[38;2;0;0;0m");
        public static Color Red => new Color("\x1b[38;2;255;0;0m");
        public static Color Green => new Color("\x1b[38;2;0;255;0m");
        public static Color Yellow => new Color("\x1b[38;2;255;255;0m");
        public static Color Blue => new Color("\x1b[38;2;0;0;255m");
        public static Color Magenta => new Color("\x1b[38;2;255;0;255m");
        public static Color Cyan => new Color("\x1b[38;2;0;255;255m");
        public static Color White => new Color("\x1b[38;2;255;255;255m");
         public static Color Reset =>new Color("\x1b[0m");   
        //      - ANSI Background Color Codes (RGB 24-bit)
        public static Color BKBlack => new Color("\x1b[48;2;0;0;0m");
        public static Color BKBlackAlMost => new Color("\x1b[48;2;50;50;50m");     // Default
        public static Color BKMediumGray => new Color("\x1b[48;2;64;64;64m");  
        public static Color BKLightDark => new Color("\x1b[48;2;80;80;80m");   
        public static Color BKRed => new Color("\x1b[48;2;255;0;0m");
        public static Color BKGreen => new Color("\x1b[48;2;0;255;0m");
        public static Color BKYellow => new Color("\x1b[48;2;255;255;0m");
        public static Color BKBlue => new Color("\x1b[48;2;0;0;255m");
        public static Color BKMagenta => new Color("\x1b[48;2;255;0;255m");
        public static Color BKCyan => new Color("\x1b[48;2;0;255;255m");
        public static Color BKWhite => new Color("\x1b[48;2;255;255;255m");        
        //      - Defaults 
        public static  Color defaultColor = Color.Red;
        public static  Color defaultBKColor = Color.BKBlackAlMost;
        
        // CTOR(s)
        public Color(string name)
        {
            _name = name;
        }

        // METHODS
        public override string ToString()   {   return _name ;}

    }   // End struct

    /// <summary>
    ///  Debug options for developers. Currently
    ///     - PrintServiceHierarchy                 →  Prints the registered services   
    ///     - Clear                                           →  Clears the console, also for redirected console
    ///     - Write                                          →  Write to console,optional different text and background colors.
    /// </summary>
    public class DiagnosticService
    {
        // MEMBERS
        private readonly IServiceProvider                   _serviceProvider;
        private IEnumerable<ServiceDescriptor>?     _dbg_Descriptors2=null;
        
        // CTOR(s)
        public DiagnosticService(IServiceProvider serviceProvider, IEnumerable<ServiceDescriptor> descriptors)
        {
            _serviceProvider = serviceProvider;  
            _dbg_Descriptors2 = descriptors;
        }

        // METHODS

        /// <summary>
        /// Refresh the descriptor snapshot with fresh data from the DI container
        /// </summary>
        public void RefreshSnapshot(IEnumerable<ServiceDescriptor> descriptors)
        {
            if (descriptors != null)
                _dbg_Descriptors2 = descriptors;
        }

        /// <summary>
        ///  Output  the defined services in the terminal
        /// </summary>
        public void PrintServiceHierarchy(string callSite="Not defined", bool IgnoreOthers=true)
        {
            // Option 1 (current): Pass descriptors via constructor - no DI setup needed
            var services = _dbg_Descriptors2;   // Passed in via constructor

           // Option 2 (alternative): Get descriptors from DI container
           // var services = _serviceProvider.GetService<IEnumerable<ServiceDescriptor>>();
           // Requires in Program.cs: builder.Services.AddSingleton<IEnumerable<ServiceDescriptor>>(dbg_Descriptors);

           // Lifetime Notes:  Singleton = one instance forever, Scoped = one per request, Transient = new each time
            
            Write("\nServices in use:\n----------------\n",Color.Cyan, Color.BKBlackAlMost);    
            Write($"CALL SITE:{callSite}\n");
            foreach (var service in services)
            {
                if( service != null)
                {                                        
                    if (service.ImplementationInstance != null)
                    {
                        Write($"Instance direct created : {service.ImplementationInstance}\n",Color.Cyan, Color.BKBlackAlMost);       // Like: builder.Services.AddSingleton(new Logger());

                        Write($"\t-  Lifetime: {service.Lifetime}\n", Color.Yellow,Color.BKBlackAlMost);
                        Write($"\t-  Service Type (Interface): {service.ServiceType}\n", Color.Yellow, Color.BKBlackAlMost);
                        if (service.ImplementationType != null)
                            Write($"\t-  Implementation Type: {service.ImplementationType}\n", Color.Yellow, Color.BKBlackAlMost);
                        Write($"\t-  Actual Runtime Type: {service.ImplementationInstance.GetType()}\n", Color.Yellow, Color.BKBlackAlMost);
                    }
                    else if (service.Lifetime == ServiceLifetime.Singleton)
                    {
                        // For singletons, try to get the instance to check if it's been created
                        try
                        {
                            var instance = _serviceProvider.GetService(service.ServiceType);
                            if (instance != null)
                            {
                                Write($"Instance lazy created(by Interface) : {instance}\t _serviceProvider.Get \n",Color.Green, Color.BKBlackAlMost);      // Like: builder.Services.AddSingleton<ILogger, ConsoleLogger>();
                                Write($"\t-  Lifetime: {service.Lifetime}\n", Color.Green,Color.BKBlackAlMost);
                                Write($"\t-  Service. Type(Interface): {service.ServiceType}\n", Color.Yellow,Color.BKBlackAlMost);
                                if (service.ImplementationType != null)
                                    Write($"\t-  Implementation Type: {service.ImplementationType}\n", Color.Yellow, Color.BKBlackAlMost);
                                Write($"\t-  Actual Runtime Type: {instance.GetType()}\n", Color.Yellow, Color.BKBlackAlMost);
                            }
                        }
                        catch
                        {
                            // Service couldn't be instantiated (missing dependencies, invalid registration, etc.)
                        }
                    }
                }
            }
            if(! IgnoreOthers)
            {
                Write("\nAll Available Services (Type -> Implementation):\n------------------------------------------------\n",Color.Magenta, Color.BKBlackAlMost);    
                Write($"CALL SITE:{callSite}\n", Color.Magenta, Color.BKBlackAlMost);
                foreach (var service in services)
                {
                    if( service != null)                                    
                        Write($"\t-  Service. Type(Interface): {service.ServiceType}\n", Color.Magenta,Color.BKBlackAlMost);
                        if (service.ImplementationType != null)
                            Write($"\t-  Implementation Type: {service.ImplementationType}\n", Color.Magenta, Color.BKBlackAlMost);                        
                }
            }
        }

        /// <summary>
        /// Console.Clear does not work for redirected Terminal like i.e Docker
        /// </summary>
        public void Clear()
        {
            if (!Console.IsOutputRedirected)        
                Console.Clear();        
            else
                Console.Write("\x1b[2J\x1b[H");
            }

        /// <summary>
        /// Write text to console, including redirected consoles
        /// </summary>
        public void Write(string text, Color? c=null, Color? bk=null )       //  ⚡ Cannot default to: c=Color.defaultColor (not compile-time constant)
        {            
            c ??= Color.defaultColor;                
            bk ??= Color.defaultBKColor;            
            Console.Write(bk.ToString() + c .ToString()+ text + Color.Reset.ToString()); 
        }    


    }       // End Class DiagnosticService


}           // Namespace: {{ cookiecutter.app_name }}.Helpers.Debug