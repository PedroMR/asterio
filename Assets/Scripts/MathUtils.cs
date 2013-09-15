using System;

public class MathUtils
{
	public static float max(float a, float b) { return a > b ? a : b; }
	public static float min(float a, float b) { return a < b ? a : b; }
	public static float clamp(float val, float min, float max) { return val < min ? min : (val > max ? max : val); }
	public static float clamp01(float val) { return clamp(val, 0, 1.0f); }
}

