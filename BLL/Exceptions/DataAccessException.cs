using System;

namespace BLL.Exceptions
{
    public class DataAccessException : Exception
    {
        public DataAccessException() : base("The data is empty. Access is not possible")
        {
        }
    }
}