using System;
using System.Collections.Generic;
using System.Text;

namespace BorderControl
{
    public interface IIdable
    {
        public string BDay { get; set; }
        bool IsFake(string check);
    }
}
