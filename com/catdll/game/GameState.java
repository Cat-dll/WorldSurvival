package com.catdll.game;

import com.catdll.game.gfx.*;

public class GameState implements IState 
{
    public GameState()
    {
        System.out.println("Init window");
        Window.init(Setting.GAME_TITLE, Setting.GAME_WIDTH, Setting.GAME_WIDTH);
    }

    public void render()
    {

    }

    public void update()
    {

    }

    public void destroy()
    {
        Window.destroy();
    }
    
}
