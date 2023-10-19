using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class JSRCommand : ACommand
    {   
        public JSRCommand() : base(InstructionSet.JSR)
        {
            bitInfo.AddInfo(nameof(this.InstructionSet), 15, 12);
            bitInfo.AddInfo(nameof(this.IsOffset), 11, 11, nameof(this.PC), nameof(this.BaseR));
            bitInfo.AddInfo(nameof(this.PC), 10, 0);
            bitInfo.AddInfo(nameof(this.BaseR), 8, 6);
        }
        public ushort PC { get; set; }
        public bool IsOffset { get; set; }  
        public Registers BaseR { get; set; }
    }
}
