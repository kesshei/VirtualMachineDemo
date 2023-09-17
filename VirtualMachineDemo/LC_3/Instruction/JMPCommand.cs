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
            bitInfo.AddInfo(nameof(this.InstructionSet), 15, 12);
            bitInfo.AddInfo(nameof(this.BaseR), 8, 6);
        }
        public Registers BaseR { get; set; }

        public bool RET { get { return (int)BaseR == 111; } }

    }
}
