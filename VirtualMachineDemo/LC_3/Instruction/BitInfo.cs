using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
        public List<(int start, int end)> defaultValue = new List<(int start, int end)>();
        public ACommand ACommand { get; private set; }
        private Dictionary<string, (int start, int end)> info = new Dictionary<string, (int start, int end)>(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, (string trueName, string falseName)> conditions = new Dictionary<string, (string trueName, string falseName)>();
        public void AddInfo(string name, int startBit, int endBit)
        {
            info[name] = (15 - startBit, 15 - endBit);
        }
        public void AddDefault(int start, int end)
        {
            defaultValue.Add((15 - start, 15 - end));
        }
        public void AddInfo(string name, int startBit, int endBit, string trueName, string falseName)
        {
            info[name] = (15 - startBit, 15 - endBit);
            conditions[name] = (trueName, falseName);
        }

        public void BinToCommand(string bin)
        {
            int getValue(string name, Dictionary<string, (int start, int end)> set, Dictionary<string, PropertyInfo> PropertyInfos)
            {
                var result = set[name];
                var propertie = PropertyInfos[name];
                var binStr = bin.Skip(result.start).Take(result.end - result.start + 1);
                return Convert.ToInt32(new string(binStr.ToArray()), 2);
            }
            void setValue(int value, string name, Dictionary<string, PropertyInfo> PropertyInfos)
            {
                var propertie = PropertyInfos[name];
                if (propertie.PropertyType == typeof(Registers))
                {
                    propertie.SetValue(this.ACommand, (Registers)value);
                }
                else if (propertie.PropertyType == typeof(bool))
                {
                    propertie.SetValue(this.ACommand, value == 0 ? false : true);
                }
                else
                {
                    propertie.SetValue(this.ACommand, Convert.ChangeType(value, propertie.PropertyType));
                }
            }
            var infos = info.OrderBy(t => t.Value.start).ToArray();
            var Properties = this.ACommand.GetType().GetProperties().Where(t => t.Name != nameof(ACommand.InstructionSet)).ToDictionary(t => t.Name, t => t);
            var index = 0;
            while (index < infos.Length)
            {
                var next = infos[index];
                var result = next.Value;
                if (conditions.TryGetValue(next.Key, out var condition))
                {
                    var conditionValue = getValue(next.Key, info, Properties);
                    var trueValue = getValue(condition.trueName, info, Properties);
                    var falseValue = getValue(condition.falseName, info, Properties);
                    var trueValueIndex = Array.IndexOf(infos, infos.Where(t => t.Key == condition.trueName).First());
                    var falseValueIndex = Array.IndexOf(infos, infos.Where(t => t.Key == condition.falseName).First());
                    var EndIndex = new List<int>() { trueValueIndex, falseValueIndex }.Max(i => i) + 1;
                    index = EndIndex;
                    setValue(conditionValue, next.Key, Properties);
                    if (conditionValue == 1)
                    {
                        setValue(trueValue, condition.trueName, Properties);
                    }
                    else
                    {
                        setValue(falseValue, condition.falseName, Properties);
                    }
                }
                else
                {
                    if (Properties.TryGetValue(next.Key, out var propertie))
                    {
                        var pp = getValue(next.Key, info, Properties);
                        setValue(pp, next.Key, Properties);
                    }
                    index++;
                }
            }
        }
        public string ToBin()
        {
            var bin = new char[16];
            bin = bin.Select(t => '0').ToArray();
            int getValue(string name, Dictionary<string, (int start, int end)> set, Dictionary<string, PropertyInfo> PropertyInfos)
            {
                var Property = PropertyInfos[name];
                var obj = Property.GetValue(this.ACommand, null);
                if (Property.PropertyType == typeof(bool))
                {
                    if ((bool)obj)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                return (int)obj;
            }
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
            void SetIntValue(char[] b, int index, int value)
            {
                b[index] = (char)value;
            }
            var Properties = this.ACommand.GetType().GetProperties().ToDictionary(t => t.Name, t => t);
            var infos = info.OrderBy(t => t.Value.start).ToArray();
            var index = 0;
            while (index < infos.Length)
            {
                var next = infos[index];
                if (conditions.TryGetValue(next.Key, out var condition))
                {
                    var conditionValue = getValue(next.Key, info, Properties);
                    var trueValue = getValue(condition.trueName, info, Properties);
                    var falseValue = getValue(condition.falseName, info, Properties);
                    var trueValueInfo = infos.Where(t => t.Key == condition.trueName).First();
                    var trueValueIndex = Array.IndexOf(infos, trueValueInfo);
                    var falseValueInfo = infos.Where(t => t.Key == condition.falseName).First();
                    var falseValueIndex = Array.IndexOf(infos, falseValueInfo);
                    var EndIndex = new List<int>() { trueValueIndex, falseValueIndex }.Max(i => i) + 1;
                    index = EndIndex;
                    SetValue(bin, conditionValue, next.Value.start, next.Value.end);
                    if (conditionValue == 1)
                    {
                        SetValue(bin, trueValue, trueValueInfo.Value.start, trueValueInfo.Value.end);
                    }
                    else
                    {
                        SetValue(bin, falseValue, falseValueInfo.Value.start, falseValueInfo.Value.end);
                    }
                }
                else
                {
                    if (Properties.TryGetValue(next.Key, out var propertie))
                    {
                        var obj = propertie.GetValue(this.ACommand, null);
                        SetValue(bin, (int)obj, next.Value.start, next.Value.end);
                    }
                }
                index++;
            }
            foreach (var item in defaultValue)
            {
                for (int i = item.start; i <= item.end; i++)
                {
                    bin[i] = (char)1;
                }
            }
            return new string(bin);
        }
    }
}
