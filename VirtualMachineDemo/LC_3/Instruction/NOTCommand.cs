﻿using System;
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
        }
        /// <summary>
        /// 目标寄存器
        /// </summary>
        public Registers DR { get; set; }
        public Registers SR { get; set; }

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

        public override int ToBin()
        {
            throw new NotImplementedException();
        }
    }
}