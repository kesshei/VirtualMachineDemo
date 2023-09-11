using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class TRAPCommand : ACommand
    {
        public TRAPCommand() : base(InstructionSet.TRAP)
        {
        }
        public int Trapverct { get; set; }
    }
}
