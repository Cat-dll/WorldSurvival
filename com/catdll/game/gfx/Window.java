package com.catdll.game.gfx;

import com.catdll.game.*;

import org.lwjgl.glfw.*;

import static org.lwjgl.glfw.GLFW.*;
import static org.lwjgl.system.MemoryUtil.*;

import static org.lwjgl.glfw.Callbacks.*;
import java.nio.ByteBuffer;
import java.awt.image.BufferedImage;

public class Window 
{
    // GLFW window
    private static long CurrentWindow;

    private static GLFWImage.Buffer Icon;

    private static String Title = "Game";

    private static int Width, Height = 0;

    private static boolean IsFocused = false;

    private static boolean IsMaximized = false;

    private static boolean IsClosed = false;

    // Stats
    private static int Fps, Frames = 0;

    private static double Frametime, DeltaTime = 0;

    private static double Now, Last, LastFrames = 0;


    private static GLFWErrorCallback ErrorCallback;

    private static GLFWWindowCloseCallback CloseCallback;

    private static GLFWFramebufferSizeCallback FramebufferCallback;

    private static GLFWWindowFocusCallback FocusCallback;

    private static GLFWWindowMaximizeCallback MaximizeCallback;

    private static boolean IsInit;

    public static void init(String title, int width, int height)
    {
        if (!IsInit)
        {
            Title = title;
            Width = width;
            Height = height;

            if (!glfwInit())
            {
                Main.LOG.severe("Failure to create window!" + glfwGetVersionString());
                throw new Error();
            }

            glfwDefaultWindowHints();
            glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GLFW_FALSE);
            glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);
            glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 4);
            glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 0);

            glfwSetErrorCallback(ErrorCallback = new GLFWErrorCallback() {
                @Override
                public void invoke(int code, long desc)
                {
                    Main.LOG.severe("Error " + code + "\n" + GLFWErrorCallback.getDescription(desc));
                    throw new Error();
                }
            });

            CurrentWindow = glfwCreateWindow(Width, Height, Title, 0, 0);
            if (CurrentWindow == NULL)
            {
                Main.LOG.severe("Failure to create window!");
                throw new Error();
            }

            glfwSetWindowCloseCallback(CurrentWindow, CloseCallback = new GLFWWindowCloseCallback() {
                @Override
                public void invoke(long window)
                {
                    IsClosed = true;
                }
            });

            glfwSetWindowFocusCallback(CurrentWindow, FocusCallback = new GLFWWindowFocusCallback() {
                @Override
                public void invoke(long window, boolean focus)
                {
                    IsFocused = focus;
                }
            });
            glfwSetWindowMaximizeCallback(CurrentWindow, MaximizeCallback = new GLFWWindowMaximizeCallback() {
                @Override
                public void invoke(long window, boolean maximize)
                {
                    IsMaximized = maximize;
                }    
            });

            glfwSetFramebufferSizeCallback(CurrentWindow, FramebufferCallback = new GLFWFramebufferSizeCallback() {
                @Override
                public void invoke(long window, int newWidth, int newHeight)
                {
                    Renderer.RenderWidth = newWidth;
                    Renderer.RenderHeight = newHeight;
                }
            });

            glfwMakeContextCurrent(CurrentWindow);
            glfwSwapInterval(1);

            Renderer.init("C:\\Users\\HP\\eclipse-workspace\\MineAndCraft\\MineAndCraft\\com\\catdll\\game\\resources\\vertex.shader", "C:\\Users\\HP\\eclipse-workspace\\MineAndCraft\\MineAndCraft\\com\\catdll\\game\\resources\\fragment.shader");

            IsInit = true;
        }
    }

    public static void loop(Runnable tick, Runnable render, Runnable update)
    {
        while (!IsClosed)
        {
            glfwPollEvents();

            if (Frames - LastFrames == Setting.TICK_PER_SECOND)
            {
                tick.run();
                LastFrames = Frames;
            }

            update.run();
            render.run();

            glfwSwapBuffers(CurrentWindow);

            Frames++;

            Last = Now;
            Now = System.nanoTime();
            Frametime = Now - Last;
            Fps = (int)((1000.0f / Frametime) * 1000.0f);
            DeltaTime = Frametime / 1000.0f;
        }
    }

    public static void destroy()
    {
        try
        {
            /*
            ErrorCallback = null;
            FramebufferCallback = null;
            MaximizeCallback = null;
            CloseCallback = null;
            FocusCallback = null;
            */
            glfwFreeCallbacks(CurrentWindow);
        }
        catch (Exception e)
        {
            Main.LOG.warning("Cannot destroy window correctly!");
            throw new Error();
        }

        if (CurrentWindow != NULL)
        {
            glfwDestroyWindow(CurrentWindow);
            CurrentWindow = NULL;
        }

        glfwTerminate();

        Renderer.destroy();
    }

    // Setters
    public static void setTitle(String title)
    {
        Title = title;
        glfwSetWindowTitle(CurrentWindow, Title);
    }

    public static void setDisplay(int width, int height)
    {
        Width = width;
        Height = height;
        glfwSetWindowSize(CurrentWindow, Width, Height);
    }

    public static void setIcon(BufferedImage image)
    {
        int imageSize = image.getWidth() * image.getHeight() * 4;

        Icon = GLFWImage.malloc(imageSize);
        Icon.width(image.getWidth());
        Icon.height(image.getHeight());

        ByteBuffer buffer = ByteBuffer.allocate(imageSize); // R G B A
        for (int x = 0; x < image.getWidth(); x++)
        {
            for (int y = 0; y < image.getHeight(); y++)
            {
                int color = image.getRGB(x, y);
                buffer.put((byte)(color | 0xFF000000)); 
                buffer.put((byte)(color | 0x00FF0000));
                buffer.put((byte)(color | 0x0000FF00));
                buffer.put((byte)(color | 0x000000FF));
            }
        }

        Icon.get().set(image.getWidth(), image.getHeight(), buffer.flip());
        glfwSetWindowIcon(CurrentWindow, Icon);
    }

    // Getters
    public static boolean isClose() { return IsClosed; }

    public static boolean isMaximize() { return IsMaximized; }

    public static boolean isFocus() { return IsFocused; }

    public static String getTitle() { return new String(Title); }

    public static int getWidth() { return Width; }

    public static int getHeight() { return Height; }

    public static int getFPS() { return Fps; }

    public static double getFrametime() { return Frametime; }

    public static double getDeltatime() { return DeltaTime; }

}
