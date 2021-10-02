using System;

public class RandomUtility
{
    public static float NextFloat(Random rand, float min, float max) {
        double val = (rand.NextDouble() * (max - min) + min);
        return (float)val;
    }

    public static float RandNorm(Random rand, float mean, float sigma) {
        double u1 = 1.0 - rand.NextDouble();
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                     Math.Sin(2.0 * Math.PI * u2);
        double randNormal =
                     mean + sigma * randStdNormal;

        return (float)randNormal;
    }
}
