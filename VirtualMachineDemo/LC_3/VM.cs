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
            Traps.Add(TrapSet.TRAP_GETC, Trap_GETC);
            Traps.Add(TrapSet.TRAP_OUT, TRAP_OUT);
            Traps.Add(TrapSet.TRAP_PUTS, TRAP_PUTS);
            Traps.Add(TrapSet.TARP_IN, TRAP_IN);
            Traps.Add(TrapSet.TRAP_PUTSP, TRAP_PUTSP);
            Traps.Add(TrapSet.TRAP_HALT, TRAP_HALT);
        }
        public string[] Memory = new string[UInt16.MaxValue];
        public Dictionary<Registers, UInt16> Reg = new Dictionary<Registers, UInt16>();
        public Dictionary<TrapSet, Action> Traps = new Dictionary<TrapSet, Action>();
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
        public bool IsRuning { get; private set; }
        public void Run()
        {
            IsRuning = true;
            while (IsRuning)
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
                        #endregion
                }
                PC++;
            }
            Console.WriteLine("虚拟机停止了运行!");
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
                        aCommand.BinToCommand(item);
                    }
                    break;
            }
            return aCommand;
        }
        public void Trap_GETC()
        {
            Console.WriteLine("Trap_GETC  begin");
            Console.WriteLine("请输入一个字符:");
            Reg[Registers.R0] = (UInt16)Console.Read();
            Console.WriteLine("Trap_GETC  end");
        }
        public void TRAP_OUT()
        {
            Console.WriteLine("TRAP_OUT  begin");
            Console.WriteLine((char)Reg[Registers.R0]);
            Console.WriteLine("TRAP_OUT  end");
        }
        public void TRAP_PUTS()
        {
            Console.WriteLine("TRAP_PUTS  begin");
            var adress = Reg[Registers.R0];
            while (true)
            {
                adress++;
                var c = Memory[adress];
                var cc = (char)Convert.ToUInt16(c, 2);
                if (cc == '\0')
                {
                    Console.WriteLine();
                    break;
                }
                Console.Write(cc);
            }
            Console.WriteLine("TRAP_PUTS  end");
        }
        public void TRAP_IN()
        {
            Console.WriteLine("TRAP_IN  begin");
            Console.WriteLine("请输入一个字符:");
            var c = Console.Read();
            Console.WriteLine(c);
            Reg[Registers.R0] = (UInt16)c;
            Console.WriteLine("TRAP_IN  end");
        }
        public void TRAP_PUTSP()
        {
            Console.WriteLine("TRAP_PUTSP  begin");

            var adress = Reg[Registers.R0];
            while (true)
            {
                adress++;
                var c = Memory[adress];
                var cc = (char)Convert.ToUInt16(c, 2);
                if (cc == '\0')
                {
                    Console.WriteLine();
                    break;
                }
                Console.Write(cc);
            }
            Console.WriteLine("TRAP_PUTSP  end");
        }
        public void TRAP_HALT()
        {
            IsRuning = false;
            Console.WriteLine("Halt");
            Console.WriteLine("准备停止运行!");
        }


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
            Traps.TryGetValue((TrapSet)command.Trapverct, out var action);
            action?.Invoke();
        }
    }
    public enum TrapSet
    {
        /// <summary>
        /// 从键盘输入
        /// </summary>
        TRAP_GETC = 0x20,
        /// <summary>
        /// 输出字符
        /// </summary>
        TRAP_OUT = 0x21,
        /// <summary>
        /// 输出字符串
        /// </summary>
        TRAP_PUTS = 0x22,
        /// <summary>
        /// 打印输入提示，读取单个字符
        /// </summary>
        TARP_IN = 0x23,
        /// <summary>
        /// 输出字符串
        /// </summary>
        TRAP_PUTSP = 0x24,
        /// <summary>
        /// 退出程序
        /// </summary>
        TRAP_HALT = 0x25,
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
        TRAP = 15
    }
}