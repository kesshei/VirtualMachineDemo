using System;
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
            bitInfo.AddInfo(nameof(this.InstructionSet), 15, 12);
            bitInfo.AddInfo(nameof(this.N), 11, 11);
            bitInfo.AddInfo(nameof(this.Z), 10, 10);
            bitInfo.AddInfo(nameof(this.P), 9, 9);
            bitInfo.AddInfo(nameof(this.PC), 8, 0);
        }
        public bool N { get; set; }
        public bool Z { get; set; }
        public bool P { get; set; }
        public ushort PC { get; set; }

    }
}
