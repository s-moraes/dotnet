//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using Microsoft.ML.Data;

namespace SampleClassification.Model
{
    public class ModelInput
    {
        [ColumnName("test1"), LoadColumn(0)]
        public float Test1 { get; set; }


        [ColumnName("test2"), LoadColumn(1)]
        public float Test2 { get; set; }


        [ColumnName("hourly"), LoadColumn(2)]
        public bool Hourly { get; set; }


        [ColumnName("age"), LoadColumn(3)]
        public float Age { get; set; }


        [ColumnName("job"), LoadColumn(4)]
        public string Job { get; set; }


        [ColumnName("income"), LoadColumn(5)]
        public float Income { get; set; }


        [ColumnName("satisfac"), LoadColumn(6)]
        public string Satisfac { get; set; }


    }
}
