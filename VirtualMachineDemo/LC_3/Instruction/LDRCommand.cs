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
            bitInfo.AddInfo(nameof(this.InstructionSet), 15, 12);
            bitInfo.AddInfo(nameof(this.DR), 11, 9);
            bitInfo.AddInfo(nameof(this.BaseR), 8, 6);
            bitInfo.AddInfo(nameof(this.offset6), 5, 0);
        }
        /// <summary>
        /// 目标寄存器
        /// </summary>
        public Registers DR { get; set; }
        public Registers BaseR { get; set; }
        public ushort offset6 { get; set; }
    }
}
