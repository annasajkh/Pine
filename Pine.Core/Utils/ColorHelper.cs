using Color = Foster.Framework.Color;

namespace Pine.Core.Utils;

public static class ColorHelper
{
    public static Color ColorFromHSV(float h, float s, float v, float a = 1)
    {
        h = h % 1f;

        if (s == 0)
        {
            return new Color(v, v, v, a);
        }

        h *= 360f;

        float sector = h / 60;

        int i = (int)sector;

        float f = sector - i;
        float p = v * (1 - s);
        float q = v * (1 - s * f);
        float t = v * (1 - s * (1 - f));

        switch (i)
        {
            case 0:
                return new Color(v, t, p, a);
            case 1:
                return new Color(q, v, p, a);
            case 2:
                return new Color(p, v, t, a);
            case 3:
                return new Color(p, q, v, a);
            case 4:
                return new Color(t, p, v, a);
            default:
                return new Color(v, p, q, a);
        }
    }

    public static (float h, float s, float v) RGBToHSV(Color color)
    {
        float min, max, delta;

        float r = Math.Clamp(color.R, 0f, 1f);
        float g = Math.Clamp(color.G, 0f, 1f);
        float b = Math.Clamp(color.B, 0f, 1f);

        min = Math.Min(Math.Min(r, g), b);
        max = Math.Max(Math.Max(r, g), b);

        float v = max;

        if (max == 0f)
        {
            return (0f, 0f, 0f);
        }

        delta = max - min;

        float s = delta / max;

        float h;

        if (delta == 0f)
        {
            h = 0f;
        }
        else
        {
            if (max == r)
            {
                h = (g - b) / delta + (g < b ? 6f : 0f);
            }
            else if (max == g)
            {
                h = (b - r) / delta + 2f;
            }
            else
            {
                h = (r - g) / delta + 4f;
            }

            h /= 6f;
        }

        h = (h + 1f) % 1f;

        return (h, s, v);
    }
}
