using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class LEACommand : ACommand
    {
        public LEACommand() : base(InstructionSet.LEA)
        {
            bitInfo.AddInfo(nameof(this.InstructionSet), 15, 12);
            bitInfo.AddInfo(nameof(this.DR), 11, 9);
            bitInfo.AddInfo(nameof(this.PC), 8, 0);
        }
        /// <summary>
        /// 目标寄存器
        /// </summary>
        public Registers DR { get; set;}
        public ushort PC { get; set; }
    }
}
