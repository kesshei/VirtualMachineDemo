namespace LC_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool a = false;
            VM VM = new VM();
            //VM.LoadAsm(File.ReadAllLines(@"C:\Users\Administrator\Desktop\test\test2\test2.asm"));
            VM.LoadBin(File.ReadAllLines(@"C:\Users\Administrator\Desktop\其他\计算机系统(1)\实验\实验4：LC-3简单游戏设计\code\NIM.bin"));
            VM.Run();
            Console.WriteLine("LC_3 !");
        }
    }
}