using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum CalcOperation
        {
            None = 0,
            Plus = '+',
            Minus = '-',
            Division = '÷',
            Multiply = '×',
        }

        double memory = 0; // значение, которое хранится в памяти/memory
        double result = 0; // ответ
        CalcOperation op = CalcOperation.None; // оператор в примере
        (CalcOperation prevOP, double prevVal) prevAct = (CalcOperation.None, 0); // предыдущий пример
        bool isSolved = false; // последние нажатие было на кнопку =
        bool isByOperator = false; // последние нажатие было на кнопку + - * /
        bool isDisabled = false; // некоторые кнопки отключены

        history history = new history();

        public MainWindow()
        {
            InitializeComponent();
            history.Hide();
        }


        #region NUM Buttons

        private void zeroButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isByOperator)
            {
                resultTextBox.Text = "";
                isByOperator = false;
            }

            if (isDisabled)
            {
                resultTextBox.Text = "0";
                historyTextBox.Text = "";
                result = 0;
                op = CalcOperation.None;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;

                
                foreach (Control item in memoryPanel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op1Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op2Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in numsPanel.Children)
                {
                    item.IsEnabled = true;
                }

                isDisabled = false;
                return;
            }
            if (isSolved)
            {
                historyTextBox.Text = "";
                resultTextBox.Text = "0";
                result = 0;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;
                return;
            }

            if (resultTextBox.Text.Length == 13)
            {
                MessageBox.Show("", "", MessageBoxButton.OK);
                return;
            }

            if (resultTextBox.Text != "0") resultTextBox.Text += '0';
        }
        private void oneButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isByOperator)
            {
                resultTextBox.Text = "";
                isByOperator = false;
            }

            if (isDisabled)
            {
                resultTextBox.Text = "1";
                historyTextBox.Text = "";
                result = 0;
                op = CalcOperation.None;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;

                foreach (Control item in memoryPanel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op1Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op2Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in numsPanel.Children)
                {
                    item.IsEnabled = true;
                }

                isDisabled = false;
                return;
            }
            if (isSolved)
            {
                historyTextBox.Text = "";
                resultTextBox.Text = "1";
                result = 0;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;
                return;
            }

            if (resultTextBox.Text.Length == 13)
            {
                MessageBox.Show("To much digits in the value", "INFO", MessageBoxButton.OK);
                return;
            }

            if (resultTextBox.Text == "0") resultTextBox.Text = "";
            resultTextBox.Text += '1';
        }
        private void twoButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isByOperator)
            {
                resultTextBox.Text = "";
                isByOperator = false;
            }

            if (isDisabled)
            {
                resultTextBox.Text = "2";
                historyTextBox.Text = "";
                result = 0;
                op = CalcOperation.None;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;

                foreach (Control item in memoryPanel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op1Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op2Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in numsPanel.Children)
                {
                    item.IsEnabled = true;
                }

                isDisabled = false;
                return;
            }
            if (isSolved)
            {
                historyTextBox.Text = "";
                resultTextBox.Text = "2";
                result = 0;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;
                return;
            }

            if (resultTextBox.Text.Length == 13)
            {
                MessageBox.Show("To much digits in the value", "INFO", MessageBoxButton.OK);
                return;
            }

            if (resultTextBox.Text == "0") resultTextBox.Text = "";
            resultTextBox.Text += '2';
        }
        private void threeButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isByOperator)
            {
                resultTextBox.Text = "";
                isByOperator = false;
            }

            if (isDisabled)
            {
                resultTextBox.Text = "3";
                historyTextBox.Text = "";
                result = 0;
                op = CalcOperation.None;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;

                foreach (Control item in memoryPanel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op1Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op2Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in numsPanel.Children)
                {
                    item.IsEnabled = true;
                }

                isDisabled = false;
                return;
            }
            if (isSolved)
            {
                historyTextBox.Text = "";
                resultTextBox.Text = "3";
                result = 0;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;
                return;
            }

            if (resultTextBox.Text.Length == 13)
            {
                MessageBox.Show("To much digits in the value", "INFO", MessageBoxButton.OK);
                return;
            }

            if (resultTextBox.Text == "0") resultTextBox.Text = "";
            resultTextBox.Text += '3';
        }
        private void fourButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isByOperator)
            {
                resultTextBox.Text = "";
                isByOperator = false;
            }

            if (isDisabled)
            {
                resultTextBox.Text = "4";
                historyTextBox.Text = "";
                result = 0;
                op = CalcOperation.None;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;

                foreach (Control item in memoryPanel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op1Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op2Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in numsPanel.Children)
                {
                    item.IsEnabled = true;
                }

                isDisabled = false;
                return;
            }
            if (isSolved)
            {
                historyTextBox.Text = "";
                resultTextBox.Text = "4";
                result = 0;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;
                return;
            }

            if (resultTextBox.Text.Length == 13)
            {
                MessageBox.Show("To much digits in the value", "INFO", MessageBoxButton.OK);
                return;
            }

            if (resultTextBox.Text == "0") resultTextBox.Text = "";
            resultTextBox.Text += '4';
        }
        private void fiveButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isByOperator)
            {
                resultTextBox.Text = "";
                isByOperator = false;
            }

            if (isDisabled)
            {
                resultTextBox.Text = "5";
                historyTextBox.Text = "";
                result = 0;
                op = CalcOperation.None;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;

                foreach (Control item in memoryPanel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op1Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op2Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in numsPanel.Children)
                {
                    item.IsEnabled = true;
                }

                isDisabled = false;
                return;
            }
            if (isSolved)
            {
                historyTextBox.Text = "";
                resultTextBox.Text = "5";
                result = 0;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;
                return;
            }

            if (resultTextBox.Text.Length == 13)
            {
                MessageBox.Show("To much digits in the value", "INFO", MessageBoxButton.OK);
                return;
            }

            if (resultTextBox.Text == "0") resultTextBox.Text = "";
            resultTextBox.Text += '5';
        }
        private void sixButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isByOperator)
            {
                resultTextBox.Text = "";
                isByOperator = false;
            }

            if (isDisabled)
            {
                resultTextBox.Text = "6";
                historyTextBox.Text = "";
                result = 0;
                op = CalcOperation.None;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;

                foreach (Control item in memoryPanel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op1Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op2Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in numsPanel.Children)
                {
                    item.IsEnabled = true;
                }

                isDisabled = false;
                return;
            }
            if (isSolved)
            {
                historyTextBox.Text = "";
                resultTextBox.Text = "6";
                result = 0;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;
                return;
            }

            if (resultTextBox.Text.Length == 13)
            {
                MessageBox.Show("To much digits in the value", "INFO", MessageBoxButton.OK);
                return;
            }

            if (resultTextBox.Text == "0") resultTextBox.Text = "";
            resultTextBox.Text += '6';
        }
        private void sevenButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isByOperator)
            {
                resultTextBox.Text = "";
                isByOperator = false;
            }

            if (isDisabled)
            {
                resultTextBox.Text = "7";
                historyTextBox.Text = "";
                result = 0;
                op = CalcOperation.None;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;

                foreach (Control item in memoryPanel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op1Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op2Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in numsPanel.Children)
                {
                    item.IsEnabled = true;
                }

                isDisabled = false;
                return;
            }
            if (isSolved)
            {
                historyTextBox.Text = "";
                resultTextBox.Text = "7";
                result = 0;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;
                return;
            }

            if (resultTextBox.Text.Length == 13)
            {
                MessageBox.Show("To much digits in the value", "INFO", MessageBoxButton.OK);
                return;
            }

            if (resultTextBox.Text == "0") resultTextBox.Text = "";
            resultTextBox.Text += '7';
        }
        private void eightButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isByOperator)
            {
                resultTextBox.Text = "";
                isByOperator = false;
            }

            if (isDisabled)
            {
                resultTextBox.Text = "8";
                historyTextBox.Text = "";
                result = 0;
                op = CalcOperation.None;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;

                foreach (Control item in memoryPanel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op1Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op2Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in numsPanel.Children)
                {
                    item.IsEnabled = true;
                }

                isDisabled = false;
                return;
            }
            if (isSolved)
            {
                historyTextBox.Text = "";
                resultTextBox.Text = "8";
                result = 0;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;
                return;
            }

            if (resultTextBox.Text.Length == 13)
            {
                MessageBox.Show("To much digits in the value", "INFO", MessageBoxButton.OK);
                return;
            }

            if (resultTextBox.Text == "0") resultTextBox.Text = "";
            resultTextBox.Text += '8';
        }
        private void nineButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isByOperator)
            {
                resultTextBox.Text = "";
                isByOperator = false;
            }

            if (isDisabled)
            {
                resultTextBox.Text = "9";
                historyTextBox.Text = "";
                result = 0;
                op = CalcOperation.None;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;

                foreach (Control item in memoryPanel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op1Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op2Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in numsPanel.Children)
                {
                    item.IsEnabled = true;
                }

                isDisabled = false;
                return;
            }
            if (isSolved)
            {
                historyTextBox.Text = "";
                resultTextBox.Text = "9";
                result = 0;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;
                return;
            }

            if (resultTextBox.Text.Length == 13)
            {
                MessageBox.Show("To much digits in the value", "INFO", MessageBoxButton.OK);
                return;
            }

            if (resultTextBox.Text == "0") resultTextBox.Text = "";
            resultTextBox.Text += '9';
        }
        private void comaButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isSolved)
            {
                historyTextBox.Text = "";
                resultTextBox.Text = "0,";
                result = 0;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;
                return;
            }

            if (!resultTextBox.Text.Contains(','))
            {
                if (resultTextBox.Text.Length == 13)
                {
                    MessageBox.Show("To much digits in the value", "INFO", MessageBoxButton.OK);
                    return;
                }

                if (isByOperator)
                {
                    resultTextBox.Text = "0";
                    isByOperator = false;
                }
                resultTextBox.Text += ',';
            }
        }

        #endregion

        #region OP Buttons

        private void BackSpaceButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isByOperator) return;

            if (isDisabled)
            {
                resultTextBox.Text = "0";
                historyTextBox.Text = "";
                result = 0;
                op = CalcOperation.None;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;

                foreach (Control item in memoryPanel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op1Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op2Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in numsPanel.Children)
                {
                    item.IsEnabled = true;
                }

                isDisabled = false;
                return;
            }
            if (isSolved)
            {
                historyTextBox.Text = "";
                return;
            }

            if (resultTextBox.Text != "0")
                resultTextBox.Text = resultTextBox.Text.Remove(resultTextBox.Text.Length - 1);
            if (resultTextBox.Text.Length == 0 || resultTextBox.Text == "-" || resultTextBox.Text == "-0")
                resultTextBox.Text = "0";
        }
        private void ChangeSignButton_MouseUp(object sender, RoutedEventArgs e)
        {
            isByOperator = false;

            if (isSolved)
            {
                historyTextBox.Text = (resultTextBox.Text[0] == '-' ?
                    resultTextBox.Text.Remove(0, 1) : ("-" + resultTextBox.Text)) + " =";
                result = -result;
                resultTextBox.Text = result.ToString();
                return;
            }

            if (resultTextBox.Text != "0")
            {
                if (resultTextBox.Text[0] == '-')
                    resultTextBox.Text = resultTextBox.Text.Remove(0, 1);
                else resultTextBox.Text = "-" + resultTextBox.Text;
            }
        }
        private void CEButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isDisabled)
            {
                resultTextBox.Text = "0";
                historyTextBox.Text = "";
                result = 0;
                op = CalcOperation.None;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;

                foreach (Control item in memoryPanel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op1Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op2Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in numsPanel.Children)
                {
                    item.IsEnabled = true;
                }

                isDisabled = false;
                return;
            }
            if (isSolved)
            {
                historyTextBox.Text = "";
                result = 0;
                op = CalcOperation.None;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;
            }

            isByOperator = false;
            resultTextBox.Text = "0";
        }
        private void CButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isDisabled)
            {
                foreach (Control item in memoryPanel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op1Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op2Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in numsPanel.Children)
                {
                    item.IsEnabled = true;
                }

                isDisabled = false;
            }

            resultTextBox.Text = "0";
            historyTextBox.Text = "";
            result = 0;
            op = CalcOperation.None;
            prevAct = (CalcOperation.None, 0);
            isSolved = false;
            isByOperator = false;
        }
        private void PercentButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isSolved || op == CalcOperation.None) return;

            resultTextBox.Text = (result / 100 * double.Parse(resultTextBox.Text)).ToString();

            isByOperator = false;
        }
        private void one_div_xButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (double.Parse(resultTextBox.Text) == 0) return;
            resultTextBox.Text = (1 / double.Parse(resultTextBox.Text)).ToString();
        }
        private void x_multiply_xButton_MouseUp(object sender, RoutedEventArgs e)
        {
            resultTextBox.Text = (double.Parse(resultTextBox.Text) * double.Parse(resultTextBox.Text)).ToString();
        }
        private void SqrtButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (double.Parse(resultTextBox.Text) < 0)
            {
                foreach (Control item in memoryPanel.Children)
                {
                    item.IsEnabled = false;
                }
                foreach (Button item in op1Panel.Children)
                {
                    if (item.Content != "C" && item.Content != "CE" && item.Content != "<=") item.IsEnabled = false;
                }
                foreach (Button item in op2Panel.Children)
                {
                    if (item.Content != "=") item.IsEnabled = false;
                }
                foreach (Button item in numsPanel.Children)
                {
                    if (item.Content == "+/-" && item.Content == ",") item.IsEnabled = false;
                }

                isDisabled = true;
                MessageBox.Show("У отрицательного числа нет корня", "INFO", MessageBoxButton.OK);
                return;
            }
            resultTextBox.Text = (Math.Sqrt(double.Parse(resultTextBox.Text))).ToString();
        }
        private void divisionButton_MouseUp(object sender, RoutedEventArgs e) // ÷
        {
            op = CalcOperation.Division;
            if (isByOperator)
            {
                historyTextBox.Text = historyTextBox.Text.Remove(historyTextBox.Text.Length - 1) + "÷";
                return;
            }
            if (isSolved)
            {
                historyTextBox.Text = resultTextBox.Text + " ÷";
                isSolved = false;

                resultTextBox.Text = result.ToString();
            }
            else
            {
                prevAct.prevVal = double.Parse(resultTextBox.Text);
                result = Solve(result, prevAct.prevOP != CalcOperation.None ?
                                prevAct.prevOP : CalcOperation.Plus,
                                double.Parse(resultTextBox.Text));

                historyTextBox.Text += (historyTextBox.Text.Length == 0 ? "" : " ") +
                    resultTextBox.Text + " ÷";

                if (prevAct.prevOP != CalcOperation.None) resultTextBox.Text = result.ToString();
            }

            isByOperator = true;
            prevAct.prevOP = op;
        }
        private void multiplyButton_MouseUp(object sender, RoutedEventArgs e) // ×
        {
            op = CalcOperation.Multiply;
            if (isByOperator)
            {
                historyTextBox.Text = historyTextBox.Text.Remove(historyTextBox.Text.Length - 1) + "×";
                return;
            }
            if (isSolved)
            {
                historyTextBox.Text = resultTextBox.Text + " ×";
                isSolved = false;

                resultTextBox.Text = result.ToString();
            }
            else
            {
                prevAct.prevVal = double.Parse(resultTextBox.Text);
                result = Solve(result, prevAct.prevOP != CalcOperation.None ?
                                prevAct.prevOP : CalcOperation.Plus,
                                double.Parse(resultTextBox.Text));

                historyTextBox.Text += (historyTextBox.Text.Length == 0 ? "" : " ") +
                    resultTextBox.Text + " ×";

                if (prevAct.prevOP != CalcOperation.None) resultTextBox.Text = result.ToString();
            }

            isByOperator = true;
            prevAct.prevOP = op;
        }
        private void minusButton_MouseUp(object sender, RoutedEventArgs e)
        {
            op = CalcOperation.Minus;
            if (isByOperator)
            {
                historyTextBox.Text = historyTextBox.Text.Remove(historyTextBox.Text.Length - 1) + "-";
                return;
            }
            if (isSolved)
            {
                historyTextBox.Text = resultTextBox.Text + " -";
                isSolved = false;

                resultTextBox.Text = result.ToString();
            }
            else
            {
                prevAct.prevVal = double.Parse(resultTextBox.Text);
                result = Solve(result, prevAct.prevOP != CalcOperation.None ?
                                prevAct.prevOP : CalcOperation.Plus,
                                double.Parse(resultTextBox.Text));

                historyTextBox.Text += (historyTextBox.Text.Length == 0 ? "" : " ") +
                    resultTextBox.Text + " -";

                if (prevAct.prevOP != CalcOperation.None) resultTextBox.Text = result.ToString();
            }

            isByOperator = true;
            prevAct.prevOP = op;
        }
        private void plusButton_MouseUp(object sender, RoutedEventArgs e)
        {
            op = CalcOperation.Plus;
            if (isByOperator)
            {
                historyTextBox.Text = historyTextBox.Text.Remove(historyTextBox.Text.Length - 1) + "+";
                return;
            }
            if (isSolved)
            {
                historyTextBox.Text = resultTextBox.Text + " +";
                isSolved = false;

                resultTextBox.Text = result.ToString();
            }
            else
            {
                prevAct.prevVal = double.Parse(resultTextBox.Text);
                result = Solve(result, prevAct.prevOP != CalcOperation.None ?
                                prevAct.prevOP : CalcOperation.Plus,
                                double.Parse(resultTextBox.Text));

                historyTextBox.Text += (historyTextBox.Text.Length == 0 ? "" : " ") +
                    resultTextBox.Text + " +";

                if (prevAct.prevOP != CalcOperation.None) resultTextBox.Text = result.ToString();
            }

            isByOperator = true;
            prevAct.prevOP = op;
        }
        private void equalButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isDisabled)
            {
                resultTextBox.Text = "0";
                historyTextBox.Text = "";
                result = 0;
                op = CalcOperation.None;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;

                foreach (Control item in memoryPanel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op1Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in op2Panel.Children)
                {
                    item.IsEnabled = true;
                }
                foreach (Control item in numsPanel.Children)
                {
                    item.IsEnabled = true;
                }

                isDisabled = false;
                return;
            }

            isByOperator = false;
            if (op == CalcOperation.None && prevAct.prevOP != CalcOperation.None)
            {
                historyTextBox.Text = $"{resultTextBox.Text} {(char)prevAct.prevOP} {prevAct.prevVal} =";
                result = Solve(result, prevAct.prevOP, prevAct.prevVal);
            }
            else if (op != CalcOperation.None)
            {
                historyTextBox.Text += $" {resultTextBox.Text} =";
                result = Solve(result, op, double.Parse(resultTextBox.Text));

                prevAct.prevVal = double.Parse(resultTextBox.Text);
                prevAct.prevOP = op;
                op = CalcOperation.None;
            }
            else
            {
                historyTextBox.Text += $"{resultTextBox.Text} =";
                result = double.Parse(resultTextBox.Text);
            }


            isSolved = true;
            resultTextBox.Text = result.ToString();
            history.listBox.Items.Add(historyTextBox.Text + " " + resultTextBox.Text);
        }

        #endregion

        #region MEM Buttons

        private void MCButton_MouseUp(object sender, RoutedEventArgs e)
        {
            memory = 0;
        }
        private void MRButton_MouseUp(object sender, RoutedEventArgs e)
        {
            if (isSolved)
            {
                historyTextBox.Text = "";
                result = 0;
                op = CalcOperation.None;
                prevAct = (CalcOperation.None, 0);
                isSolved = false;
            }

            isByOperator = false;
            resultTextBox.Text = memory.ToString();
        }
        private void MPlusButton_MouseUp(object sender, RoutedEventArgs e)
        {
            memory += double.Parse(resultTextBox.Text);
        }
        private void MMinusButton_MouseUp(object sender, RoutedEventArgs e)
        {
            memory -= double.Parse(resultTextBox.Text);
        }

        #endregion

        private double Solve(double val1, CalcOperation op, double val2)
        {
            try
            {
                switch (op)
                {
                    case CalcOperation.Plus: return val1 + val2;
                    case CalcOperation.Minus: return val1 - val2;
                    case CalcOperation.Division:
                        if (val2 == 0) throw new Exception("Cannot divide by zero");
                        return val1 / val2;
                    case CalcOperation.Multiply: return val1 * val2;
                    default:
                        {
                            MessageBox.Show("Solve() принял CalcOperation.None", "Programmer exception", MessageBoxButton.OK);
                            resultTextBox.Text = "0";
                            historyTextBox.Text = "";
                            result = 0;
                            this.op = CalcOperation.None;
                            return 0;
                        }
                }
            }
            catch (Exception ex)
            {
                foreach (Control item in memoryPanel.Children)
                {
                    item.IsEnabled = false;
                }
                foreach (Button item in op1Panel.Children)
                {
                    if (item.Content != "C" && item.Content != "CE" && item.Content != "<=") item.IsEnabled = false;
                }
                foreach (Button item in op2Panel.Children)
                {
                    if (item.Content != "=") item.IsEnabled = false;
                }
                foreach (Button item in numsPanel.Children)
                {
                    if (item.Content == "+/-" && item.Content == ",") item.IsEnabled = false;
                }

                isDisabled = true;
                MessageBox.Show(ex.Message, "INFO", MessageBoxButton.OK);

                return 0;
            }
        }

        private void historyButton_Click(object sender, RoutedEventArgs e)
        {
            history.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            history.CL();
        }
    }
}
