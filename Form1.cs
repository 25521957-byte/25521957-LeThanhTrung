using System;
using Microsoft.Data.SqlClient;
using MyProject;
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            KiemTraKetNoi();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            lblThongBao.Text = "";

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrEmpty(password))
            {
                lblThongBao.Text = "Vui lòng nhập tên đăng nhập và mật khẩu.";
                return;
            }

            MessageBox.Show("Dữ liệu đầu vào hợp lệ!");
        }
        private void KiemTraKetNoi()
        {
            try
            {
                using (SqlConnection conn = DbConnection.GetConnection())
                {
                    conn.Open();
                    MessageBox.Show("Kết nối SQL Server thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể kết nối SQL Server.\n" + ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblThongBao_Click(object sender, EventArgs e)
        {

        }
    }

}
