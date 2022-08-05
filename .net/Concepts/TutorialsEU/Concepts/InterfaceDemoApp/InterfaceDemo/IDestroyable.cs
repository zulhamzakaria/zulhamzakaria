using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceDemo
{
    public interface IDestroyable
    {
        public string DestructionSound { get; set; }
        void Destroy();
    }
}
