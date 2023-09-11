using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class LDCommand : ACommand
    {
        public LDCommand() : base(InstructionSet.LD)
        {
        }
        /// <summary>
        /// 目标寄存器
        /// </summary>
        public Registers DR { get; set; }
        public int PC { get; set; }
    }
}
