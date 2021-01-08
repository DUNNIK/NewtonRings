using System;

namespace NewtonRings.Exceptions
{
    public class TextBoxException : Exception
    {
        public TextBoxException() : base("There is no information in the textbox field.")
        {
        }
    }
}