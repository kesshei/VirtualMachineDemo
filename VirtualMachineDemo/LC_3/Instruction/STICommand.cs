using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class STICommand : ACommand
    {
        public STICommand() : base(InstructionSet.STI)
        {
        }
        public Registers SR { get; set; }
        public int PC { get; set; }
    }
}
