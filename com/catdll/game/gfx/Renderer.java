package com.catdll.game.gfx;

import com.catdll.game.gfx.GL.*;
import com.catdll.game.gfx.GL.Shader.*;

import java.util.*;

public class Renderer  
{
    private ShaderProgram program;

    private Shader vertex;
    
    private Shader fragment;

    private VAO vao;

    private VBO vbo;

    private ArrayList<Sprite> sprites;

    public Renderer(String vertexShdPath, String fragmentShdPath)
    {
        this.vertex = new Shader(vertexShdPath, ShaderType.VERTEX);
        this.fragment = new Shader(fragmentShdPath, ShaderType.FRAGMENT);

        this.program = new ShaderProgram();
        program.attach(vertex);
        program.attach(fragment);

        this.vao = new VAO();
        this.vbo = new VBO();

        this.sprites = new ArrayList<Sprite>();
    }

    public void destroy()
    {
        program.dispose();
        vertex.dispose();
        fragment.dispose();

        vao.dispose();
        vbo.dispose();

        // TODO: Performance issues ?
        sprites.forEach((s) -> s.dispose());
        sprites.clear();
    }
}
