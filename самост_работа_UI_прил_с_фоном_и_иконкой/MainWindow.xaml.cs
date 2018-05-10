using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace самост_работа_UI_прил_с_фоном_и_иконкой
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Uri iconUri = new Uri(@"Z:\pictures\icon.jpg", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource =
                new BitmapImage(new Uri(@"Z:\pictures\background.jpg", UriKind.Absolute));
            this.Background = myBrush;

            DateService.iconDateChange = File.GetLastWriteTime(@"Z:\pictures\icon.jpg");
            DateService.backgroundDateChange = File.GetLastWriteTime(@"Z:\pictures\background.jpg");
            //Timer timer2 = new Timer(new TimerCallback(stubObject => Console.WriteLine(DateTime.Now.ToLongTimeString())), null, 0, 15000);
            //---------------------------------------------------------------------------------------------------------------------------------
            Thread.CurrentThread.Name = "Первый поток";
            txtBlck.Text = Thread.CurrentThread.Name;

            Timer timer = new Timer(timerTick, null, 0, 3000);

            
        }

        private void timerTick(object args)
        {
            Thread.CurrentThread.Name = "Второй поток";

            DateTime _iconDateChange = File.GetLastWriteTime(@"Z:\pictures\icon.jpg");
            DateTime _backgroundDateChange = File.GetLastWriteTime(@"Z:\pictures\background.jpg");

            if (DateService.iconDateChange != _iconDateChange)
            {
                Uri iconUri = new Uri(@"Z:\pictures\icon.jpg", UriKind.RelativeOrAbsolute);
                this.Dispatcher.Invoke(new Action(() =>
                {
                    this.Icon = BitmapFrame.Create(iconUri);
                }));
                DateService.iconDateChange = File.GetLastWriteTime(@"Z:\pictures\icon.jpg");

                MessageBox.Show("");
            }

            if (DateService.backgroundDateChange != _backgroundDateChange)
            {
                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri(@"Z:\pictures\background.jpg", UriKind.Absolute));
                this.Background = myBrush;
                DateService.backgroundDateChange = File.GetLastWriteTime(@"Z:\pictures\background.jpg");
            }

            
        }
    }
}
