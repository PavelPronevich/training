﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelephoneStation
{
   public class BlockPortEventArgs
    {
        public Port port { get; set; }
        public bool IsSwitched { get; set; }
    }
}