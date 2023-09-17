using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class ENDCommand : ACommand
    {
        public ENDCommand() : base(InstructionSet.END)
        {
            bitInfo.AddInfo(nameof(this.InstructionSet), 15, 12);
        }
    }
}
