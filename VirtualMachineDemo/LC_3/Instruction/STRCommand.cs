using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class STRCommand : ACommand
    {
        public STRCommand() : base(InstructionSet.STR)
        {
        }
        public Registers SR { get; set; }
        public Registers BaseR { get; set; }
        public int offset6 { get; set; }
    }
}
