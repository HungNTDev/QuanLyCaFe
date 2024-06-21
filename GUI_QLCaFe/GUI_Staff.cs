using BUS_QLCaFe;
using DTO_QLCaFe;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_QLCaFe
{
    public partial class GUI_Staff : Form
    {
        public GUI_Staff()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        DTO_Staff staff = new DTO_Staff();
        private void ListProfileStaff()
        {
            try
            {
                IdStafff_txt.Text = BUS_Staff.Instance.ListProfileStaff().Rows[0][0].ToString();
                NameStaff_txt.Text = BUS_Staff.Instance.ListProfileStaff().Rows[0][1].ToString();
                Adress_txt.Text = BUS_Staff.Instance.ListProfileStaff().Rows[0][2].ToString();
                PhoneNumber_txt.Text = BUS_Staff.Instance.ListProfileStaff().Rows[0][3].ToString();
                Email_txt.Text = BUS_Staff.Instance.ListProfileStaff().Rows[0][4].ToString();
                Gender_cbb.Text = BUS_Staff.Instance.ListProfileStaff().Rows[0][5].ToString();
                BirthDay_date.Text = BUS_Staff.Instance.ListProfileStaff().Rows[0][6].ToString();
                Role_cbb.Text = BUS_Staff.Instance.ListProfileStaff().Rows[0][7].ToString();
                PathPicture_txt.Text = BUS_Staff.Instance.ListProfileStaff().Rows[0][8].ToString();
                Picture_ptb.Image = Image.FromFile(Application.StartupPath + BUS_Staff.Instance.ListProfileStaff().Rows[0][9].ToString());

            }
            catch (Exception) { }
        }

        private void FomatDategridView()
        {
            ListStaff_dgv.DataSource = BUS_Staff.Instance.ListProfileStaff_DGV();
            ListStaff_dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListStaff_dgv.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListStaff_dgv.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListStaff_dgv.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListStaff_dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ListStaff_dgv.Columns[0].Width = 80;
            ListStaff_dgv.Columns[3].Width = 130;
            ListStaff_dgv.Columns[4].Width = 300;
            ListStaff_dgv.Columns[5].Width = 130;
        }
        private void GUI_Staff_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Edit_ckb_CheckedChanged(object sender, EventArgs e)
        {
            if (Edit_ckb.Checked == true)
            {
                //text
                NameStaff_txt.ReadOnly = false;
                PhoneNumber_txt.ReadOnly = false;
                Adress_txt.ReadOnly = false;

                //compoBox
                Gender_cbb.Enabled = true;
                Role_cbb.Enabled = true;


                //date
                BirthDay_date.Enabled = true;

                //btn
                CreateID_btn.Enabled = true;
                Delete_btn.Enabled = true;
                Edit_btn.Enabled = true;
                OpenFilePicture_btn.Visible = true;
            }
            else
            {
                //text
                NameStaff_txt.ReadOnly = true;
                PhoneNumber_txt.ReadOnly = true;
                Adress_txt.ReadOnly = true;
                Find_txt.ReadOnly = false;

                //compoBox
                Gender_cbb.Enabled = false;
                Role_cbb.Enabled = false;
                FindBy_cbb.Enabled = true;

                //date
                BirthDay_date.Enabled = false;

                //btn
                CreateID_btn.Enabled = false;
                Add_btn.Enabled = false;
                Delete_btn.Enabled = false;
                Edit_btn.Enabled = false;
                OpenFilePicture_btn.Visible = false;
                ResetFind_btn.Enabled = true;

                //datagridview
                ListStaff_dgv.Enabled = true;
                //

                NameStaff_lbl.ForeColor = Color.Black;
                Email_lbl.ForeColor = Color.Black;
                Gender_lbl.ForeColor = Color.Black;
                Role_lbl.ForeColor = Color.Black;
                PhoneNumber_lbl.ForeColor = Color.Black;
                Email_lbl.ForeColor = Color.Black;
                Address_lbl.ForeColor = Color.Black;

            }

        }


        private void ResetText()
        {
            IdStafff_txt.Text = "";
            NameStaff_txt.Text = "";
            PhoneNumber_txt.Text = "";
            Email_txt.Text = "";
            Adress_txt.Text = "";
            Role_cbb.SelectedIndex = -1;
            Gender_cbb.SelectedIndex = -1;
            BirthDay_date.Value = DateTime.Now;
            PathPicture_txt.Text = @"\Image\Icon\unknown1.PNG";
            Picture_ptb.Image = Image.FromFile(Application.StartupPath + @"\Image\Icon\unknown1.PNG");

        }
    }
}
