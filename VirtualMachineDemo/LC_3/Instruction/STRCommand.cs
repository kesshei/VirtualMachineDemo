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
            bitInfo.AddInfo(nameof(this.InstructionSet), 15, 12);
            bitInfo.AddInfo(nameof(this.SR), 11, 9);
            bitInfo.AddInfo(nameof(this.BaseR), 8, 6);
            bitInfo.AddInfo(nameof(this.offset6), 5, 0);
        }
        public Registers SR { get; set; }
        public Registers BaseR { get; set; }
        public int offset6 { get; set; }
    }
}
