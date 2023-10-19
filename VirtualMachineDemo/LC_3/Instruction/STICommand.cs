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
            bitInfo.AddInfo(nameof(this.InstructionSet), 15, 12);
            bitInfo.AddInfo(nameof(this.SR), 11, 9);
            bitInfo.AddInfo(nameof(this.PC), 8, 0);
        }
        public Registers SR { get; set; }
        public ushort PC { get; set; }
    }
}
