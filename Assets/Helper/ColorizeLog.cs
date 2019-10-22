using System;
using UnityEngine;

public static class ColorizeLog
{

    public struct Colors
    {
        public static string Orange = ("#FFA500");
        public static string Olive = ("#808000");
        public static string DarkRed = ("#8B0000");
        public static string DarkGreen = ("#006400");
        public static string DarkOrange = ("#FF8C00");
        public static string Gold = ("#FFD700");
    }
    public static void MoeenLog(string log, string Color)
    {
       
        Debug.Log("<color=" + Color + ">" + log + "</color> ");
    }





}




