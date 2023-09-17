﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class ADDCommand : ACommand
    {
        public ADDCommand() : base(InstructionSet.ADD)
        {
            bitInfo.AddInfo(nameof(this.InstructionSet), 15, 12);
            bitInfo.AddInfo(nameof(this.DR), 11, 9);
            bitInfo.AddInfo(nameof(this.SR1), 8, 6);
            bitInfo.AddInfo(nameof(this.IsImmediateNumber), 5, 5, nameof(this.ImmediateNumber), nameof(this.SR2));
            bitInfo.AddInfo(nameof(this.ImmediateNumber), 4, 0);
            bitInfo.AddInfo(nameof(this.SR2), 2, 0);
        }
        /// <summary>
        /// 目的寄存器
        /// </summary>
        public Registers DR { get; }
        /// <summary>
        /// 源寄存器1
        /// </summary>
        public Registers SR1 { get; }
        public bool IsImmediateNumber { get; }
        public int ImmediateNumber { get; private set; }
        /// <summary>
        /// 源寄存器2
        /// </summary>
        public Registers SR2 { get; }
    }
}
