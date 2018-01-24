using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadaca2_Pozoriste
{
    public class FormatUnosaException : Exception
    {
        public FormatUnosaException() {}
        public FormatUnosaException(string message): base(message) {}
        public FormatUnosaException(string message, Exception inner) : base(message, inner) { }
    }
}
