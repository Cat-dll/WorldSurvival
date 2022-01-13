package com.catdll.game.gfx;

import com.catdll.game.gfx.GL.*;
import com.catdll.game.gfx.GL.Shader.*;

import org.lwjgl.opengl.GL;

import java.util.HashMap;

import static org.lwjgl.opengl.GL40.*;
import java.util.*;

public class Renderer  
{
    public final static int MAX_VERTICES_BATCH_SIZE = 8192 * 32;

    public final static int MAX_INDEX_BATCH_SIZE = 8192 * 4;

    private static Texture2D EmptyTexture;

    public static int RenderWidth;

    public static int RenderHeight;
    
    public static Color ClearColor;


    private static ShaderProgram Program;

    private static Shader Vertex;
    
    private static Shader Fragment;

    private static VAO Vao;

    // TODO: Remplace VBO Object by a (OpenGL) Buffer class
    private static GPUBuffer Vbo;

    private static GPUBuffer Ebo;


    // Sprite Batching
    private static HashMap<Texture2D, List<Sprite>> SpritesData;


    public static void init(String vertexShdPath, String fragmentShdPath)
    {
        // Create context in the thread
        GL.createCapabilities();

        EmptyTexture = new Texture2D(1, 1, new int[] {0xFFFFFFFF});

        RenderWidth = Window.getWidth();
        RenderHeight = Window.getHeight();

        // Shaders
        Vertex = new Shader(vertexShdPath, ShaderType.VERTEX);
        Fragment = new Shader(fragmentShdPath, ShaderType.FRAGMENT);

        Program = new ShaderProgram();
        Program.attach(Vertex);
        Program.attach(Fragment);
        /////////////////////////

        // Vertex Array Object
        Vao = new VAO();
        Vao.bind();
        Vao.attribute(0, 2, GL_FLOAT, true,  32, 0);
        Vao.attribute(1, 4, GL_FLOAT, false, 32, 8);
        Vao.attribute(2, 2, GL_FLOAT, false, 32, 24);
        /////////////////////////////////////////////

        // Vertex Buffer Object
        Vbo = new GPUBuffer(GL_ARRAY_BUFFER);
        Vbo.allocate(MAX_VERTICES_BATCH_SIZE);
        //////////////////////////////////////

        // Index Buffer
        Ebo = new GPUBuffer(GL_ELEMENT_ARRAY_BUFFER);
        Ebo.allocate(MAX_INDEX_BATCH_SIZE);
        //////////////////////////////////

        SpritesData = new HashMap<Texture2D, List<Sprite>>();
    }

    public static void renderSprite(Sprite sprite)
    {
        Texture2D id = sprite.getTexture();
        ArrayList<Sprite> sprites = (ArrayList<Sprite>)SpritesData.get(id);
        if (sprites == null)
            SpritesData.put(id, sprites = new ArrayList<Sprite>());

        sprites.add(sprite);
    }

    // TODO: Manage other sprite parameters
    public static void end()
    {
        if (SpritesData.isEmpty())
            return;

        glActiveTexture(GL_TEXTURE0);

        Vao.bind();
        Vbo.bind();

        Sprite sprite;
        float width, height = 0;
        int color;
        Rectangle texcoord;

        int i = 0;

        for (Texture2D texture : SpritesData.keySet())
        {
            ArrayList<Sprite> sprites = (ArrayList<Sprite>)SpritesData.get(texture);
            int size = sprites.size();

            texture.bind();

            for (int s = 0; s < size; s++)
            {
                sprite = sprites.get(s);
                width = sprite.getWidth();
                height = sprite.getHeight();
                color = sprite.color.toHexa();
                texcoord = sprite.getSources();
                texcoord.width /= width;
                texcoord.height /= height;

                Vbo.data(new float[] {
                    sprite.posX - sprite.originX,           sprite.posY - sprite.originY,                              color,    texcoord.x,                  texcoord.y,
                    sprite.posX + width - sprite.originX,   sprite.posY - sprite.originY,                              color,    texcoord.x + texcoord.width, texcoord.y,
                    sprite.posX + height - sprite.originX,  sprite.posY + (height * sprite.scaleY) - sprite.originY,   color,    texcoord.x + texcoord.width, texcoord.y + texcoord.height,
                    sprite.posX - sprite.originX,           sprite.posY + (height * sprite.scaleY) - sprite.originY,   color,    texcoord.x,                  texcoord.y + texcoord.height,
                });

                Ebo.data(new int[] { i + 0, i + 1, i + 2, i + 2, i + 0, i + 3});
                i += 4; // Four vertices (QUAD)
            }

            Vbo.seek(0);
            Ebo.seek(0);
            glDrawElements(GL_TRIANGLES, Ebo.getUsedMemory() / Integer.BYTES, GL_INT, 0);
        }
    }

    public static void begin()
    {
        glViewport(0, 0, RenderWidth, RenderHeight);

        glClear(GL_COLOR_BUFFER_BIT);
        glClearColor(ClearColor.r, ClearColor.g, ClearColor.b, ClearColor.a);

        Program.use();
    }

    public static void destroy()
    {
        Vertex.dispose();
        Fragment.dispose();
        Program.dispose();

        Vao.dispose();
        Vbo.dispose();

        for (Texture2D k : SpritesData.keySet())
        {
            k.dispose();
            SpritesData.remove(k);
        }

        EmptyTexture.dispose();
    }
}