using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SP_Unsafe_DLL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // статическое подключение DLL к программе
        [DllImport("AlgLibrary", SetLastError = true, EntryPoint = "Test2")]
        static extern unsafe void Test2();

        [DllImport("AlgLibrary", SetLastError = true, EntryPoint = "Test")]
        static extern unsafe int Test(int a, int b);

        [DllImport("AlgLibrary", SetLastError = true, EntryPoint = "array_sum", CallingConvention = CallingConvention.Cdecl)]
        static extern unsafe int array_sum(uint[] data, int length);

        [DllImport("AlgLibrary", SetLastError = true, EntryPoint = "SelectionSort", CallingConvention = CallingConvention.Cdecl)]
        static extern unsafe int SelectionSort(int[] data, int length);


        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        static extern unsafe int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        static extern unsafe IntPtr GetProcAddress(int hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        static extern unsafe bool FreeLibrary(int hModule);

        delegate int SortDel(int[] arr, int size);

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Test2();

            //int res = Test(3, 5);
            //MessageBox.Show(String.Format("Result: {0}", res));

            try
            {
                //int res = array_sum(new uint[] { 1, 2, 3, 4, 7 }, 5);
                //MessageBox.Show(String.Format("Result: {0}", res));

                int[] arr = new int[] { 9, 7, 1, 2, 3 };
                SelectionSort(arr, 5);
                MessageBox.Show($"Result: {string.Join(", ", arr)}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                int hModule = LoadLibrary(@"AlgLibrary.dll");
                if (hModule != 0)
                {
                    IntPtr intPtr = GetProcAddress(hModule, "SelectionSort");

                    SortDel selectionSort = (SortDel)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(SortDel));

                    int[] arr = new int[] { 9, 8, 7, 3, 4, 7, 1, 2, 3 };
                    selectionSort(arr, 9);
                    MessageBox.Show($"Result: {string.Join(", ", arr)}");

                    FreeLibrary(hModule);
                }
                else MessageBox.Show($"DLL not found!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        const int ArraySize = 50000;

        int[] sourceArr = new int[ArraySize];

        Random rand = new Random();

        void ManagedSelectionSort(int[] a, long size)
        {
            long i, j, k;
            int x;

            for (i = 0; i < size; i++)
            {
                k = i; x = a[i];

                for (j = i + 1; j < size; j++)
                    if (a[j] < x)
                    {
                        k = j; x = a[j];
                    }

                a[k] = a[i]; a[i] = x;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ArraySize; i++)
            {
                sourceArr[i] = rand.Next(1000);
            }

            DateTime startTime = DateTime.Now;
            ManagedSelectionSort(sourceArr, ArraySize);
            DateTime stopTime = DateTime.Now;

            TimeSpan workingTime = stopTime - startTime;
            MessageBox.Show($"Managed sort time: {workingTime.TotalMilliseconds}");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ArraySize; i++)
            {
                sourceArr[i] = rand.Next(1000);
            }

            DateTime startTime = DateTime.Now;
            SelectionSort(sourceArr, ArraySize);
            DateTime stopTime = DateTime.Now;

            TimeSpan workingTime = stopTime - startTime;
            MessageBox.Show($"Managed sort time: {workingTime.TotalMilliseconds}");
        }
    }
}
