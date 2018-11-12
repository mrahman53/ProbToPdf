﻿using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProbToPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                //.WriteTo.File("log.txt")
                .CreateLogger();

            //initializing services
            var services = new ServiceCollection()
                .AddTransient<HtmlWeb>()
                //.AddTransient<ICar, Car>()
                .BuildServiceProvider();
            
            //Stopwatch stopwatch = Stopwatch.StartNew();

            Book book = new Book("book.json", services);

            book.Process();

            Downloader downloader = new Downloader(book);
            Downloader.Download(book);
            
            PDFGenerator.Generate(book);
            
            //stopwatch.Stop();

            // Write hours, minutes and seconds.
            //Console.WriteLine("Time elapsed: {0:hh\\:mm\\:ss}", stopwatch.Elapsed);
            Console.ReadLine();
        }
    }
}