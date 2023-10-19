using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class TRAPCommand : ACommand
    {
        public TRAPCommand() : base(InstructionSet.TRAP)
        {
            bitInfo.AddInfo(nameof(this.InstructionSet), 15, 12);
            bitInfo.AddInfo(nameof(this.Trapverct), 7, 0);
        }
        public ushort Trapverct { get; set; }
        public override bool Check(string bin)
        {
            return new string(bin.Take(8).ToArray()) == "11110000";
        }
    }
}
