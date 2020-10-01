using System;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
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

namespace Tema1
{
    public partial class MainWindow : Window
    {
        double nr1 = 0;
        double nr2 = int.MinValue;
        double numarMemo = 0;
        int checkBoxDigitGrouping = 0;
        string operatii = "";
        string sirOperatii = "";
        Queue<string> operatori = new Queue<string>();
        Queue<double> numere = new Queue<double>();
        bool punct = false;
        int i = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetSettings();
            if (checkBoxDigitGrouping == 1)
                DGR.IsChecked = true;
            else if (checkBoxDigitGrouping == 2)
                DGE.IsChecked = true;
        }

        private double Update(double nr1, double nr2, string operatii)
        {
            double aux= 0;
            switch(operatii)
            {
                case "+":
                    aux = nr1 + nr2;
                    break;
                case "-":
                    aux = nr1 - nr2;
                    break;
                case "*":
                    aux = nr1 * nr2;
                    break;
                case "/":
                    if(nr2!=0||nr2!=0.0)
                        aux = nr1 / nr2;
                    else
                    {
                        txtDisplay.Text = "Can't divide by 0!";
                       
                        nr2 = 0;
                        aux = int.MinValue;
                    }
                    break;
            }
            return aux;
        }
        private void SpecificNumberShowing(double nr)
        {
            switch (checkBoxDigitGrouping)
            {
                case 0:
                    txtDisplay.Text = nr.ToString();
                    break;
                case 1:
                    txtDisplay.Text = nr.ToString("N1",
                  CultureInfo.CreateSpecificCulture("ro-RO"));
                    break;
                case 2:
                    txtDisplay.Text = nr.ToString("N1",
                  CultureInfo.InvariantCulture);
                    break;
            }
        }
        private void Numar(int operand)
        {
            if (punct == false)
            {
                if (operatii == "")
                {
                    nr1 = (nr1 * 10) + operand;
                    SpecificNumberShowing(nr1);
                }
                else
                {
                    nr2 = (nr2 * 10) + operand;
                    SpecificNumberShowing(nr2);

                }
            }
            else
            {
                i--;
                if (operatii == "")
                {
                    nr1 = (operand * Math.Pow(10, i)) + nr1;

                    SpecificNumberShowing(nr1);
                }
                else
                {
                    nr2 = (operand * Math.Pow(10, i)) + nr2;
                    SpecificNumberShowing(nr2);

                }
            }
            sirOperatii = sirOperatii + operand.ToString();
        }
        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            Numar(0);
        }
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            Numar(1);
        }
        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            Numar(2);
        }
        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            Numar(3);
        }
        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            Numar(4);
        }
        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            Numar(5);
        }
        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            Numar(6);
        }
        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            Numar(7);
        }
        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            Numar(8);
        }
        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            Numar(9);
        }
        private void btnPunctClick(object sender, RoutedEventArgs e)
        {
            punct = true;
        }

        // Operatori
        private void Operator(String Operator)
        {
            punct = false;i = 0;
            if(numere.Count()==0)
            {
                numere.Enqueue(nr1);
                nr2 = 0;
            }
            else if(numere.Count()==1)
            {
                numere.Enqueue(nr2);
                nr1 = Update(numere.Dequeue(), numere.Dequeue(), operatori.Dequeue());
                numere.Clear();
                numere.Enqueue(nr1);
                nr2 = 0;
            }
            operatori.Enqueue(Operator);
            operatii = Operator;
            sirOperatii = sirOperatii + Operator;
        }
        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            Operator("+");
        }
        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            Operator("-");
        }
        private void btnInmultire_Click(object sender, RoutedEventArgs e)
        {
            Operator("*");
        }
        private void btnImpartire_Click(object sender, RoutedEventArgs e)
        {
            Operator("/");
        }
        private void PregatireNumar()
        {
            punct = false;
            i = 0;
            if (nr2!= int.MinValue&&operatori.Count!=0)
            {
                numere.Enqueue(nr2);
                nr1 = Update(numere.Dequeue(), numere.Dequeue(), operatori.Dequeue());
                nr2 = int.MinValue;
            }

        }

        private void btnPatrat_Click(object sender, RoutedEventArgs e)
        {
            PregatireNumar();
            nr1 = Math.Pow(nr1,2);
            SpecificNumberShowing(nr1);
        }

        private void btnProcent_Click(object sender, RoutedEventArgs e)
        {
            PregatireNumar();
            nr1 = nr1 / 100;
            SpecificNumberShowing(nr1);
        }

        private void btnRadical_Click(object sender, RoutedEventArgs e)
        {
            PregatireNumar();
            nr1 = Math.Sqrt(nr1);
            SpecificNumberShowing(nr1);
        }
        private void btnInversare_Click(object sender, RoutedEventArgs e)
        {
            PregatireNumar();
            nr1 = 1/(nr1);
            SpecificNumberShowing(nr1);
        }
        private void btnPolaritate_Click(object sender, RoutedEventArgs e)
        {
            PregatireNumar();
            nr1 = -(nr1);
            SpecificNumberShowing(nr1);
        }

        //Operatii stergere
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            punct = false;
            i = 0;
            if (nr2 == int.MinValue)
            {
                nr1 = (nr1 - (nr1 % 10)) / 10;
                SpecificNumberShowing(nr1);
            }
            else
            {
                nr2 = (nr2 - (nr2 % 10)) / 10;
                SpecificNumberShowing(nr2);
            }
        }
        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            i = 0;
            nr1 = 0;
            nr2 = int.MinValue;
            numarMemo = 0;

            operatii = "";
            sirOperatii = "";

            numere.Clear();
            operatori.Clear();
            punct = false;

            SpecificNumberShowing(nr1);
        }
        private void btnCE_Click(object sender, RoutedEventArgs e)
        {
            punct = false;
            i = 0;
            if (nr2 == int.MinValue)
            {
                nr1 = int.MinValue;
                SpecificNumberShowing(nr1);
            }
            else
            {
                nr2 = int.MinValue;
                SpecificNumberShowing(nr2);
            }
        }


        //operatii memorie
        private void btnMPlus_Click(object sender, RoutedEventArgs e)
        {
            if (nr2 == int.MinValue)
            {
                numarMemo += nr1;
                SpecificNumberShowing(nr1);
            }
            else
            {
                numarMemo += nr2;
                SpecificNumberShowing(nr2);
            }
            punct = false;
            i = 0;

        }
        private void btnMMinus_Click(object sender, RoutedEventArgs e)
        {
            punct = false;
            i = 0;

            if (nr2 == int.MinValue)
            {
                numarMemo -= nr1;
                SpecificNumberShowing(nr1);
            }
            else
            {
                numarMemo -= nr2;
                SpecificNumberShowing(nr2);
            }
        }
        private void btnMR_Click(object sender, RoutedEventArgs e)
        {
            nr2 = numarMemo;
            SpecificNumberShowing(nr2);
            punct = false;
            i = 0;

        }
        private void btnMC_Click(object sender, RoutedEventArgs e)
        {
            numarMemo = 0;
            nr2 = numarMemo;
            SpecificNumberShowing(nr2);
            punct = false;
            i = 0;
        }
        private void btnEgal_Click(object sender, RoutedEventArgs e)
        {
            punct = false;
            i = 0;

            operatii = "=";
            sirOperatii = sirOperatii + "=";
            if (nr2 != int.MinValue)
            {
                numere.Enqueue(nr2);
                nr1 = Update(numere.Dequeue(), numere.Dequeue(), operatori.Dequeue());
                nr2 = int.MinValue;
            }
            if(nr1==int.MinValue)
            {
                txtDisplay.Text = "Cannot divide by 0!";
                nr1 = 0;
                nr2 = int.MinValue;
                operatii = "";
               // operatori.Dequeue();
            }
            else
            SpecificNumberShowing(nr1);
        }

        // cut copy paste

        private void MenuItem1_Click(System.Object sender, System.EventArgs e)
        {
            Clipboard.SetText(txtDisplay.Text);
        }
        private void MenuItem2_Click(System.Object sender, System.EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true)
            {
                txtDisplay.Text = Clipboard.GetText();
            }
        }
        private void MenuItem3_Click(System.Object sender, System.EventArgs e)
        {
            Clipboard.SetText(txtDisplay.Text);
            txtDisplay.Text = "0";
        }

        private void txtDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D1:
                    btn1_Click(sender,e);
                    break;
                case Key.D2:
                    btn2_Click(sender, e);
                    break;
                case Key.D3:
                    btn3_Click(sender, e);
                    break;
                case Key.D4:
                    btn4_Click(sender, e);
                    break;
                case Key.D5:
                    btn5_Click(sender, e);
                    break;
                case Key.D6:
                    btn6_Click(sender, e);
                    break;
                case Key.D7:
                    btn7_Click(sender, e);
                    break;
                case Key.D8:
                    btn8_Click(sender, e);
                    break;
                case Key.D9:
                    btn9_Click(sender, e);
                    break;
                case Key.D0:
                    btn0_Click(sender, e);
                    break;
                case Key.OemPlus:
                    btnPlus_Click(sender, e);
                    break;
                case Key.Add:
                    btnPlus_Click(sender, e);
                    break;
                case Key.OemMinus:
                    btnMinus_Click(sender, e);
                    break;
                case Key.Subtract:
                    btnMinus_Click(sender, e);
                    break;
                case Key.Multiply:
                    btnInmultire_Click(sender, e);
                    break;
                case Key.Divide:
                    btnImpartire_Click(sender, e);
                    break;
                case Key.Enter:
                    btnEgal_Click(sender, e);
                    break;
                case Key.Escape:
                    btnC_Click(sender, e);
                    break;
                case Key.Back:
                    btnDelete_Click(sender, e);
                    break;
                case Key.OemPeriod:
                    btnPunctClick(sender, e);
                    break;
            }
        }

        private void DG_Checked(object sender, RoutedEventArgs e)
        {
            if (DGE.IsChecked == true)
                DGE.IsChecked = false;
            checkBoxDigitGrouping = 1;
            if (nr2 != int.MinValue)
                SpecificNumberShowing(nr2);
            else
                SpecificNumberShowing(nr1);
            SaveSettings();
        }

        private void DG_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBoxDigitGrouping = 0;
            if (nr2 != int.MinValue)
                SpecificNumberShowing(nr2);
            else
                SpecificNumberShowing(nr1);
            SaveSettings();

        }

        private void DGE_Checked(object sender, RoutedEventArgs e)
        {
            if (DGR.IsChecked == true)
                DGR.IsChecked = false;
            checkBoxDigitGrouping = 2;
            if (nr2 != int.MinValue)
                SpecificNumberShowing(nr2);
            else
                SpecificNumberShowing(nr1);
            SaveSettings();

        }

        private void DGE_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBoxDigitGrouping = 0;
            if (nr2 != int.MinValue)
                SpecificNumberShowing(nr2);
            else
                SpecificNumberShowing(nr1);
            SaveSettings();
        }
        public void GetSettings()
        {
            this.checkBoxDigitGrouping = Properties.Settings.Default.checkBox;
        }
        public void SaveSettings()
        {
            if (DGR.IsChecked == true)
                checkBoxDigitGrouping = 1;
            else if (DGE.IsChecked == true)
                checkBoxDigitGrouping = 2;
            else
                checkBoxDigitGrouping = 0;
            Properties.Settings.Default.checkBox = checkBoxDigitGrouping;
            Properties.Settings.Default.Save();

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(Hyperlink))
                return;
            string link = ((Hyperlink)sender).NavigateUri.ToString();
           System.Diagnostics.Process.Start(link);
        }

        private void txtDisplay_LostFocus(object sender, RoutedEventArgs e)
        {
           txtDisplay.Focus();
        }
    }
}