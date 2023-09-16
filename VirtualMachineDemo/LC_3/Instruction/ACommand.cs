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
            bitInfo = new BitInfo(this);
        }
        public BitInfo bitInfo { get; set; }
        public string LableName { get; set; }
        public InstructionSet InstructionSet { get; private set; }
        public abstract string ToBin();
        public abstract string ToASM();
        public abstract void BinToCommand(string bin);
        public abstract void BinToCommand(int bin);
        public abstract void ASMToCommand(string asm);
    }
}
