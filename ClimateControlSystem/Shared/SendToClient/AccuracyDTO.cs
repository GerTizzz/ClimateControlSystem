using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateControlSystem.Shared.SendToClient
{
    public sealed class AccuracyDTO
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }
    }
}
