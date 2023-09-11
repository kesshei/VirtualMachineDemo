using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class LDRCommand : ACommand
    {
        public LDRCommand() : base(InstructionSet.LDR)
        {
        }
        /// <summary>
        /// 目标寄存器
        /// </summary>
        public Registers DR { get; set; }
        public Registers BaseR { get; set; }
        public int offset6 { get; set; }
    }
}
