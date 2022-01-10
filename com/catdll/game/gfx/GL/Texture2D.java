package com.catdll.game.gfx.GL;

import static org.lwjgl.opengl.GL40.*;

import com.catdll.game.*;

public class Texture2D extends Disposable
{
    public int id;
    
    public int width, height;

    public Texture2D(int width, int height, int[] data)
    {
        this.id = glGenTextures();
        this.width = width;
        this.height = height;

        glBindTexture(GL_TEXTURE_2D, id);
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);

        glTexImage2D(GL_TEXTURE_2D, 1, GL_RGBA32I, width, height, 0, GL_RGBA, GL_UNSIGNED_INT, data);
    }

    public int getID() { return this.id; }

    @Override
    public void dispose()
    {
        if (!isDisposed)
        {
            glDeleteTextures(this.id);
            this.id = GL_NONE;
            this.width = 0;
            this.height = 0;
        }
    }
}
