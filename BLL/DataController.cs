using System.Collections.Generic;
using BLL.Exceptions;
using Data;
using TinyCsvParser.Mapping;

namespace BLL
{
    public static class DataController
    {
        public static void AddFirstPoints(List<Point> data)
        {
            ObtainedData.FirstPoints = data;
        }
        public static void AddSecondPoints(List<Point> data)
        {
            ObtainedData.SecondPoints = data;
        }
        public static void AddThirdPoints(List<Point> data)
        {
            ObtainedData.ThirdPoints = data;
        }
        public static void AddFourthPoints(List<Point> data)
        {
            ObtainedData.FourthPoints = data;
        }
        public static void AddFirstData(List<CsvMappingResult<RingsData>> data)
        {
            ObtainedData.FirstFileData = data;
        }
        public static void AddSecondData(List<CsvMappingResult<RingsData>> data)
        {
            ObtainedData.SecondFileData = data;
        }
        public static void AddThirdData(List<CsvMappingResult<RingsData>> data)
        {
            ObtainedData.ThirdFileData = data;
        }
        public static void AddFourthData(List<CsvMappingResult<RingsData>> data)
        {
            ObtainedData.FourthFileData = data;
        }

        public static List<CsvMappingResult<RingsData>> GetFirstData()
        {
            if (ObtainedData.FirstFileData == null) throw new DataAccessException();
            return ObtainedData.FirstFileData;
        }
        public static List<CsvMappingResult<RingsData>> GetSecondData()
        {
            if (ObtainedData.SecondFileData == null) throw new DataAccessException();
            return ObtainedData.SecondFileData;
        }
        public static List<CsvMappingResult<RingsData>> GetThirdData()
        {
            if (ObtainedData.ThirdFileData == null) throw new DataAccessException();
            return ObtainedData.ThirdFileData;
        }
        public static List<CsvMappingResult<RingsData>> GetFourthData()
        {
            if (ObtainedData.FourthFileData == null) throw new DataAccessException();
            return ObtainedData.FourthFileData;
        }
        public static List<Point> GetFirstPoints()
        {
            if (ObtainedData.FirstPoints == null) throw new DataAccessException();
            return ObtainedData.FirstPoints;
        }
        public static List<Point> GetSecondPoints()
        {
            if (ObtainedData.SecondPoints == null) throw new DataAccessException();
            return ObtainedData.SecondPoints;
        }
        public static List<Point> GetThirdPoints()
        {
            if (ObtainedData.ThirdPoints == null) throw new DataAccessException();
            return ObtainedData.ThirdPoints;
        }
        public static List<Point> GetFourthPoints()
        {
            if (ObtainedData.FourthPoints == null) throw new DataAccessException();
            return ObtainedData.FourthPoints;
        }
    }
}