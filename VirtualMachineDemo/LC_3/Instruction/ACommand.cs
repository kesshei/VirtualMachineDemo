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
        public virtual string ToBin()
        {
            return bitInfo.ToBin();
        }
        public virtual string ToASM()
        {
            return string.Empty;
        }
        public virtual void BinToCommand(string bin)
        {
            bitInfo.BinToCommand(bin);
            var code = ToBin();
            if (code == bin)
            {
                Console.WriteLine($"{InstructionSet}编码解析成功!");
            }
        }
        public virtual void BinToCommand(int bin)
        {
         
        }
        public virtual void ASMToCommand(string asm)
        {
         }
    }
}
