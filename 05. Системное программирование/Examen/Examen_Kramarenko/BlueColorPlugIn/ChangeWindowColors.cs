using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BlueColorPlugIn
{
    public class ChangeWindowColors
    {
        public static void ChangeColor(Window window)
        {
            ResourceDictionary rDic = window.Resources;

            if (rDic.Contains("FirstGrid")) rDic["FirstGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6e6ecc"));
            if (rDic.Contains("SecondGrid")) rDic["SecondGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF18187a"));
            if (rDic.Contains("FourthGrid")) rDic["FourthGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6e6ecc"));

            if (rDic.Contains("borderBrushMenuBtn")) rDic["borderBrushMenuBtn"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF001a52"));
            if (rDic.Contains("backgroundMenuBtn")) rDic["backgroundMenuBtn"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3a65c2"));

            if (rDic.Contains("GridLinesBrushDataGrid")) rDic["GridLinesBrushDataGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF071130"));
            if (rDic.Contains("RowBackgroundDataGrid")) rDic["RowBackgroundDataGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF94a6e3"));
            if (rDic.Contains("AlternatingRowBackgroundDataGrid")) rDic["AlternatingRowBackgroundDataGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

            if (rDic.Contains("ProgressBarForeground")) rDic["ProgressBarForeground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0041cf"));
            if (rDic.Contains("TextBoxBackground")) rDic["TextBoxBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFced6ed"));

            //if (rDic.Contains("MyDarkColorCmb")) rDic["MyDarkColorCmb"] = (Color)ColorConverter.ConvertFromString("#FF101a87");
            //if (rDic.Contains("MyColorCmb")) rDic["MyColorCmb"] = (Color)ColorConverter.ConvertFromString("#FF1e2dd4");
            //if (rDic.Contains("MyLightColorCmb")) rDic["MyLightColorCmb"] = (Color)ColorConverter.ConvertFromString("#FF404dd6");
            //if (rDic.Contains("MyLightColor2Cmb")) rDic["MyLightColor2Cmb"] = (Color)ColorConverter.ConvertFromString("#FF747ddb");
            //if (rDic.Contains("MyVeryLightColorCmb")) rDic["MyVeryLightColorCmb"] = (Color)ColorConverter.ConvertFromString("#FFa4a9de");
        }
    }
}
