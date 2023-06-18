namespace VirtualMachine_2
{
    /// <summary>
    /// 虚拟机2，实现多指令
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            VM VM = new VM();
            bool mark = true;//可以修改if条件
            var result = 0;

            if (!mark)
            {
                result = ((((10 + 4) - 2) * 6) / 5);
            }
            else
            {
                result = (((4 - 2) * 6) / 5);
            }
            Console.WriteLine($"所要达到的结果:{result}");
            var program = new List<int>()
            {
                (int)InstructionSet.SET,(int)Register.F,mark ? 0:1,
                (int)InstructionSet.IF,(int)Register.F,0,14,
                (int)InstructionSet.PUSH,10,
                (int)InstructionSet.PUSH,4,
                (int)InstructionSet.ADD,
                (int)InstructionSet.JMP,16,
                (int)InstructionSet.PUSH,4,
                (int)InstructionSet.PUSH,2,
                (int)InstructionSet.SUB,
                (int)InstructionSet.PUSH,6,
                (int)InstructionSet.MUL,
                (int)InstructionSet.PUSH,5,
                (int)InstructionSet.DIV,
                (int)InstructionSet.LDR,(int)Register.A,
                (int)InstructionSet.LOGR,(int)Register.A,
                (int)InstructionSet.SET,(int)Register.C,9,
                (int)InstructionSet.LOGR,(int)Register.C,
                (int)InstructionSet.MOV,(int)Register.B,(int)Register.C,
                (int)InstructionSet.STR,(int)Register.B,
                (int)InstructionSet.LDR,(int)Register.D,
                (int)InstructionSet.HALT
            };
            VM.Run(program.ToArray());
            Console.ReadLine();
        }
    }
    public class VM
    {
        public bool Runing { get; private set; }
        /// <summary>
        /// 指令指针寄存器
        /// </summary>
        public int IP { get; private set; } = 0;
        /// <summary>
        /// 栈指针寄存器
        /// </summary>
        public int StackPointer { get; private set; } = -1;
        /// <summary>
        /// 栈
        /// </summary>
        public int[] Stack { get; private set; } = new int[256];
        /// <summary>
        /// 寄存器
        /// </summary>
        public int[] Registers = new int[Enum.GetNames(typeof(Register)).Length];
        public bool IsJump { get; set; }
        public void Run(int[] Instructions)
        {
            Start();
            while (Runing)
            {
                int instruction = Instructions[IP];
                Eval((InstructionSet)instruction, Instructions);
                if (!IsJump)
                {
                    IP++;
                }
                else
                {
                    IsJump = false;
                }
            }
            PrintStack();
            PrintRegisters();
            Console.WriteLine("VM 虚拟机 退出运行!");
        }
        private void Eval(InstructionSet instruction, int[] Instructions)
        {
            switch (instruction)
            {
                case InstructionSet.HALT:
                    {
                        Runing = false;
                        Console.WriteLine("虚拟机停止");
                    }
                    break;
                case InstructionSet.PUSH:
                    {
                        StackPointer++;
                        ++IP;
                        Stack[StackPointer] = Instructions[IP];
                    }
                    break;
                case InstructionSet.POP:
                    {
                        int popValue = Stack[StackPointer];
                        StackPointer--;
                        Console.WriteLine($"poped {popValue}");
                    }
                    break;
                case InstructionSet.ADD:
                    {
                        //从栈中取出两个操作数，相加，然后，保存在栈中
                        int a = Stack[StackPointer];
                        StackPointer--;
                        int b = Stack[StackPointer];
                        StackPointer--;
                        int sum = a + b;
                        StackPointer++;
                        Stack[StackPointer] = sum;
                    }
                    break;
                case InstructionSet.SUB:
                    {
                        //从栈中取出两个操作数，相减，然后，保存在栈中
                        int a = Stack[StackPointer];
                        StackPointer--;
                        int b = Stack[StackPointer];
                        StackPointer--;
                        int sum = b - a;
                        StackPointer++;
                        Stack[StackPointer] = sum;
                    }
                    break;
                case InstructionSet.MUL:
                    {
                        //从栈中取出两个操作数，相乘，然后，保存在栈中
                        int a = Stack[StackPointer];
                        StackPointer--;
                        int b = Stack[StackPointer];
                        StackPointer--;
                        int sum = a * b;
                        StackPointer++;
                        Stack[StackPointer] = sum;
                    }
                    break;
                case InstructionSet.DIV:
                    {
                        //从栈中取出两个操作数，相除，然后，保存在栈中
                        int a = Stack[StackPointer];
                        StackPointer--;
                        int b = Stack[StackPointer];
                        StackPointer--;
                        if (a == 0)
                        {
                            Console.WriteLine("除数不能为0");
                            Runing = false;
                        }
                        else
                        {
                            int sum = b / a;
                            StackPointer++;
                            Stack[StackPointer] = sum;
                        }
                    }
                    break;
                case InstructionSet.MOV:
                    {
                        IP++;
                        //将一个寄存器的值，放入到另外一个寄存器里
                        //目标寄存器
                        int dr = Instructions[IP];

                        IP++;
                        //源寄存器
                        int sr = Instructions[IP];

                        //目标寄存器的值为源寄存器的值
                        Registers[dr] = Registers[sr];
                    }
                    break;
                case InstructionSet.STR:
                    {
                        IP++;
                        //将指定寄存器中的参数，放入栈中
                        int r = Instructions[IP];
                        StackPointer++;
                        Stack[StackPointer] = Registers[r];
                    }
                    break;
                case InstructionSet.LDR:
                    {
                        int value = Stack[StackPointer];
                        IP++;
                        int r = Instructions[IP];
                        Registers[r] = value;
                    }
                    break;
                case InstructionSet.IF:
                    {
                        //如果寄存器的值和后面的数值相等，则跳转
                        IP++;
                        int r = Instructions[IP];
                        IP++;
                        if (Registers[r] == Instructions[IP])
                        {
                            IP++;
                            IP = Instructions[IP];
                            IsJump = true;
                            Console.WriteLine($"jump if :{IP}");
                        }
                        else
                        {
                            IP++;
                        }
                    }
                    break;
                case InstructionSet.SET:
                    {
                        IP++;
                        int r = Instructions[IP];
                        IP++;
                        int value = Instructions[IP];
                        Registers[r] = value;
                    }
                    break;
                case InstructionSet.LOGR:
                    {
                        IP++;
                        int r = Instructions[IP];
                        int value = Registers[r];
                        Console.WriteLine($"log register_{(Register)r} {value}");
                    }
                    break;
                case InstructionSet.JMP:
                    {
                        IP++;
                        IP = Instructions[IP];
                        IsJump = true;
                        Console.WriteLine($"jump if :{IP}");
                    }
                    break;
            }
        }
        public void Start()
        {
            Runing = true;
            IP = 0;
            Stack = new int[256];
            StackPointer = -1;
            Registers = new int[Enum.GetNames(typeof(Register)).Length];
        }
        public void PrintStack()
        {
            Console.WriteLine($"{Environment.NewLine}=========begin print stack:=========");
            for (int i = 0; i <= StackPointer; i++)
            {
                Console.WriteLine($"{Stack[i]}");
                if ((i + 1) % 4 == 0)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine($"=========begin stack done:=========");

        }
        public void PrintRegisters()
        {
            Console.WriteLine($"{Environment.NewLine}=========begin print registers:=========");
            for (int i = 0; i < Registers.Length; i++)
            {
                Console.WriteLine($"{(Register)i}:{Registers[i]}");
            }
            Console.WriteLine($"=========begin registers done:=========");
        }
    }
    public enum Register
    {
        A,
        B,
        C,
        D,
        E,
        F,//A-F通用寄存器
    }
    /// <summary>
    /// 指令集
    /// </summary>
    public enum InstructionSet
    {
        /// <summary>
        /// PUSH 5; 
        /// 将数据放入栈中
        /// </summary>
        PUSH,
        /// <summary>
        /// 取栈顶数据
        /// </summary>
        POP,
        /// <summary>
        /// 给寄存器赋值
        /// SET reg,3
        /// </summary>
        SET,
        /// <summary>
        /// MOV reg1,reg2
        /// 将寄存器 reg2中的数据移动到 reg1中
        /// </summary>
        MOV,
        /// <summary>
        /// 加法
        /// 取出栈中的两个操作数，执行加法操作，然后，放入栈中
        /// </summary>
        ADD,
        /// <summary>
        /// 减法
        /// 取出栈中的两个操作数，执行减法操作，然后，放入栈中
        /// </summary>
        SUB,
        /// <summary>
        /// 除法
        /// 取出栈中的两个操作数，执行除法操作，然后，放入栈中
        /// </summary>
        DIV,
        /// <summary>
        /// 乘法
        /// 取出栈中的两个操作数，执行乘法操作，然后，放入栈中
        /// </summary>
        MUL,
        /// <summary>
        /// 存储指令，将寄存器的数据放入栈中
        /// </summary>
        STR,
        /// <summary>
        /// 将栈顶数据放入寄存器中
        /// </summary>
        LDR,
        /// <summary>
        /// IF reg, value,ip;
        /// 如果reg中的值等于value，就把IP指针指向ip地址。
        /// </summary>
        IF,
        /// <summary>
        /// 打印寄存器中的数据
        /// </summary>
        LOGR,
        /// <summary>
        /// 无条件跳转
        /// </summary>
        JMP,
        /// <summary>
        /// 虚拟机停止运行
        /// </summary>
        HALT
    }
}