using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    /// <summary>
    /// 指令集
    /// </summary>
    public abstract class ACommand
    {
        public ACommand(InstructionSet InstructionSet)
        {
            this.InstructionSet = InstructionSet;
        }
        public InstructionSet InstructionSet { get; private set; }
    }
}
