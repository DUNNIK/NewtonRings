using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace BLL.Csv
{
    public static class CsvReader
    {
        public static List<CsvMappingResult<RingsData>> ReadCsv(string filePath)
        {
            var csvParserOptions = new CsvParserOptions(true, ';');
            var csvMapper = new CsvPersonMapping();
            var csvParser = new CsvParser<RingsData>(csvParserOptions, csvMapper);

            var result = csvParser
                .ReadFromFile(filePath, Encoding.ASCII)
                .ToList();
            
            return result;
        }
    }
}