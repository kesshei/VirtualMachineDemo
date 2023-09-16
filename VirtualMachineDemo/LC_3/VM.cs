using LC_3.Instruction;
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
        public void Run(Dictionary<int, ACommand> program)
        {

        }
        /// <summary>
        /// load asm
        /// </summary>
        public static ACommand[] LoadAsm(string[] asm)
        {
            foreach (var item in asm)
            {

            }
            return null;
        }
        /// <summary>
        /// load bin 
        /// 二进制
        /// </summary>
        public static ACommand[] LoadBin(string[] bincode)
        {
            var aCommands = new List<ACommand>();
            if (bincode.Where(t => t.Length != 16).Any())
            {
                throw new Exception("非二进制码");
            }
            foreach (var item in bincode)
            {
                InstructionSet set = (InstructionSet)Convert.ToInt32(new string(item.Take(4).ToArray()), 2);
                ACommand aCommand = null;
                switch (set)
                {
                    case InstructionSet.BR:
                        {
                            aCommand = new BRCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.ADD:
                        {
                            aCommand = new ADDCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.LD:
                        {
                            aCommand = new LDCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.ST:
                        {
                            aCommand = new STCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.JSR:
                        {
                            aCommand = new JSRCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.AND:
                        {
                            aCommand = new ANDCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.LDR:
                        {
                            aCommand = new LDRCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.STR:
                        {
                            aCommand = new STRCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.RTI:
                        {
                            aCommand = new RTICommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.NOT:
                                            {
                            aCommand = new NOTCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.LDI:
                        {
                            aCommand = new LDICommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.STI:
                        {
                            aCommand = new STICommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.JMP:
                        {
                            aCommand = new JMPCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.RES:
                        {
                            aCommand = new RESCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.LEA:
                        {
                            aCommand = new LEACommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.TRAP:
                        {
                            aCommand = new TRAPCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.ORIG:
                        {
                            aCommand = new ORIGCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.FILL:
                        {
                            aCommand = new FILLCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.BLKW:
                        {
                            aCommand = new BLKWCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.STRINGZ:
                        {
                            aCommand = new STRINGZCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                    case InstructionSet.END:
                        {
                            aCommand = new ENDCommand();
                            aCommand.BinToCommand(item);
                        }
                        break;
                }
                if (aCommand != null)
                {
                    aCommands.Add(aCommand);
                }
            }
            return aCommands.ToArray();
        }
        /// <summary>
        /// load bin 
        /// 二进制
        /// </summary>
        public static ACommand[] LoadBin(List<int> bincode)
        {
            return null;
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
        /// <summary>
        /// 备用
        /// </summary>
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
        /// 陷阱，中断，系统函数调用
        /// </summary>
        TRAP = 15,
        /// <summary>
        /// 告诉汇编器将程序放在内存的哪个位置。
        /// </summary>
        ORIG = 100,
        /// <summary>
        /// 占用一个地址，并往地址所指向的内存单元填充初始值。 
        /// </summary>
        FILL,
        /// <summary>
        /// 占用连续的地址空间。
        /// </summary>
        BLKW,
        /// <summary>
        /// 连续占用地址空间，并对其初始化，内存最后一个单元被置为x0000，类似C语言的/0。
        /// </summary>
        STRINGZ,
        /// <summary>
        /// 源程序结束。 
        /// </summary>
        END
    }
}
