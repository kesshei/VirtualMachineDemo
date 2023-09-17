using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class BLKWCommand : ACommand
    {
        public BLKWCommand() : base(InstructionSet.BLKW)
        {
            bitInfo.AddInfo(nameof(this.InstructionSet), 15, 12);
        }
    }
}
