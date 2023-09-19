namespace WpfAppSample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AirplaneTelemetry
    {      
        /// <summary>
        /// The state of engine is On/Off
        /// </summary>
        public bool Ignition { get; set; } = false;

        /// <summary>
        /// The airplane speed
        /// </summary>
        public double Speed { get; set; } = 0.0;

        /// <summary>
        /// The airplane atack angle in radians
        /// </summary>
        public double Angle { get; set; } = 0.0;

        /// <summary>
        /// The airplane height in meters
        /// </summary>
        public double Height { get; set; } = 0.0;

    }
}
