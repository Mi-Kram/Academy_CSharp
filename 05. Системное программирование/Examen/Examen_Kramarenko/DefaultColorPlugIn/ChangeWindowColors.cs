using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DefaultColorPlugIn
{
    public class ChangeWindowColors
    {
        public static void ChangeColor(Window window)
        {
            ResourceDictionary rDic = window.Resources;

            if (rDic.Contains("FirstGrid"))  rDic["FirstGrid"]  = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD4D4D4"));
            if (rDic.Contains("SecondGrid")) rDic["SecondGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA8A8A8"));
            if (rDic.Contains("FourthGrid")) rDic["FourthGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD4D4D4"));

            if (rDic.Contains("borderBrushMenuBtn")) rDic["borderBrushMenuBtn"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF696969"));
            if (rDic.Contains("backgroundMenuBtn"))  rDic["backgroundMenuBtn"]  = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC4C4C4"));

            if (rDic.Contains("GridLinesBrushDataGrid")) rDic["GridLinesBrushDataGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF696969"));
            if (rDic.Contains("RowBackgroundDataGrid")) rDic["RowBackgroundDataGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD3D3D3"));
            if (rDic.Contains("AlternatingRowBackgroundDataGrid")) rDic["AlternatingRowBackgroundDataGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

            if (rDic.Contains("ProgressBarForeground")) rDic["ProgressBarForeground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF06B025"));
            if (rDic.Contains("TextBoxBackground")) rDic["TextBoxBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFededed"));

            //if (rDic.Contains("MyDarkColorCmb")) rDic["MyDarkColorCmb"] = (Color)ColorConverter.ConvertFromString("#FF4E4E4E");
            //if (rDic.Contains("MyColorCmb")) rDic["MyColorCmb"] = (Color)ColorConverter.ConvertFromString("#FFa8a8a8");
            //if (rDic.Contains("MyLightColorCmb")) rDic["MyLightColorCmb"] = (Color)ColorConverter.ConvertFromString("#FFB3B3B3");
            //if (rDic.Contains("MyLightColor2Cmb")) rDic["MyLightColor2Cmb"] = (Color)ColorConverter.ConvertFromString("#FFc9c9c9");
            //if (rDic.Contains("MyVeryLightColorCmb")) rDic["MyVeryLightColorCmb"] = (Color)ColorConverter.ConvertFromString("#FFe0e0e0");
        }
    }
}
