using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class JMPCommand : ACommand
    {
        public JMPCommand() : base(InstructionSet.JMP)
        {
        }
        public Registers BaseR { get; set; }

        public bool RET { get { return BaseR == 111; } }
    }
}
