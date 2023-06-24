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
using System.Windows.Shell;

using Path = System.IO.Path;

namespace Stacks
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

        private void AddTask(object sender, RoutedEventArgs e)
        {
            // Configure a new JumpTask.
            JumpTask jumpTask1 = new JumpTask();
            // Get the path to Calculator and set the JumpTask properties.
            jumpTask1.ApplicationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "calc.exe");
            jumpTask1.IconResourcePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "calc.exe");
            jumpTask1.Title = "Calculator";
            jumpTask1.Description = "Open Calculator.";
            jumpTask1.CustomCategory = "User Added Tasks";
            // Get the JumpList from the application and update it.
            JumpList jumpList1 = JumpList.GetJumpList(App.Current);
            jumpList1.JumpItems.Add(jumpTask1);
            JumpList.AddToRecentCategory(jumpTask1);
            jumpList1.Apply();
        }
        private void ClearJumpList(object sender, RoutedEventArgs e)
        {
            JumpList jumpList1 = JumpList.GetJumpList(App.Current);
            jumpList1.JumpItems.Clear();
            jumpList1.Apply();
        }
        private void SetNewJumpList(object sender, RoutedEventArgs e)
        {
            //Configure a new JumpTask
            JumpTask jumpTask1 = new JumpTask();
            // Get the path to WordPad and set the JumpTask properties.
            jumpTask1.ApplicationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "write.exe");
            jumpTask1.IconResourcePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "write.exe");
            jumpTask1.Title = "WordPad";
            jumpTask1.Description = "Open WordPad.";
            jumpTask1.CustomCategory = "Jump List 2";
            // Create and set the new JumpList.
            JumpList jumpList2 = new JumpList();
            jumpList2.JumpItems.Add(jumpTask1);
            JumpList.SetJumpList(App.Current, jumpList2);
        }

        private void AddRdpConnection(object sender, RoutedEventArgs e)
        {
            var server = textRdp.Text;

            var jumpTask = new JumpTask
            {
                Title = server,
                Description = $"Connect to {server}",
                ApplicationPath = @"%windir%\system32\mstsc.exe",
                WorkingDirectory = @"%windir%\system32",
                Arguments = $"/v:{server}",
            };

            // Get the JumpList from the application and update it.
            JumpList jumpList = JumpList.GetJumpList(App.Current);
            jumpList.JumpItems.Add(jumpTask);
            JumpList.AddToRecentCategory(jumpTask);
            jumpList.Apply();
        }
    }
}
