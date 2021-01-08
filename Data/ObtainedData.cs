using System.Collections.Generic;
using TinyCsvParser.Mapping;

namespace Data
{
    public static class ObtainedData
    {
        public static List<CsvMappingResult<RingsData>> FirstFileData { get; set; }
        public static List<CsvMappingResult<RingsData>> SecondFileData{ get; set; }
        public static List<CsvMappingResult<RingsData>> ThirdFileData{ get; set; }
        public static List<CsvMappingResult<RingsData>> FourthFileData{ get; set; }
        
        public static List<Point> FirstPoints { get; set; }
        public static List<Point> SecondPoints { get; set; }
        public static List<Point> ThirdPoints { get; set; }
        public static List<Point> FourthPoints { get; set; }
        
        public static List<Point> FirstMax { get; set; } 
        public static List<Point> SecondMax { get; set; } 
        public static List<Point> ThirdMax { get; set; } 
        public static List<Point> FourthMax { get; set; } 
        public static List<Point> FirstMin { get; set; } 
        public static List<Point> SecondMin { get; set; } 
        public static List<Point> ThirdMin { get; set; } 
        public static List<Point> FourthMin { get; set; } 
        
    }
}