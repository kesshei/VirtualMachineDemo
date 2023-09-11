using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class ANDCommand : ACommand
    {
        public ANDCommand() : base(InstructionSet.AND)
        {
        }
        /// <summary>
        /// 目的寄存器
        /// </summary>
        public Registers DR { get; }
        /// <summary>
        /// 源寄存器1
        /// </summary>
        public Registers SR1 { get; }
        public bool ImmediateNumber { get; }
        /// <summary>
        /// 源寄存器2
        /// </summary>
        public Registers SR2 { get; }
    }
}
