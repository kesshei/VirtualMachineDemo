using LC_3.Instruction;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
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
            Memory = new string[UInt16.MaxValue];
            foreach (Registers item in Enum.GetValues(typeof(Registers)))
            {
                Reg.Add(item, 0);
            }
        }
        public string[] Memory = new string[UInt16.MaxValue];
        public Dictionary<Registers, UInt16> Reg = new Dictionary<Registers, UInt16>();
        public Dictionary<int, Action> Traps = new Dictionary<int, Action>();
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
        public UInt16 ReadMem(int address)
        {
            if (address < 0 || address >= Memory.Length)
            {
                throw new ArgumentOutOfRangeException("address out of memory");
            }
            return ushort.Parse(Memory[address]);
        }
        public void WriteMem(int address, UInt16 value)
        {
            if (address < 0 || address >= Memory.Length)
            {
                throw new ArgumentOutOfRangeException("address out of memory");
            }
            Memory[address] = value.ToString();
        }
        /// <summary>
        /// 标志寄存器
        /// </summary>
        public FlagRegister COND { get; set; }
        public void Run()
        {
            while (true)
            {
                var info = Memory[PC];
                var cmd = GetCommand(info);
                switch (cmd.InstructionSet)
                {
                    case InstructionSet.ADD:
                        Add((ADDCommand)cmd);
                        break;
                    case InstructionSet.AND:
                        And((ANDCommand)cmd);
                        break;
                    case InstructionSet.NOT:
                        Not((NOTCommand)cmd);
                        break;
                    case InstructionSet.BR:
                        BR((BRCommand)cmd);
                        break;
                    case InstructionSet.JMP:
                        Jump((JMPCommand)cmd);
                        break;
                    case InstructionSet.JSR:
                        Jump_Subroutine((JSRCommand)cmd);
                        break;
                    case InstructionSet.LD:
                        Load((LDCommand)cmd);
                        break;
                    case InstructionSet.LDI:
                        Load_Indirect((LDICommand)cmd);
                        break;
                    case InstructionSet.LDR:
                        Load_Register((LDRCommand)cmd);
                        break;
                    case InstructionSet.LEA:
                        Load_Effective_address((LEACommand)cmd);
                        break;
                    case InstructionSet.ST:
                        Store((STCommand)cmd);
                        break;
                    case InstructionSet.STI:
                        Store_indirect((STICommand)cmd);
                        break;
                    case InstructionSet.STR:
                        Store_register((STRCommand)cmd);
                        break;
                    case InstructionSet.TRAP:
                        Trap((TRAPCommand)cmd);
                        break;

                    #region 未用
                    case InstructionSet.RTI:
                        break;
                    case InstructionSet.RES:
                        break;
                    case InstructionSet.FILL:
                        break;
                    case InstructionSet.BLKW:
                        break;
                    case InstructionSet.ORIG:
                        break;
                    case InstructionSet.STRINGZ:
                        break;
                    case InstructionSet.END:
                        break;
                    case InstructionSet.Address:
                        break;
                        #endregion
                }
                PC++;
            }
        }
        /// <summary>
        /// load bin 
        /// 二进制
        /// </summary>
        public void LoadBin(string[] bincode)
        {
            var ORIG_Base = Convert.ToUInt16(bincode.First(), 2);
            var ORIG = ORIG_Base;
            foreach (var item in bincode.Skip(1))
            {
                Memory[ORIG] = item;
                ORIG++;
            }
            PC = ORIG_Base;
        }
        /// <summary>
        /// load bin 
        /// 二进制
        /// </summary>
        public static ACommand GetCommand(string item)
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
                        if (aCommand.Check(item))
                        {
                            aCommand.BinToCommand(item);
                        }
                        else
                        {
                            aCommand = new AddressCommand();
                            aCommand.BinToCommand(item);
                        }
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
            return aCommand;
        }
        ///// <summary>
        ///// load bin 
        ///// 二进制
        ///// </summary>
        //public static ACommand[] LoadBin(string[] bincode)
        //{
        //    bincode = bincode.Select(t => new string(t.Take(16).ToArray())).ToArray();
        //    var aCommands = new List<ACommand>();
        //    if (bincode.Where(t => t.Length != 16).Any())
        //    {
        //        throw new Exception("非二进制码");
        //    }
        //    aCommands.Add(new ORIGCommand(bincode.First()));

        //    foreach (var item in bincode.Skip(1))
        //    {
        //        InstructionSet set = (InstructionSet)Convert.ToInt32(new string(item.Take(4).ToArray()), 2);
        //        ACommand aCommand = null;
        //        switch (set)
        //        {
        //            case InstructionSet.BR:
        //                {
        //                    aCommand = new BRCommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.ADD:
        //                {
        //                    aCommand = new ADDCommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.LD:
        //                {
        //                    aCommand = new LDCommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.ST:
        //                {
        //                    aCommand = new STCommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.JSR:
        //                {
        //                    aCommand = new JSRCommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.AND:
        //                {
        //                    aCommand = new ANDCommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.LDR:
        //                {
        //                    aCommand = new LDRCommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.STR:
        //                {
        //                    aCommand = new STRCommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.RTI:
        //                {
        //                    aCommand = new RTICommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.NOT:
        //                {
        //                    aCommand = new NOTCommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.LDI:
        //                {
        //                    aCommand = new LDICommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.STI:
        //                {
        //                    aCommand = new STICommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.JMP:
        //                {
        //                    aCommand = new JMPCommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.RES:
        //                {
        //                    aCommand = new RESCommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.LEA:
        //                {
        //                    aCommand = new LEACommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.TRAP:
        //                {
        //                    aCommand = new TRAPCommand();
        //                    if (aCommand.Check(item))
        //                    {
        //                        aCommand.BinToCommand(item);
        //                    }
        //                    else
        //                    {
        //                        aCommand = new AddressCommand();
        //                        aCommand.BinToCommand(item);
        //                    }
        //                }
        //                break;
        //            case InstructionSet.FILL:
        //                {
        //                    aCommand = new FILLCommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.BLKW:
        //                {
        //                    aCommand = new BLKWCommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.STRINGZ:
        //                {
        //                    aCommand = new STRINGZCommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //            case InstructionSet.END:
        //                {
        //                    aCommand = new ENDCommand();
        //                    aCommand.BinToCommand(item);
        //                }
        //                break;
        //        }
        //        if (aCommand != null)
        //        {
        //            aCommands.Add(aCommand);
        //        }
        //    }
        //    return aCommands.ToArray();
        //}
        public void Update_flags(Registers reg)
        {
            Int16 value = (Int16)Reg[reg];
            if (value == 0)
            {
                COND = FlagRegister.ZRO;
            }
            else if (value < 0)
            {
                // 最高位 1，负数
                COND = FlagRegister.NEG;
            }
            else
            {
                COND = FlagRegister.POS;
            }
        }
        public void Add(ADDCommand command)
        {
            if (command.IsImmediateNumber)
            {
                Reg[command.DR] = (ushort)(Reg[command.SR1] + command.ImmediateNumber);
            }
            else
            {
                Reg[command.DR] = (ushort)(Reg[command.SR1] + Reg[command.SR2]);
            }
            Console.WriteLine($"{command.DR} : {Reg[command.DR]}");
            Update_flags(Registers.R0);
        }
        public void And(ANDCommand command)
        {
            if (command.IsImmediateNumber)
            {
                Reg[command.DR] = (ushort)(Reg[command.SR1] + command.ImmediateNumber);
            }
            else
            {
                Reg[command.DR] = (ushort)(Reg[command.SR1] + Reg[command.SR2]);
            }
            Console.WriteLine($"{command.DR} : {Reg[command.DR]}");
            Update_flags(Registers.R0);
        }
        public void Not(NOTCommand command)
        {
            Reg[command.DR] = (ushort)~Reg[command.SR];
            Update_flags(Registers.R0);
        }
        public void BR(BRCommand command)
        {
            var dic = new Dictionary<FlagRegister, bool>();
            dic[FlagRegister.ZRO] = command.Z;
            dic[FlagRegister.NEG] = command.N;
            dic[FlagRegister.POS] = command.P;
            var values = new List<bool>();
            foreach (var item in dic)
            {
                if (item.Key == COND)
                {
                    values.Add(item.Value);
                }
                else
                {
                    values.Add(!item.Value);
                }
            }
            if (!values.Any(t => !t))
            {
                PC = command.PC;
            }
        }
        public void Jump(JMPCommand command)
        {
            PC = Reg[command.BaseR];
        }
        public void Load_Indirect(LDICommand command)
        {
            var address = ReadMem(command.PC + PC);
            var data = ReadMem(address);
            Reg[command.DR] = data;
            Update_flags(command.DR);
        }
        public void Load_Effective_address(LEACommand command)
        {
            Reg[command.DR] = (ushort)(PC + command.PC);
            Update_flags(command.DR);
        }
        public void Jump_Subroutine(JSRCommand command)
        {
            Reg[Registers.R7] = PC;
            if (command.IsOffset)
            {
                PC = (ushort)(PC + command.PC);
            }
            else
            {
                PC = Reg[command.BaseR];
            }
        }
        public void Load(LDCommand command)
        {
            Reg[command.DR] = ReadMem(command.PC + PC);
            Update_flags(command.DR);
        }
        public void Load_Register(LDRCommand command)
        {
            var address = Reg[command.BaseR] + command.offset6;
            var value = ReadMem(address);
            Reg[command.DR] = value;
            Update_flags(command.DR);
        }
        public void Store(STCommand command)
        {
            var address = command.PC + PC;
            var value = Reg[command.SR];
            WriteMem(address, value);
        }
        public void Store_indirect(STICommand command)
        {
            var address = PC + command.PC;
            var value = Reg[command.SR];
            WriteMem(address, value);
        }
        public void Store_register(STRCommand command)
        {
            var address = Reg[command.BaseR] + command.offset6;
            var value = Reg[command.SR];
            WriteMem(address, value);
        }
        public void Trap(TRAPCommand command)
        {
            Traps.TryGetValue(command.Trapverct, out var action);
            action?.Invoke();
        }
    }
    /// <summary>
    /// 寄存器定义
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
        END,
        Address
    }
}
