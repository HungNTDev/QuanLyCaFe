using DTO_QLCaFe;
using System;

using BUS_QLCaFe;
using System.Threading;
using System.Windows.Forms;

namespace GUI_QLCaFe
{
    public partial class GUI_Login : Form
    {
        public GUI_Login()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        DTO_Staff staff = new DTO_Staff();
        public static string TimeMin;
        public static string TimeMax;
        public static string Shift;
        public static string Email;
        public static string IdStaff;
        private void GUI_Login_Load(object sender, EventArgs e)
        {
            timer1.Stop();
            this.ActiveControl = label1;
            ReadSetting();
            
        }

        private void ReadSetting() 
        {
            if(Properties.Settings.Default.Memo == "true")
            {
                Email_txt.Text = Properties.Settings.Default.Email;
                Password_txt.Text = Properties.Settings.Default.Password;
                Memo_ckb.Checked = true;
            }
            else
            {
                Email_txt.Text = Properties.Settings.Default.Email;
                Password_txt.Text = " ";
                this.ActiveControl = Password_txt;
                Memo_ckb.Checked = false;
            }
        }
        private void SaveSettings()
        {
            if (Memo_ckb.Checked) 
            {
                Properties.Settings.Default.Email = this.Email_txt.Text;
                Properties.Settings.Default.Password = this.Password_txt.Text;
                Properties.Settings.Default.Memo = "true";
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Email = this.Email_txt.Text;
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Memo = "false";
                Properties.Settings.Default.Save();
            }
        }

        private void Show_ptb_Click(object sender, EventArgs e)
        {

        }

        private void ShowHide_ckb_CheckedChanged(object sender, EventArgs e)
        {
            if(ShowHide_ckb.Checked == true)
            {
                Show_ptb.Show();
                Hide_ptb.Show();
                Password_txt.UseSystemPasswordChar = false;
            }
            else
            {
                Show_ptb.Hide(); 
                Hide_ptb.Show();
                Password_txt.UseSystemPasswordChar=true;
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                if (Email_txt.Text == "")
                {
                    MessageBox.Show("Đăng nhập không thành công, email không được để trống ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.ActiveControl = Email_txt;
                }
                else if (Password_txt.Text == "")
                {
                    MessageBox.Show("Đăng nhập không thành công, mật khẩu không được để trống ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.ActiveControl = Password_txt;
                }
                else
                {
                    staff.Email = Email_txt.Text;
                    staff.Password = BUS_Staff.Instance.Encryption(Password_txt.Text);
                    if(BUS_Staff.Instance.AcceptLogin(staff) == true &&
                       BUS_Staff.Instance.Check(staff).Rows[0][10].ToString() == "False")
                    {
                        if (BUS_Staff.Instance.Check(staff).Rows[0][7].ToString() == "Quản lý")
                        {
                            Confirm_frm.Result = 1;
                        }
                        else
                        {
                            Confirm_frm.Result = 0;
                        }
                        btnDangNhap.Enabled = false;
                        SaveSettings();
                        IdStaff = BUS_Staff.Instance.Check(staff).Rows[0][0].ToString();
                        LoadBar.Visible = true;
                        timer1_Tick(sender, e);
                    }
                } 
            }
            catch (Exception)
            {
                MessageBox.Show("Đăng nhập không thành công, kiểm tra lại email hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.LoadBar.Increment(12);
            LoadBar.Maximum = 100;
            if(LoadBar.Visible == true)
            {
                timer1.Start();
                Login_grb.Enabled = false;
                if(LoadBar.Value == LoadBar.Maximum)
                {
                    LoadBar.Visible = false;
                    this.Hide();
                    timer1.Stop();
                    Email = Email_txt.Text;
                    if (DateTime.Parse(DateTime.Now.TimeOfDay.ToString()) > DateTime.Parse("06:30:00") && DateTime.Parse(DateTime.Now.TimeOfDay.ToString()) < DateTime.Parse("14:30:00"))
                    {
                        Shift = "Ca sáng";
                        TimeMin = "06:30:00";
                        TimeMax = "14:30:00";
                    }
                    else
                    {
                        Shift = "Ca tối";
                        TimeMin = "14:30:00";
                        TimeMax = "23:30:00";
                    }
                    
                }
                btnDangNhap.Text += ".";
                if (btnDangNhap.Text.Length >= 0) 
                {
                    btnDangNhap.Text = ".";
                }
            }
        }

        private void GUI_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (LoadBar.Visible == true)
            {
                e.Cancel = true;
            }
            else if (LoadBar.Visible != true && GUI_Loading.Status == 1)
            {
                var dlr = MessageBox.Show("Bạn chắc muốn thoát", "Đóng chương trình", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dlr == DialogResult.Yes)
                {
                    System.Environment.Exit(1);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void ForgotPassword_lblk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GUI_ForgotPassword forgotPassword = new GUI_ForgotPassword();
            forgotPassword.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadBar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Password_txt_TextChanged(object sender, EventArgs e)
        {
            if (Password_txt.TextLength > 30)
            {
                MessageBox.Show("Bạn nhập dài quá !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Email_txt_TextChanged(object sender, EventArgs e)
        {
            if (Email_txt.TextLength > 30)
            {
                MessageBox.Show("Bạn nhập dài quá !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Memo_ckb_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
