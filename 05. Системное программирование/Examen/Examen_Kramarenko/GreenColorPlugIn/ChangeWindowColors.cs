using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GreenColorPlugIn
{
    public class ChangeWindowColors
    {
        public static void ChangeColor(Window window)
        {
            ResourceDictionary rDic = window.Resources;

            if (rDic.Contains("FirstGrid")) rDic["FirstGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4fc460"));
            if (rDic.Contains("SecondGrid")) rDic["SecondGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0b801d"));
            if (rDic.Contains("FourthGrid")) rDic["FourthGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4fc460"));

            if (rDic.Contains("borderBrushMenuBtn")) rDic["borderBrushMenuBtn"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF06330d"));
            if (rDic.Contains("backgroundMenuBtn")) rDic["backgroundMenuBtn"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF258a34"));

            if (rDic.Contains("GridLinesBrushDataGrid")) rDic["GridLinesBrushDataGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF022407"));
            if (rDic.Contains("RowBackgroundDataGrid")) rDic["RowBackgroundDataGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF81d48d"));
            if (rDic.Contains("AlternatingRowBackgroundDataGrid")) rDic["AlternatingRowBackgroundDataGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

            if (rDic.Contains("ProgressBarForeground")) rDic["ProgressBarForeground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0d701c"));
            if (rDic.Contains("TextBoxBackground")) rDic["TextBoxBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFa6deaf"));

            //if (rDic.Contains("MyDarkColorCmb")) rDic["MyDarkColorCmb"] = (Color)ColorConverter.ConvertFromString("#FF085c08");
            //if (rDic.Contains("MyColorCmb")) rDic["MyColorCmb"] = (Color)ColorConverter.ConvertFromString("#FF15b015");
            //if (rDic.Contains("MyLightColorCmb")) rDic["MyLightColorCmb"] = (Color)ColorConverter.ConvertFromString("#FF3cc93c");
            //if (rDic.Contains("MyLightColor2Cmb")) rDic["MyLightColor2Cmb"] = (Color)ColorConverter.ConvertFromString("#FF67cf67");
            //if (rDic.Contains("MyVeryLightColorCmb")) rDic["MyVeryLightColorCmb"] = (Color)ColorConverter.ConvertFromString("#FF9fe39f");
        }
    }
}
