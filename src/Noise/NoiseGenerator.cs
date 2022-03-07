using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishGame.Noise
{
    public class NoiseGenerator
    {
        private long seed;

        public long Seed
        {
            get => seed;
            set
            {
                if (noise is not null)
                {
                    seed = value;
                    this.InitSeed();
                }
            }
        }

        public float Period { get; set; }

        public float Lacunarity { get; set; }

        public float Persistence { get; set; }

        public int Octaves { get; set; }

        public const int MAX_OCTAVES = 9;

        protected OpenSimplexNoise[] noise;

        public NoiseGenerator(long seed = 1258898706)
        {
            this.noise = new OpenSimplexNoise[MAX_OCTAVES];

            this.Period = 50.0f;
            this.Persistence = 0.48f;
            this.Lacunarity = 3.3f;
            this.Octaves = 5;

            this.InitSeed();
        }

        private void InitSeed()
        {
            for (int i = 0; i < MAX_OCTAVES; i++)
                noise[i] = new OpenSimplexNoise(seed + i * 2);
        }

        public double GetNoiseValue(double x, double y)
        {
            x /= Period;
            y /= Period;

            double amp = 1.0;
            double max = 1.0;
            double sum = noise[0].Evaluate(x, y);

            int i = 0;
            while (i++ < Octaves)
            {
                x *= Lacunarity;
                y *= Lacunarity;
                amp *= Persistence;
                max += amp;
                sum += noise[i].Evaluate(x, y) * amp;
            }

            return sum / max;
        }
    }
}
