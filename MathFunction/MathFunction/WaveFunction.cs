namespace MathFunction
{
    using System;

    public class WaveFunction
    {
        private static readonly Random Rng = new Random();

        public enum WaveType
        {
            Sine,
            Square,
            Sawtooth,
            Triangle,
            Noise,
            Count
        }

        public double Amplitude = 1.0;
        public double Frequency = 440.0;
        public WaveType Type = WaveType.Sine;

        public void NextType()
        {
            const int count = (int)WaveType.Count;
            int currentType = (int)Type;
            Type = (WaveType)((currentType + 1) % count);
        }

        public double MathFunction(double time)
        {
            switch (Type)
            {
                default /* WaveType.Sine */ : 
                    return Math.Sin(Frequency * time * 2 * Math.PI) * Amplitude;
                case WaveType.Square:
                    return Math.Sin(Frequency * time * 2 * Math.PI) >= 0 ? Amplitude : -Amplitude;
                case WaveType.Sawtooth:
                    return 2 * (time * Frequency - Math.Floor(time * Frequency + 0.5)) * Amplitude;
                case WaveType.Triangle:
                    return Math.Abs(2 * (time * Frequency - Math.Floor(time * Frequency + 0.5))) * Amplitude;
                case WaveType.Noise:
                    return (Rng.NextDouble() - Rng.NextDouble()) * Amplitude;
            }
        }
    }
}
