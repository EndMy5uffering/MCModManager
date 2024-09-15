using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftModManager
{
    class Mod
    {
        public string ModName { get; set; }
        public bool IsEnabled { get; set; } = true; 

        public string ModPath { get; set; }

    }
}
