using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class JSRCommand : ACommand
    {   
        public JSRCommand() : base(InstructionSet.JSR)
        {
        }
        public bool IsPC { get; set; }
        public int PC { get; set; }
        public Registers BaseR { get; set; }
    }
}
