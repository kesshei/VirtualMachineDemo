using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_3.Instruction
{
    public class AddressCommand : ACommand
    {
        public AddressCommand() : base(InstructionSet.Address)
        {
            IsAddress = true;
        }
    }
}
