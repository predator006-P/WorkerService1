using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WorkerService1.Models;
using WorkerService1.Services;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly CSVServices _cSVServices;

        public Worker(ILogger<Worker> logger, CSVServices cSVServices)
        {
            _cSVServices = cSVServices;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var hello = await _cSVServices.SayHello();
                //_logger.LogInformation(hello);

        
        
                _logger.LogInformation("Start CSV File Reading...");
                var _personService = new CSVServices();
                var path = @"C:\Users\Peti\Source\Repos\predator006-P\WorkerService1\WorkerService1\csv\person.csv";
    
                //Here We are calling function to read CSV file
                var resultData = _personService.ReadCSVFile(path);
            
               // Guid guid = Guid.NewGuid();
                //Create an object of the person class
                // Person person = new Person();
              
                // person.Firstname = "Thomas";
                // person.Surname = "Big";
                // person.Sex = "male";
                // person.Age = 24;
                // person.Status = "single";

               // resultData.Add(person);
                //Here We are calling function to write file
    
                _personService.WriteCSVFile(@"C:\Users\Peti\Source\Repos\predator006-P\WorkerService1\WorkerService1\csv\NewpersonFile.csv", resultData);
                //Here D: Drive and Tutorials is the Folder name, and CSV File name will be "NewpersonFile.csv"
    
                _logger.LogInformation("New File Created Successfully.");

                await Task.Delay(5000, stoppingToken);
            }
        }
        public override async Task StartAsync(CancellationToken cancellationToken) => base.StartAsync(cancellationToken);
        public override async Task StopAsync(CancellationToken cancellationToken) => base.StopAsync(cancellationToken);
    }
}
