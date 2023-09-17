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
            bitInfo.AddDefault(11, 11);
            bitInfo.AddInfo(nameof(this.BaseR), 8, 6);
        }
        public bool IsPC { get; set; }
        public int PC { get; set; }
        public Registers BaseR { get; set; }
    }
}
