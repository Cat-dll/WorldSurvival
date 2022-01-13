package com.catdll.game.gfx.GL;

import static org.lwjgl.opengl.GL40.*;
import java.util.*;
import com.catdll.game.*;

public class ShaderProgram extends Disposable
{
    private ArrayList<Shader> attachedShaders;

    private int id;
    
    public ShaderProgram()
    {
        this.id = glCreateProgram();
        this.attachedShaders = new ArrayList<Shader>();
    }

    public void attach(Shader shader)
    {
        glAttachShader(this.id, shader.getID());
        this.attachedShaders.add(shader);
    }

    public void detach(Shader shader)
    {
        glDetachShader(this.id, shader.getID());
        this.attachedShaders.remove(shader);
    }

    public void link()
    {
        glLinkProgram(this.id);
        if (glGetProgrami(this.id, GL_LINK_STATUS) == GL_FALSE)
            throw new RuntimeException("Failure to link OpenGL shader program!\n\t" + glGetProgramInfoLog(this.id));
    }

    public void use()
    {
        glUseProgram(this.id);
    }

    @Override
    public void dispose()
    {
        if (!isDisposed)
        {
            glDeleteProgram(this.id);
            this.id = GL_NONE;
        }
    }

    // TODO: Add uniform() definition for vector and matrix
    public void uniform(String uniformName, int value)
    {
        glUniform1i(glGetUniformLocation(this.id, uniformName), value);
    }

    public void uniform(String uniformName, float value)
    {
        glUniform1f(glGetUniformLocation(this.id, uniformName), value);
    }

    public void uniform(String uniformName, double value)
    {
        glUniform1d(glGetUniformLocation(this.id, uniformName), value);
    }

    // Getter
    public int getID() { return this.id; }
}
