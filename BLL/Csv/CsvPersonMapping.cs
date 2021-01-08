using Data;
using TinyCsvParser.Mapping;

namespace BLL.Csv
{
    public class CsvPersonMapping : CsvMapping<RingsData>
    {
        public CsvPersonMapping()
            : base()
        {
            MapProperty(0, x => x.Radius);
            MapProperty(1, x => x.Intensity);
        }
    }
}