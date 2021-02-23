using System.Windows;
using System;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Windows.Controls;

namespace SalaoDeCabelereiro.View
{
    public partial class SplashScreen : Window
    {
        public DispatcherTimer Dt = new DispatcherTimer();
        

        public SplashScreen()
        {
            InitializeComponent();

            Dt.Tick += new EventHandler(DtTick);
            Dt.Interval = new TimeSpan(0, 0, 3);
            Dt.Start();

            Duration duration = new Duration(TimeSpan.FromSeconds(3));
            DoubleAnimation doubleAnimation = new DoubleAnimation(progress.Value + 200, duration);
            progress.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
        }

        private void DtTick(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();

            Dt.Stop();
            this.Close();
        }
    }
}
