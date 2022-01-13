package com.catdll.game.gfx.GL;

import static org.lwjgl.opengl.GL40.*;

import com.catdll.game.*;

public class GPUBuffer extends Disposable
{
    private int id;

    private int size;
    
    private int usedMemory;

    private int type;

    public GPUBuffer(int type)
    {
        this.type = type;
        this.size = 0;
        this.usedMemory = 0;
        this.id = glGenBuffers();
    }

    public void allocate(int size)
    {
        if (size != 0)
        {
            this.size = size;
            glBufferData(type, size, GL_STREAM_DRAW);
        }
    }

    public void reallocate(int newSize)
    {
        if (newSize > this.size) 
        {
            int newVBO = glGenBuffers();
            glBindBuffer(type, newVBO);
            glBufferData(type, newSize, GL_STREAM_DRAW);

            glBindBuffer(GL_COPY_READ_BUFFER, this.id);
            glCopyBufferSubData(GL_COPY_READ_BUFFER, type, 0, 0, this.size);

            this.size = newSize;
            this.id = newVBO;
            bind();
        }
    }

    public void data(float[] data)
    {
        this.usedMemory += data.length * Float.BYTES;
        glBufferSubData(type, usedMemory, data);
    }

    public void data(double[] data)
    {
        this.usedMemory += data.length * Double.BYTES;
        glBufferSubData(type, usedMemory, data);
    }

    public void data(int[] data)
    {
        this.usedMemory += data.length * Integer.BYTES;
        glBufferSubData(type, usedMemory, data);
    }

    public void seek(int pos)
    {
        if (pos >= 0 && pos < this.size)
            this.usedMemory = pos;
    }

    public void bind()
    {
        glBindBuffer(type, this.id);
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

    public int getType() { return this.type; }
}
