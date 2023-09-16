using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class JSRCommand : ACommand
    {   
        public JSRCommand() : base(InstructionSet.JSR)
        {
        }
        public bool IsPC { get; set; }
        public int PC { get; set; }
        public Registers BaseR { get; set; }

        public override void ASMToCommand(string asm)
        {
            throw new NotImplementedException();
        }

        public override void BinToCommand(string bin)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
