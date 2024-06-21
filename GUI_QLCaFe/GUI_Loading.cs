using System;
using System.Threading;
using System.Windows.Forms;
using KAutoHelper;
using BUS_QLCaFe;

namespace GUI_QLCaFe
{
    public partial class GUI_Loading : Form
    {
        public static int Status = 0;
        public GUI_Loading()
        {
            InitializeComponent();
        }

        private void GUI_Loading_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                BUS_Table.Instance.ListTable();
                Thread.Sleep(1000);
                GUI_Login Login = new GUI_Login();
                Login.ShowDialog();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Status == 1)
            {
                this.Hide();
                timer1.Stop();
                IntPtr hWnd = IntPtr.Zero;
                hWnd = AutoControl.FindWindowHandle(null, "Đăng nhập");
                AutoControl.BringToFront(hWnd);
            }
        }

        private void GUI_Loading_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
