using System.Collections.Generic;
using System.Linq;
using Data;
using TinyCsvParser.Mapping;

namespace BLL.Csv
{
    public static class CsvConverter
    {
        public static List<Point> ConvertDataToPoints(IEnumerable<CsvMappingResult<RingsData>> data)
        {
            return data.Select(mappingResult => 
                    new Point(
                        double.Parse(mappingResult.Result.Radius), 
                        double.Parse(mappingResult.Result.Intensity)))
                .ToList();
        }
    }
}