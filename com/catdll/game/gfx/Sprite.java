package com.catdll.game.gfx;

import com.catdll.game.gfx.GL.*;

import com.catdll.game.*;

public class Sprite extends Disposable
{
    private Texture2D texture;

    private Rectangle sources;

    private int width, height;

    public float posX, posY;

    public Color color;

    public int rotation;

    // TODO: Add vector2 to this parameters
    public float scaleX, scaleY;

    public float originX, originY;

    public Sprite(Texture2D texture, float posX, float posY, Color color, int rotation)
    {
        this.texture = texture;
        this.posX = posX;
        this.posY = posY;
        this.color = color;
        this.rotation = rotation;

        // Original scale
        this.scaleX = 1.0f;
        this.scaleY = 1.0f;

        // Top-left
        this.originX = 0.0f;
        this.originY = 0.0f;

        // Entire texture
        this.sources = new Rectangle(0.0f, 0.0f, this.width, this.height);
    }

    public Sprite(Texture2D texture, float posX, float posY, Color color) { this(texture, posX, posY, color, 0); }

    public Sprite(Texture2D texture, float posX, float posY) { this(texture, posX, posY, Color.WHITE, 0); }

    @Override
    public void dispose()
    {
        if (!isDisposed)
        {
            this.width = 0;
            this.height = 0;

            texture.dispose();
            texture = null;
        }
    }
    
    // Setters

    public void setSources(Rectangle sources) { this.sources = sources; }

    public Rectangle getSources() { return new Rectangle(sources.x, sources.y, sources.width, sources.height); }
}
