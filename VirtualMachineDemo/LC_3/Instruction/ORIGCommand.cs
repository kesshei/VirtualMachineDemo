using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class ORIGCommand : ACommand
    {
        public ORIGCommand() : base(InstructionSet.ORIG)
        {
            bitInfo.AddInfo(nameof(this.InstructionSet), 15, 12);
        }
    }
}
