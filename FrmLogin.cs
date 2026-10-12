
using Microsoft.Data.SqlClient;
using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace MyProject
{
    public class FrmLogin : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;

        private readonly string chuoiKetNoi =
            @"Data Source=DESKTOP-K35USV3;Initial Catalog=QuanLyNguoiDung;Integrated Security=True;TrustServerCertificate=True;";

        public FrmLogin()
        {
            Text = "Đăng nhập";
            Size = new Size(350, 240);
            StartPosition = FormStartPosition.CenterScreen;

            Label lblUsername = new Label
            {
                Text = "Tên đăng nhập:",
                Location = new Point(20, 25),
                AutoSize = true
            };

            txtUsername = new TextBox
            {
                Location = new Point(130, 22),
                Width = 170
            };

            Label lblPassword = new Label
            {
                Text = "Mật khẩu:",
                Location = new Point(20, 65),
                AutoSize = true
            };

            txtPassword = new TextBox
            {
                Location = new Point(130, 62),
                Width = 170,
                UseSystemPasswordChar = true
            };

            btnLogin = new Button
            {
                Text = "Đăng nhập",
                Location = new Point(130, 110),
                Width = 120
            };

            btnLogin.Click += BtnLogin_Click;

            Controls.AddRange(new Control[]
            {
                lblUsername, txtUsername,
                lblPassword, txtPassword, btnLogin
            });

            AcceptButton = btnLogin;
        }

        private static byte[] BamMatKhau(
            string matKhau, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(
                matKhau,
                salt,
                600000,
                HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(32);
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Vui lòng nhập tài khoản và mật khẩu.");
                return;
            }

            User user = null;

            try
            {
                using (SqlConnection conn =
                       new SqlConnection(chuoiKetNoi))
                {
                    conn.Open();

                    string sql = @"
                        SELECT UserId, TenDangNhap, HoTen, Email,
                               MatKhau, Salt
                        FROM dbo.Users
                        WHERE TenDangNhap = @TenDangNhap";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(
                            "@TenDangNhap",
                            System.Data.SqlDbType.NVarChar,
                            50).Value = username;

                        using (SqlDataReader reader =
                               cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                byte[] hashTrongDB =
                                    (byte[])reader["MatKhau"];

                                byte[] saltTrongDB =
                                    (byte[])reader["Salt"];

                                byte[] hashNhap =
                                    BamMatKhau(password, saltTrongDB);

                                if (CryptographicOperations
                                    .FixedTimeEquals(
                                        hashNhap, hashTrongDB))
                                {
                                    user = new User
                                    {
                                        UserId = reader.GetInt32(
                                            reader.GetOrdinal("UserId")),

                                        Username = reader[
                                            "TenDangNhap"].ToString(),

                                        FullName = reader[
                                            "HoTen"].ToString(),

                                        Email = reader["Email"] == DBNull.Value
                                            ? ""
                                            : reader["Email"].ToString()
                                    };
                                }
                            }
                        }
                    }
                }

                if (user == null)
                {
                    MessageBox.Show(
                        "Tên đăng nhập hoặc mật khẩu không đúng.");
                    return;
                }

                Hide();

                using (FrmMain mainForm = new FrmMain(user))
                {
                    mainForm.ShowDialog();
                }

                txtPassword.Clear();
                Show();
                Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể đăng nhập. Vui lòng kiểm tra kết nối.\n"
                    + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
