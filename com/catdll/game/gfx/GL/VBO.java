package com.catdll.game.gfx.GL;

import static org.lwjgl.opengl.GL40.*;

import com.catdll.game.*;

public class VBO extends Disposable
{
    private int id;

    private int size;
    
    private int usedMemory;

    public VBO()
    {
        this.size = 0;
        this.usedMemory = 0;
        this.id = glGenBuffers();
    }

    public void allocate(int size)
    {
        if (size != 0)
        {
            this.size = size;
            glBufferData(GL_ARRAY_BUFFER, size, GL_STREAM_DRAW);
        }
    }

    public void reallocate(int newSize)
    {
        if (newSize > this.size) 
        {
            int newVBO = glGenBuffers();
            glBindBuffer(GL_ARRAY_BUFFER, newVBO);
            glBufferData(GL_ARRAY_BUFFER, newSize, GL_STREAM_DRAW);

            glBindBuffer(GL_COPY_READ_BUFFER, this.id);
            glCopyBufferSubData(GL_COPY_READ_BUFFER, GL_ARRAY_BUFFER, 0, 0, this.size);

            this.size = newSize;
            this.id = newVBO;
            bind();
        }
    }

    public void data(float[] data)
    {
        this.usedMemory += data.length * Float.BYTES;
        glBufferSubData(GL_ARRAY_BUFFER, usedMemory, data);
    }

    public void data(double[] data)
    {
        this.usedMemory += data.length * Double.BYTES;
        glBufferSubData(GL_ARRAY_BUFFER, usedMemory, data);
    }

    public void data(int[] data)
    {
        this.usedMemory += data.length * Integer.BYTES;
        glBufferSubData(GL_ARRAY_BUFFER, usedMemory, data);
    }

    public void seek(int offset)
    {
        if (offset > 0 && offset < this.size)
            this.usedMemory = offset;
    }

    public void bind()
    {
        glBindBuffer(GL_ARRAY_BUFFER, this.id);
    }
    
    @Override
    public void dispose()
    {
        if (!isDisposed)
        {
            this.id = GL_NONE;
            this.size = GL_NONE;
            this.usedMemory = GL_NONE;
            glDeleteBuffers(this.id);
        }
    }

    // Getters
    public int getID() { return this.id; }

    public int getUsedMemory() { return this.usedMemory; }

    public int getSize() { return this.size; }
}
