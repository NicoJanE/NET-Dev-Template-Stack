//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.CodeAnalysis.Elfie.Serialization;
//using NuGet.Configuration;

namespace {{ cookiecutter.app_name }}.Helpers.Debug      // Omit Soure in namespace
{
    // ❌⚡→ ⟹ ⇒ 

    // Ansi colors and background colors for terminal output
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
    ///     - Clear                                           →  Clears the console, also for redirected consols
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
        ///  Output  the defined services in the terminla
        /// </summary>
        public void PrintServiceHierarchy(string callSite="Not defined")
        {
            var services = _dbg_Descriptors2;
           // Option 2 lighter
           // var services2 = _serviceProvider.GetService<IEnumerable<ServiceDescriptor>>();  
           // NO DI needed in Ctor and in programs.cs  no:  builder.Services.AddSingleton<IEnumerable<ServiceDescriptor>>(dbg_Descriptors);

            Write("\nServices in use:\n----------------\n",Color.Red, Color.BKBlackAlMost);    
            Write($"CALL SITE:{callSite}\n");
            foreach (var service in services)
            {
                if( service != null)
                {                                        
                    if (service.ImplementationInstance != null)
                    {
                        Write($"Instance in use: {service.ImplementationInstance}\n");       
                    }
                    else if (service.Lifetime == ServiceLifetime.Singleton)
                    {
                        // For singletons, try to get the instance to check if it's been created
                        try
                        {
                            var instance = _serviceProvider.GetService(service.ServiceType);
                            if (instance != null)
                            {
                                Write($"Instance in use: {instance}\n");
                            }
                        }
                        catch
                        {
                            // Silently skip if service can't be instantiated
                        }
                    }
                }
            }

            Write("\nAll Available Services (Type -> Implementation):\n------------------------------------------------\n",Color.Magenta, Color.BKBlackAlMost);    
            Write($"CALL SITE:{callSite}\n", Color.Magenta, Color.BKBlackAlMost);
            foreach (var service in services)
            {
                if( service != null)                                    
                     Write($"{service.ServiceType} -> {service.ImplementationType}\n",Color.Magenta, Color.BKBlackAlMost);                
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
        /// Wrtite text to console, including redirected consoles
        /// </summary>
        public void Write(string text, Color? c=null, Color? bk=null )       //  ⚡ Cannot default to: c=Color.defaultColor (not compile-time constant)
        {            
            c ??= Color.defaultColor;                
            bk ??= Color.defaultBKColor;            
            Console.Write(bk.ToString() + c .ToString()+ text + Color.Reset.ToString()); 
        }    


    }       // End Class DiagnosticService


}           // Namespace: {{ cookiecutter.app_name }}.Helpers.Debug