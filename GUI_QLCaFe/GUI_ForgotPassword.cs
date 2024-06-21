using System;
using BUS_QLCaFe;
using DTO_QLCaFe;
using System.Windows.Forms;
using System.Threading;
using System.Net.Mail;
using System.Net;

namespace GUI_QLCaFe
{
    public partial class GUI_ForgotPassword : Form
    {
        public GUI_ForgotPassword()
        {
            InitializeComponent();
        }

        DTO_Staff staff = new DTO_Staff();

        private void GUI_ForgotPassword_Load(object sender, EventArgs e)
        {

        }

        private void Verification_btn_Click(object sender, EventArgs e)
        {
            if (Email_txt.Text == "chinhchu@gmail.com")
            {
                MessageBox.Show("Đây là tài khoản admin không được phép thay đổi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                staff.Email = Email_txt.Text;
                if (BUS_Staff.Instance.ForgotPassword(staff) == true)
                {
                    Wrong_ptb.Hide();
                    Correct_ptb.Show();
                    IdVrf_lbl.Show();
                    IdVrf_txt.Show();
                    Change_btn.Show();
                    Email_txt.ReadOnly = true;
                    Verification_btn.Text = "....";
                    Verification_btn.Enabled = false;
                    string IdVrf = BUS_Staff.Instance.Verification();
                    Thread thread = new Thread(() =>
                    {
                        SendMailVrf(Email_txt.Text, IdVrf);
                        TempIdVrf_txt.Text = IdVrf;
                        Verification_btn.Text = "Kiểm tra";
                        MessageBox.Show("Đã gửi mã xác nhận về email của bạn, vui lòng kiểm tra lại hòm thư email", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    });
                    thread.Start();
                }
                else
                {
                    Email_txt.ReadOnly = false;
                    Wrong_ptb.Show();
                    Correct_ptb.Hide();
                    IdVrf_lbl.Hide();
                    IdVrf_txt.Hide();
                    Change_btn.Hide();
                    Verification_btn.Enabled = true;
                    this.ActiveControl = Email_txt;
                }
            }

        }

        private void SendMailVrf(string Email, string IdVrf)
        {
            try
            {
                SmtpClient Client = new SmtpClient("smtp.gmail.com", 587);
                Client.EnableSsl = true;
                Client.Credentials = new NetworkCredential("hungntps38090@gmail.com", "ckyq jxtl vems yaxs");
                MailMessage Msg = new MailMessage();
                Msg.From = new MailAddress("hungntps38090@gmail.com");
                Msg.To.Add(Email);
                Msg.Subject = "Mã xác nhận";
                Msg.Body = "Chào anh/chị, mã xác nhận mật khẩu là " + IdVrf;

                Client.Send(Msg);
            }
            catch (Exception)
            {

                MessageBox.Show("Có lỗi xãy ra trong quá trình tạo mã", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendMailPassword(string Email, string Password)
        {
            try
            {
                SmtpClient Client = new SmtpClient("smtp.gmail.com", 587);
                Client.EnableSsl = true;
                Client.Credentials = new NetworkCredential("hungntps38090@gmail.com", "ckyq jxtl vems yaxs");
                MailMessage Msg = new MailMessage();
                Msg.From = new MailAddress("hungntps38090@gmail.com");
                Msg.To.Add(Email);
                Msg.Subject = "Bạn đã sử dụng tính năng quên mật khẩu";
                Msg.Body = "Chào anh/chị, mật khẩu mới truy cập vào phần mềm là " + Password;

                Client.Send(Msg);
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra trong quá trình tạo mã", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Change_btn_Click(object sender, EventArgs e)
        {
            try
            {
                VerificationPassword_grb.Enabled = false;
                Change_btn.Text = "....";
                Change_btn.Enabled = false;
                //Thread thread = new Thread(() =>
                //{
                    string NewPassword = BUS_Staff.Instance.VerificationPassword();
                    string Encryption = BUS_Staff.Instance.Encryption(NewPassword);
                    staff.Email = Email_txt.Text;
                    staff.Password = Encryption;
                    BUS_Staff.Instance.UpdatePassword(staff);
                    SendMailPassword(Email_txt.Text, NewPassword);
                    Change_btn.Text = "Khôi phục";
                    MessageBox.Show("Đã gửi mật khẩu mới về email của bạn, vui lòng kiểm tra lại email", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                //});
                //thread.Start();
            }
            catch (Exception)
            {
                VerificationPassword_grb.Enabled = true;
                Change_btn.Enabled = true;
                MessageBox.Show("Có lỗi xảy ra trong quá trình khôi phục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GUI_ForgotPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Verification_btn.Text == "...." || Change_btn.Text == "....")
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void IdVrf_txt_TextChanged(object sender, EventArgs e)
        {
            if (IdVrf_txt.Text == TempIdVrf_txt.Text)
            {
                Change_btn.Enabled = true;
                CorrectIdVrf_ptb.Show();
            }
            else
            {
                Change_btn.Enabled = false;
                CorrectIdVrf_ptb.Hide();
            }
        }
    }
}
