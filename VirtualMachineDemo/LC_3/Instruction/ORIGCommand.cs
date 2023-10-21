using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class ORIGCommand : ACommand
    {
        public ORIGCommand(string bin) : base(InstructionSet.ORIG)
        {
            PC = Convert.ToUInt16(bin, 2);
        }
        public ushort PC { get; set; }
    }
}
