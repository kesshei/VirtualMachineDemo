using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3
{
    /// <summary>
    /// 虚拟机
    /// 长度: 0x0000~0xffff
    /// 寄存器: R0-R7
    /// PC寄存器
    /// 标志寄存器
    /// </summary>
    public class VM
    {
        public VM()
        {
            Memory = new ushort[UInt16.MaxValue];
            foreach (Registers item in Enum.GetValues(typeof(Registers)))
            {
                Reg.Add(item, 0);
            }
        }
        public UInt16[] Memory = new UInt16[UInt16.MaxValue];
        public Dictionary<Registers, UInt16> Reg = new Dictionary<Registers, UInt16>();
        public UInt16 PC
        {
            get
            {
                return Reg[Registers.PC];
            }
            set
            {
                Reg[Registers.PC] = value;
            }
        }
        /// <summary>
        /// 标志寄存器
        /// </summary>
        public FlagRegister COND { get; set; }
        public void Run()
        {
            
        }
    }
    /// <summary>
    /// 寄存器
    /// </summary>
    public enum Registers
    {
        R0 = 0, R1, R2, R3, R4, R5, R6, R7, R8, PC
    }
    /// <summary>
    /// 标志寄存器
    /// </summary>
    public enum FlagRegister
    {
        /// <summary>
        /// 正数
        /// </summary>
        POS = 1,
        /// <summary>
        /// 0
        /// </summary>
        ZRO,
        /// <summary>
        /// 负数
        /// </summary>
        NEG
    }
    /// <summary>
    /// 指令集
    /// </summary>
    public enum InstructionSet
    {
        /// <summary>
        /// 条件分支
        /// </summary>
        BR = 0,
        /// <summary>
        ///  加法
        /// </summary>
        ADD = 1,
        /// <summary>
        /// load
        /// </summary>
        LD = 2,
        /// <summary>
        /// store
        /// </summary>
        ST = 3,
        /// <summary>
        /// 跳转到寄存器
        /// </summary>
        JSR = 4,
        /// <summary>
        /// 与运算
        /// </summary>
        AND = 5,
        /// <summary>
        /// 加载寄存器
        /// </summary>
        LDR = 6,
        /// <summary>
        /// 存储寄存器
        /// </summary>
        STR = 7,
        RTI = 8,
        /// <summary>
        /// 取反
        /// </summary>
        NOT = 9,
        /// <summary>
        /// 间接寻址 加载
        /// </summary>
        LDI = 10,
        /// <summary>
        /// 存储 间接寻址
        /// </summary>
        STI = 11,
        /// <summary>
        /// 直接跳
        /// </summary>
        JMP = 12,
        /// <summary>
        /// reserved
        /// </summary>
        RES = 13,
        /// <summary>
        /// 加载偏移地址
        /// </summary>
        LEA = 14,
        /// <summary>
        /// 陷阱，中断
        /// </summary>
        TRAP = 15
    }
}
