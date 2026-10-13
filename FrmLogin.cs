using Microsoft.Data.SqlClient;
using System;
using System.Data.Common;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace MyProject
{
    public class FrmLogin : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;

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

        private static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(
                    Encoding.UTF8.GetBytes(password));

                return Convert.ToHexString(bytes);
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu.");
                return;
            }

            try
            {
                using (SqlConnection conn =
                       DbConnection.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        SELECT UserId, Username, FullName, Email
                        FROM Users
                        WHERE Username = @Username
                          AND PasswordHash = @PasswordHash";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue(
                            "@PasswordHash", HashPassword(password));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show(
                                    "Tên đăng nhập hoặc mật khẩu không đúng.");
                                return;
                            }

                            User user = new User
                            {
                                UserId = reader.GetInt32(
                                    reader.GetOrdinal("UserId")),

                                Username = reader["Username"].ToString(),

                                FullName = reader["FullName"].ToString(),

                                Email = reader["Email"] == DBNull.Value
                                    ? ""
                                    : reader["Email"].ToString()
                            };

                            Hide();

                            using (FrmMain mainForm = new FrmMain(user))
                            {
                                mainForm.ShowDialog();
                            }

                            txtPassword.Clear();
                            Show();
                            Activate();
                        }
                    }
                }
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
