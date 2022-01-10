package com.catdll.game.gfx;

import com.catdll.game.gfx.GL.*;
import com.catdll.game.gfx.GL.Shader.*;

import java.util.*;

public class Renderer  
{
    private static ShaderProgram Program;

    private static Shader Vertex;
    
    private static Shader Fragment;

    private static VAO Vao;

    private static VBO Vbo;

    private static ArrayList<Sprite> Sprites;

    public Renderer(String vertexShdPath, String fragmentShdPath)
    {
        Vertex = new Shader(vertexShdPath, ShaderType.VERTEX);
        Fragment = new Shader(fragmentShdPath, ShaderType.FRAGMENT);

        Program = new ShaderProgram();
        Program.attach(Vertex);
        Program.attach(Fragment);

        Vao = new VAO();
        Vbo = new VBO();

        Sprites = new ArrayList<Sprite>();
    }

    public void destroy()
    {
        Program.dispose();
        Vertex.dispose();
        Fragment.dispose();

        Vao.dispose();
        Vbo.dispose();

        // TODO: Performance issues ?
        Sprites.forEach((s) -> s.dispose());
        Sprites.clear();
    }
}
