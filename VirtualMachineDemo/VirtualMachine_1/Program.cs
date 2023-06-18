namespace VirtualMachine_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VM VM = new VM();
            var program = new List<int>()
            {
                (int)InstructionSet.PUSH,5,
                (int)InstructionSet.PUSH,6,
                (int)InstructionSet.ADD,
                (int)InstructionSet.POP,
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
        public void Run(int[] Instructions)
        {
            Start();
            while (Runing)
            {
                int instruction = Instructions[IP];
                Eval((InstructionSet)instruction, Instructions);
                IP++;
            }
            Console.WriteLine("VM 虚拟机 退出运行!");
        }
        private void Eval(InstructionSet instruction, int[] Instructions)
        {
            switch (instruction)
            {
                case InstructionSet.HALT:
                    Runing = false;
                    Console.WriteLine("虚拟机停止");
                    break;
                case InstructionSet.PUSH:
                    StackPointer++;
                    ++IP;
                    Stack[StackPointer] = Instructions[IP];
                    break;
                case InstructionSet.POP:
                    int popValue = Stack[StackPointer];
                    StackPointer--;
                    Console.WriteLine($"poped {popValue}");
                    break;
                case InstructionSet.ADD:
                    //从栈中取出两个操作数，相加，然后，保存在栈中
                    int a = Stack[StackPointer];
                    StackPointer--;
                    int b = Stack[StackPointer];
                    StackPointer--;
                    int sum = a + b;
                    StackPointer++;
                    Stack[StackPointer] = sum;
                    break;
            }
        }
        public void Start()
        {
            Runing = true;
            IP = 0;
            Stack = new int[256];
            StackPointer = -1;
        }
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
        /// 加法
        /// 取出栈中的两个操作数，执行加法操作，然后，放入栈中
        /// </summary>
        ADD,
        /// <summary>
        /// 取栈顶数据
        /// </summary>
        POP,
        /// <summary>
        /// 虚拟机停止运行
        /// </summary>
        HALT
    }
}