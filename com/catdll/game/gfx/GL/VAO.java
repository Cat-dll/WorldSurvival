package com.catdll.game.gfx.GL;

import static org.lwjgl.opengl.GL40.*;

import com.catdll.game.*;

public class VAO extends Disposable
{
    private int id;

    public VAO()
    {
        this.id = glGenVertexArrays();
    }

    public void bind()
    {
        glBindVertexArray(this.id);
    }

    public void attribute(int index, int size, int type, int stride, long pointer)
    {
        switch (type)
        {
            case GL_BYTE:
            case GL_UNSIGNED_BYTE:
            case GL_SHORT:
            case GL_UNSIGNED_SHORT:
            case GL_INT:
            case GL_UNSIGNED_INT:
                glVertexAttribIPointer(index, size, type, stride, pointer);
                break;
            case GL_HALF_FLOAT:
            case GL_FLOAT:
            case GL_INT_2_10_10_10_REV:
            case GL_UNSIGNED_INT_2_10_10_10_REV:
            case GL_UNSIGNED_INT_10F_11F_11F_REV:
            case GL_DOUBLE:
                glVertexAttribPointer(index, size, type, false, stride, pointer);
                break;
        }

        glEnableVertexAttribArray(index);        
    }

    public int getID() { return this.id; }

    @Override
    public void dispose()
    {
        if (!isDisposed)
        {
            glDeleteVertexArrays(this.id);
            this.id = GL_NONE;
        }
    }
}
