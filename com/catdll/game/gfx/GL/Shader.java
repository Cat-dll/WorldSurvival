package com.catdll.game.gfx.GL;

import static org.lwjgl.opengl.GL40.*;

import com.catdll.game.Utils;
import com.catdll.game.*;

public class Shader extends Disposable
{
    public enum ShaderType
    {
        UNKNOW,
        VERTEX,
        FRAGMENT
    }

    private int id;

    private ShaderType type;

    public Shader(String path, ShaderType type)
    {
        this.type = type;

        this.id = glCreateShader(getGLShaderType(this.type));
        glShaderSource(this.id, new String(Utils.readFileData(path)));

        glCompileShader(this.id);
        if (glGetShaderi(this.id, GL_COMPILE_STATUS) == GL_FALSE)
            throw new RuntimeException("Failure to compile OpenGL shader!");
    }

    @Override
    public void dispose()
    {
        if (!isDisposed)
        {
            glDeleteShader(this.id);

            this.id = GL_NONE;
            this.type = ShaderType.UNKNOW;
        }
    }

    public int getID() { return this.id; }

    private int getGLShaderType(ShaderType type)
    {
        return switch (type)
        {
            case VERTEX -> GL_VERTEX_SHADER;
            case FRAGMENT -> GL_FRAGMENT_SHADER;
            default -> throw new IllegalArgumentException("Shader type is invalid!");
        };
    }

}
