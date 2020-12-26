using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.Command
{
    public class CommandResponse
    {
        public string Message { get; set; }

        public bool Success { get; set; } = true;
    }
}
