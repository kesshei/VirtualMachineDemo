using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    /// <summary>
    /// 位结构
    /// 实现结构与属性的映射转换关系
    /// </summary>
    public class BitInfo
    {
        public BitInfo(ACommand aCommand)
        {
            this.ACommand = aCommand;
        }
        public ACommand ACommand { get; private set; }
        private Dictionary<string, (int start, int end)> info = new Dictionary<string, (int start, int end)>(StringComparer.OrdinalIgnoreCase);
        public void AddInfo(string name, int startBit, int endBit)
        {
            info[name] = (15 - startBit, 15 - endBit);
        }
        public void BinToCommand(string bin)
        {
            foreach (var item in this.ACommand.GetType().GetProperties().Where(t => t.Name != nameof(ACommand.InstructionSet)))
            {
                if (info.TryGetValue(item.Name, out var result))
                {
                    var binStr = bin.Skip(result.start).Take(result.end - result.start + 1).Reverse();
                    var pp = Convert.ToInt32(new string(binStr.ToArray()), 2);
                    if (item.PropertyType == typeof(Registers))
                    {
                        item.SetValue(this.ACommand, (Registers)pp);
                    }
                    else if (item.PropertyType == typeof(bool))
                    {
                        item.SetValue(this.ACommand, pp == 0 ? false : true);
                    }
                    else
                    {
                        item.SetValue(this.ACommand, Convert.ChangeType(pp, item.PropertyType));
                    }
                }
            }
        }
        public string ToBin()
        {
            var bin = new char[16];
            bin = bin.Select(t => '0').ToArray();
            void SetValue(char[] b, int value, int start, int end)
            {
                var dd = Convert.ToString(value, 2).PadLeft(end - start + 1, '0');
                var index = 0;
                for (int i = start; i <= end; i++)
                {
                    b[i] = dd[index];
                    index++;
                }
            }
            var Properties = this.ACommand.GetType().GetProperties().ToDictionary(t => t.Name, t => t);
            foreach (var prop in info.OrderBy(t => t.Value.start))
            {
                if (Properties.TryGetValue(prop.Key, out var propertie))
                {
                    var obj = propertie.GetValue(this.ACommand, null);
                    SetValue(bin, (int)obj, prop.Value.start, prop.Value.end);
                }
            }

            //var head_bin = Convert.ToString((int)this.InstructionSet, 2).PadLeft(4, '0');
            //var sr_bin = Convert.ToString((int)this.SR, 2).PadLeft(3, '0');
            //var pc_bin = Convert.ToString((int)this.SR, 2).PadLeft(9, '0');
            //var bin = $"{head_bin}{sr_bin}{pc_bin}";
            return new string(bin);
        }
    }
}
