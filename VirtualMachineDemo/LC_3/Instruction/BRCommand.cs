﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class BRCommand : ACommand
    {
        public BRCommand() : base(InstructionSet.BR)
        {
        }
        public bool N { get; set; }
        public bool Z { get; set; }
        public bool P { get; set; }
        public int PC { get; set; }
    }
}
