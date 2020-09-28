using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WorkerService1.Mappers;
using WorkerService1.Models;

namespace WorkerService1.Services
{
    public class CSVServices : IHostedService, IDisposable
    {
        private readonly ILogger<CSVServices> _logger;

        public CSVServices(ILogger<CSVServices> logger)
        {
            _logger = logger;
        }

        public CSVServices()
        {
        }

        public async Task<string> SayHello()
        {
            var helloString = "Hello from CSVService";
            return await Task.FromResult(helloString);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StartAsync called in CSVServices");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StopAsync called in CSVServices");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogInformation("Dispose called in CSVServices");
        }

        public List<Person> ReadCSVFile(string location)
        {
            try
            {
                using (var reader = new StreamReader(location, Encoding.Default))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.RegisterClassMap<PersonMap>();
                    var records = csv.GetRecords<Person>().ToList();
                    return records;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void WriteCSVFile(string path, List<Person> Person)
        {
            using (StreamWriter writer = new StreamWriter(path))
            using (CsvWriter cw = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                cw.WriteHeader<Person>();
                cw.NextRecord();
                foreach (Person stu in Person)
                {
                    cw.WriteRecord<Person>(stu);
                    cw.NextRecord();
                }
            }
        }

        // public void ReadWriteCSV()
        // {
        //     Console.WriteLine("Start CSV File Reading...");
        //     var _personService = new CSVServices();
        //     var path = @"C:\Users\Peti\Source\Repos\predator006-P\WorkerService1\WorkerService1\csv\person.csv";
   
        //     //Here We are calling function to read CSV file
        //     var resultData = _personService.ReadCSVFile(path);
         
        //     //Create an object of the person class
        //     Person person = new Person();
        //     person.Firstname = "Thomas";
        //     person.Surname = "Big";
        //     person.Sex = "male";
        //     person.Age = 24;
        //     person.Status = "single";

        //     resultData.Add(person);
        //     //Here We are calling function to write file
   
        //     _personService.WriteCSVFile(@"C:\Users\Peti\Source\Repos\predator006-P\WorkerService1\WorkerService1\csv\NewpersonFile.csv", resultData);
        //     //Here D: Drive and Tutorials is the Folder name, and CSV File name will be "NewpersonFile.csv"
   
        //     Console.WriteLine("New File Created Successfully.");
        // }
    }
}