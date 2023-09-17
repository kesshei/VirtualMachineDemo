using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class STRINGZCommand : ACommand
    {
        public STRINGZCommand() : base(InstructionSet.STRINGZ)
        {
            bitInfo.AddInfo(nameof(this.InstructionSet), 15, 12);
        }
    }
}
