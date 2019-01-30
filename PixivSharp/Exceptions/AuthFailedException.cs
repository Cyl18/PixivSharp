using System;
using System.Collections.Generic;
using System.Text;

namespace PixivSharp.Exceptions
{
    public class AuthFailedException : Exception
    {
        public AuthFailedException(string message) : base(message)
        {
        }
    }
}
