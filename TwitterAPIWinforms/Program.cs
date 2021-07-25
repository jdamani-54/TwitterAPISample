﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using TwitterAPIWinforms.Interfaces;
using TwitterAPIWinforms.Services;

namespace TwitterAPIWinforms
{    
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; set; }
        static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<ITwitterAuthenticationService, TwitterAuthenticationService>();
            services.AddTransient<ITweetReadService, TweetReadService>();
            ServiceProvider = services.BuildServiceProvider();
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConfigureServices();
            Application.Run(new Form1());
        }
    }
}
