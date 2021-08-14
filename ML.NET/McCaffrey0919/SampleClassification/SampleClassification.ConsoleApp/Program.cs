//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using System;
using SampleClassification.Model;

namespace SampleClassification.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create single instance of sample data from first line of dataset for model input
            ModelInput sampleData = new ModelInput()
            {
                Hourly = false,
                Age = 66F,
                Job = @"mgmt",
                Income = 52100F,
            };

            // Make a single prediction on the sample data and print results
            var predictionResult = ConsumeModel.Predict(sampleData);

            Console.WriteLine("Using model to make single prediction -- Comparing actual Satisfac with predicted Satisfac from sample data...\n\n");
            Console.WriteLine($"Hourly: {sampleData.Hourly}");
            Console.WriteLine($"Age: {sampleData.Age}");
            Console.WriteLine($"Job: {sampleData.Job}");
            Console.WriteLine($"Income: {sampleData.Income}");
            Console.WriteLine($"\n\nPredicted Satisfac value {predictionResult.Prediction} \nPredicted Satisfac scores: [{String.Join(",", predictionResult.Score)}]\n\n");
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }
    }
}
