package com.catdll.game;

import java.util.logging.Logger;

public class Main 
{
    public static Logger LOG = Logger.getGlobal();
    
    public static void main(String[] args) 
    {
        GameState state = new GameState();
        
        state.destroy();
    }
}
