using System;
using System.Windows;
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
            RefreshList();
        }

        private void RefreshList()
        {
            listTasks.Items.Clear();

            JumpList jumpList = JumpList.GetJumpList(App.Current);

            foreach (var item in jumpList.JumpItems)
            {
                listTasks.Items.Add(item);                
            }
        }

        private void SaveList()
        {
            ClearJumpList();

            var jumpList = JumpList.GetJumpList(App.Current);

            foreach (JumpTask task in listTasks.Items)
            {
                jumpList.JumpItems.Add(task);
            }

            jumpList.Apply();
        }

        private void ClearJumpList()
        {
            var jumpList = JumpList.GetJumpList(App.Current);

            jumpList.JumpItems.Clear();
            jumpList.Apply();
        }

        private void ClearJumpList_Click(object sender, RoutedEventArgs e)
        {
            ClearJumpList();
            RefreshList();
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
                // IconResourcePath = @"C:\Windows\System32\shell32.dll",
                // IconResourceIndex = 138,
                ApplicationPath = @"%windir%\system32\mstsc.exe",
                WorkingDirectory = @"%windir%\system32",
                Arguments = $"/v:{server}",
            };

            // Get the JumpList from the application and update it.
            JumpList jumpList = JumpList.GetJumpList(App.Current);
            jumpList.JumpItems.Add(jumpTask);
            JumpList.AddToRecentCategory(jumpTask);
            jumpList.Apply();

            RefreshList();
        }

        private void RemoveTask_Click(object sender, EventArgs e)
        {
            int index = listTasks.SelectedIndex;

            if (index >= 0)
            {
                listTasks.Items.RemoveAt(index);
            }

            SaveList();
            RefreshList();
        }
    }
}
