namespace WpfAppSample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Media.Media3D;

    public class AirplaneSimulator
    {

        /// <summary>
        /// Simulation tick time in milliseconds
        /// </summary>
        public int DelaySim { get; set; } = 100;

        private double effectiveAtackAngle = Math.PI / 8;

        /// <summary>
        /// The maximum possible airplane heights
        /// </summary>
        public double MaxHeight { get; set; } = 100.0;

        /// <summary>
        /// Current airplane height interpolation factor
        /// </summary>
        private double HeightLerpFactor => 1 - Telemetry.Height / MaxHeight;

        /// <summary>
        /// Current airplane atack angle interpolation factor
        /// </summary>
        private double AngleLerpFactor => 1 - Telemetry.Angle / effectiveAtackAngle;

        /// <summary>
        /// Current airplane step interpolation factor
        /// </summary>
        private double LerpFactor => (HeightLerpFactor + AngleLerpFactor ) / 2;

        /// <summary>
        /// Current airplane telemetry
        /// </summary>
        public AirplaneTelemetry Telemetry { get; } = new AirplaneTelemetry();

        /// <summary>
        /// Action when some airplane params changed, params: telemetry property name
        /// </summary>
        public Action<string>? OnTelemetryChanged { get; set; }

        /// <summary>
        /// Action when igniton changed, params: new value
        /// </summary>
        public Action<bool>? OnIgnigtion { get; set; }

        /// <summary>
        /// Action when speed changed, params: old value
        /// </summary>
        public Action<double>? OnSpeedChanged { get; set; }

        /// <summary>
        /// Action when angle changed, params: old value
        /// </summary>
        public Action<double>? OnAngleChanged { get; set; }

        /// <summary>
        /// Action when height changed, params: old value
        /// </summary>
        public Action<double>? OnHeightChanged { get; set; }

        /// <summary>
        /// The speed that airplane try to reach
        /// </summary>
        public double TargetSpeed { get; set; }

        /// <summary>
        /// The target atack angle that airplane try to reach
        /// </summary>
        public double TargetAngle { get; set; }

        readonly float epsilon = 0.1f;
        readonly double deltaAmbitSpeed = 0.5f;
        readonly double deltaAmbitAngle = Math.PI / 360;


        /// <summary>
        /// Corresponding airplane simulation task
        /// </summary>
        private Task? simTask;

        public Task StartAsync()
        {
            if (Telemetry.Ignition == true)
            {
                throw new InvalidOperationException();
            }

            Telemetry.Ignition = true;
            simTask = Task.Run(DoSimulation);
            return simTask;
        }

        public Task StopAsync()
        {
            if (Telemetry.Ignition == false || simTask == null)
            {
                throw new InvalidOperationException();
            }

            Telemetry.Ignition = false;
            return simTask;
        }

        /// <summary>
        /// Simulation processing
        /// </summary>
        private void DoSimulation()
        {
            while(Telemetry.Ignition)
            {
                Thread.Sleep(DelaySim);

                DoSimulationStep();
            }
        }

        /// <summary>
        /// Do one simulation step
        /// </summary>
        private void DoSimulationStep()
        {
            var dh = LerpFactor * Telemetry.Speed * Math.Sin(Telemetry.Angle);

            if(dh > 0.0)
            {
                var oldHeight = Telemetry.Height;
                Telemetry.Height += dh;
                OnHeightChanged?.Invoke(oldHeight);
                OnTelemetryChanged?.Invoke(nameof(Telemetry.Height));
            }

            if(TargetSpeed != Telemetry.Speed)
            {
                var oldSpeed = Telemetry.Speed;
                Telemetry.Speed = oldSpeed.Lerp(TargetSpeed, epsilon);

                if (Math.Abs(Telemetry.Speed - TargetSpeed) < deltaAmbitSpeed)
                {
                    Telemetry.Speed = TargetSpeed;
                }
                OnSpeedChanged?.Invoke(oldSpeed);
                OnTelemetryChanged?.Invoke(nameof(Telemetry.Speed));
            }

            if (TargetAngle != Telemetry.Angle)
            {
                var oldAngle = Telemetry.Angle;
                Telemetry.Angle = oldAngle.Lerp(TargetAngle, epsilon);

                if (Math.Abs(Telemetry.Angle - TargetAngle) < deltaAmbitAngle)
                {
                    Telemetry.Angle = TargetAngle;
                }
                OnAngleChanged?.Invoke(oldAngle);
                OnTelemetryChanged?.Invoke(nameof(Telemetry.Angle));
            }

            if(Telemetry.Angle > effectiveAtackAngle / 2)
            {
                TargetSpeed -= Telemetry.Angle * 0.3f;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddSpeed(double val)
        {
            TargetSpeed += val;
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddAngle(double val)
        {
            TargetAngle += val;
        }

    }
}
