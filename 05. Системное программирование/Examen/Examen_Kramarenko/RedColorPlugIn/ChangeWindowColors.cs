using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace RedColorPlugIn
{
    public class ChangeWindowColors
    {
        public static void ChangeColor(Window window)
        {
            ResourceDictionary rDic = window.Resources;

            if (rDic.Contains("FirstGrid")) rDic["FirstGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFc75f5f"));
            if (rDic.Contains("SecondGrid")) rDic["SecondGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF802b2b"));
            if (rDic.Contains("FourthGrid")) rDic["FourthGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFc75f5f"));

            if (rDic.Contains("borderBrushMenuBtn")) rDic["borderBrushMenuBtn"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF520404"));
            if (rDic.Contains("backgroundMenuBtn")) rDic["backgroundMenuBtn"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFc24646"));

            if (rDic.Contains("GridLinesBrushDataGrid")) rDic["GridLinesBrushDataGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4f0606"));
            if (rDic.Contains("RowBackgroundDataGrid")) rDic["RowBackgroundDataGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFd99191"));
            if (rDic.Contains("AlternatingRowBackgroundDataGrid")) rDic["AlternatingRowBackgroundDataGrid"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

            if (rDic.Contains("ProgressBarForeground")) rDic["ProgressBarForeground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFa30b0b"));
            if (rDic.Contains("TextBoxBackground")) rDic["TextBoxBackground"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFe8cfcf"));

            //if (rDic.Contains("MyDarkColorCmb")) rDic["MyDarkColorCmb"] = (Color)ColorConverter.ConvertFromString("#FF7a0b0b");
            //if (rDic.Contains("MyColorCmb")) rDic["MyColorCmb"] = (Color)ColorConverter.ConvertFromString("#FFba0606");
            //if (rDic.Contains("MyLightColorCmb")) rDic["MyLightColorCmb"] = (Color)ColorConverter.ConvertFromString("#FFe32424");
            //if (rDic.Contains("MyLightColor2Cmb")) rDic["MyLightColor2Cmb"] = (Color)ColorConverter.ConvertFromString("#FFe36262");
            //if (rDic.Contains("MyVeryLightColorCmb")) rDic["MyVeryLightColorCmb"] = (Color)ColorConverter.ConvertFromString("#FFf0a1a1");
        }
    }
}
