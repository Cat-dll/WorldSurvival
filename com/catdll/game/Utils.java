package com.catdll.game;

import java.io.*;

import javax.imageio.ImageIO;

import java.awt.image.BufferedImage;

public class Utils 
{
    public static byte[] readFileData(String path)
    {
        File file = new File(path);

        try
        {
            if (!file.canRead())
                throw new IOException("Cannot read '" + file.getName() + "'!");

            FileInputStream input = new FileInputStream(file);

            byte[] data = input.readAllBytes();

            input.close();

            return data;
        }
        catch (IOException ioe)
        {
            ioe.printStackTrace();
            throw new Error(ioe.getMessage());
        }
    }

    public static BufferedImage loadImage(String path)
    {
        File file = new File(path);
        try
        {
            if (!file.canRead())
                throw new IOException("Cannot read '" + file.getName() + "'!");

                return ImageIO.read(file);
        }
        catch (IOException ioe)
        {
            ioe.printStackTrace();
            throw new Error(ioe.getMessage());
        }
    } 
}
