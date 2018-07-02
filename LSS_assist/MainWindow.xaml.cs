using System;
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
using System.Windows.Threading;
using System.Windows.Forms;

namespace LSS_assist
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
        DispatcherTimer dispatcherTimer;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //  DispatcherTimer setup
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            // 10ms for each tick.
            // normally the target can get ~50 clicks per seconds, more messages are abandoned.
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);


            label.Content = "Handle: " + WindowHook.FindWindow(null, "LSS").ToString();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 1; i++)
            {
                if (radioButtonEarn.IsChecked == true)
                    WindowHook.SendMouseManaged("EARN");
                if (radioButtonGive.IsChecked == true)
                    WindowHook.SendMouseManaged("GIVE");
            }
            //SendKeys.SendWait("{TAB}");
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            //progressBar1.IsIndeterminate = dispatcherTimer.IsEnabled;
            //if (dispatcherTimer.IsEnabled)
            //    dispatcherTimer.Start();
            //else dispatcherTimer.Stop();
            //
            //label.Content = "Handle: " + WindowHook.FindWindow(null, "LSS").ToString();
        }

        private void buttonSendEnter_Click(object sender, RoutedEventArgs e)
        {
            //WindowHook.SendKeyManaged(Key.Enter);
        }
        private void buttonSendTab_Click(object sender, RoutedEventArgs e)
        {
            //WindowHook.SendKeyManaged(Key.Tab);
        }

        private void radioButtonNone_Checked(object sender, RoutedEventArgs e)
        {
            if (radioButtonNone.IsChecked == false)
            {
                dispatcherTimer.Start();
                label.Content = "Handle: " + WindowHook.FindWindow(null, "LSS").ToString();
                // You have to make progress bar looping so that LSS window can receive messages.
                // Otherwise, message would deliver at a very slow various rate, (1 message per sec)
                progressBar1.IsIndeterminate = true;
            }
            else
            {
                dispatcherTimer?.Stop();
                label.Content = "Stoppped";
                if (progressBar1 != null)
                    progressBar1.IsIndeterminate = false;
            }
        }
    }
}
