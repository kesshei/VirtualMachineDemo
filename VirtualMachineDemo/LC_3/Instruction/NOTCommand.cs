using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LC_3.Instruction
{
    public class NOTCommand : ACommand
    {
        public NOTCommand() : base(InstructionSet.NOT)
        {
            bitInfo.AddInfo(nameof(this.InstructionSet), 15, 12);
            bitInfo.AddInfo(nameof(this.DR), 11, 9);
            bitInfo.AddInfo(nameof(this.SR), 8, 6);
            bitInfo.AddDefault(5, 0);
        }
        /// <summary>
        /// 目标寄存器
        /// </summary>
        public Registers DR { get; set; }
        public Registers SR { get; set; }
    }
}
