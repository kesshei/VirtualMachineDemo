using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class STCommand : ACommand
    {
        public STCommand() : base(InstructionSet.ST)
        {
            bitInfo.AddInfo(nameof(this.InstructionSet), 15, 12);
            bitInfo.AddInfo(nameof(this.SR), 11, 9);
            bitInfo.AddInfo(nameof(this.PC), 8, 0);
        }
        /// <summary>
        /// 目标寄存器
        /// </summary>
        public Registers SR { get; set; }
        public int PC { get; set; }

        public override void ASMToCommand(string asm)
        {
            throw new NotImplementedException();
        }

        public override void BinToCommand(string bin)
        {
            bitInfo.BinToCommand(bin);
            var code = ToBin();
            if (code == bin)
            {
                Console.WriteLine($"{nameof(STCommand)}编码解析成功!");
            }
        }

        public override void BinToCommand(int bin)
        {
            throw new NotImplementedException();
        }

        public override string ToASM()
        {
            throw new NotImplementedException();
        }

        public override string ToBin()
        {
          return  bitInfo.ToBin();
        }
    }
}
