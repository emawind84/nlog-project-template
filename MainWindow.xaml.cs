using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
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

namespace ConsoleTextTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Logger logger;

        public MainWindow()
        {
            InitializeComponent();

            Target.Register<FlowDocumentTarget>("flowDocument");

            foreach (var target in NLog.LogManager.Configuration.AllTargets) {
                Console.WriteLine(target);
            }

            var doc = LogManager.Configuration.FindTargetByName<FlowDocumentTarget>("wpf")?.Document;
            ConsoleText.Document = doc ?? new FlowDocument(new Paragraph(new Run("No target!")));
            ConsoleText.TextChanged += (sender, args) => ConsoleText.ScrollToEnd();

            logger = LogManager.GetCurrentClassLogger();

            logger.Info("Testing 1234");
            logger.Warn("Testing 1234");
            logger.Debug("Testing 1234");
            logger.Error("error !!!!!");
        }

        async private void doLog()
        {
            while (true)
            {
                Thread.Sleep(1000);
                logger.Info("Testing 1234");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread oThread = new Thread(new ThreadStart(doLog));
            oThread.Start();
        }
    }
}
