namespace LC_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VM VM = new VM();
            VM.LoadBin(File.ReadAllLines(@"C:\Users\Administrator\Desktop\test\hello\hello.bin"));
            VM.Run();
            Console.WriteLine("LC_3 !");
        }
    }
}