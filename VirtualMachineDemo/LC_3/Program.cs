namespace LC_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool a = false;
 
            //VM.LoadAsm(File.ReadAllLines(@"C:\Users\Administrator\Desktop\test\test2\test2.asm"));
            VM.LoadBin(File.ReadAllLines(@"C:\Users\Administrator\Desktop\test\新建文件夹\123.bin"));
            Console.WriteLine("LC_3 !");
        }
    }
}