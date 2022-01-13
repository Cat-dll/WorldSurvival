package com.catdll.game.gfx;


public class Color
{
    public static final Color WHITE   = new Color(250, 250, 253);
    public static final Color BLACK   = new Color(6, 6, 10);
    public static final Color GRAY    = new Color(86, 108, 134);
    public static final Color RED     = new Color(177, 62, 83);
    public static final Color BLUE    = new Color(59, 93, 201);
    public static final Color YELLOW  = new Color(255, 205, 117);
    public static final Color GREEN   = new Color(56, 183, 100);
    public static final Color ORANGE  = new Color(239, 125, 87);
    public static final Color PURPLE  = new Color(90, 36, 113);
    public static final Color PINK    = new Color(227, 78, 237);
    public static final Color MAGENTA = new Color(112, 39, 93);
    public static final Color CYAN    = new Color(65, 166, 246);
    public static final Color LIME    = new Color(167, 240, 112);

    public float r;

    public float g;
    
    public float b;

    public float a;

    public Color(float r, float g, float b, float a)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }

    public Color(int r, int g, int b, int a)
    {
        this(r / 255f, g / 255f, b / 255f, a / 255f);
    }

    public Color(int r, int g, int b)
    {
        this(r, g, b, 255);
    }

    public int toHexa()
    {
        return (((int)r & 0xFF) << 24) + (((int)g & 0xFF) << 16) + (((int)b & 0xFF) << 8) + ((int)a & 0xFF);
    }
} 