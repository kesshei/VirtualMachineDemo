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
            bitInfo.AddInfo(nameof(this.InstructionSet), 15, 12);
            bitInfo.AddInfo(nameof(this.DR), 11, 9);
            bitInfo.AddInfo(nameof(this.SR1), 8, 6);
            bitInfo.AddInfo(nameof(this.IsImmediateNumber), 5, 5);
            bitInfo.AddInfo(nameof(this.SR2), 2, 1);
            bitInfo.AddInfo(nameof(this.ImmediateNumber), 4, 0);
        }
        /// <summary>
        /// 目的寄存器
        /// </summary>
        public Registers DR { get; private set; }
        /// <summary>
        /// 源寄存器1
        /// </summary>
        public Registers SR1 { get; private set; }
        public bool IsImmediateNumber { get; private set; }
        /// <summary>
        /// 源寄存器2
        /// </summary>
        public Registers SR2 { get; private set; }
        public int ImmediateNumber { get; private set; }

        public override void ASMToCommand(string asm)
        {
            throw new NotImplementedException();
        }

        public override void BinToCommand(string bin)
        {
            var dr = bin.Skip(4).Take(4);
            var sr1 = bin.Skip(4).Skip(4).Take(3);
            var isimi = bin.Skip(4).Skip(4).Skip(3).Take(1).First();

            DR = (Registers)Convert.ToInt32(new string(dr.ToArray()), 2);
            SR1 = (Registers)Convert.ToInt32(new string(sr1.ToArray()), 2);
            if (isimi == '0')
            {
                IsImmediateNumber = false;
                //var sr2 = bin.Skip(4).Skip(4).Skip(3).Skip(1).Skip();
                //SR2 = (Registers)Convert.ToInt32(new string(sr2.ToArray()), 2);
            }
            else
            {
                IsImmediateNumber = true;
                var imm = bin.Skip(4).Skip(4).Skip(3).Skip(1);
                ImmediateNumber = Convert.ToInt32(new string(imm.ToArray()), 2);
            }
            var code = ToBin();
            if (code == bin)
            {
                Console.WriteLine($"{nameof(ANDCommand)}编码解析成功!");
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
            var head_bin = Convert.ToString((int)this.InstructionSet, 2).PadLeft(4, '0');
            var dr_bin = Convert.ToString((int)this.DR, 2).PadLeft(4, '0');
            var sr1_bin = Convert.ToString((int)this.SR1, 2).PadLeft(3, '0');
            var isimm_bin = IsImmediateNumber ? "1" : "0";
            if (IsImmediateNumber)
            {
                var imm_bin = Convert.ToString((int)this.ImmediateNumber, 2).PadLeft(5, '0');
                var bin = $"{head_bin}{dr_bin}{sr1_bin}{isimm_bin}{imm_bin}";
                return bin;
            }
            else
            {
                var sr2_bin = Convert.ToString((int)this.SR2, 2).PadLeft(3, '0');
                var bin = $"{head_bin}{dr_bin}{sr1_bin}{isimm_bin}00{sr2_bin}";
                return bin;
            }
        }
    }
}
