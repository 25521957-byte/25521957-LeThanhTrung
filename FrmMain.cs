
using Microsoft.Data.SqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyProject
{
    public class FrmMain : Form
    {
        private readonly User currentUser;

        private Label lblWelcome;
        private Label lblUserId;
        private Label lblUsername;
        private Label lblFullName;
        private Label lblEmail;
        private Button btnLogout;

        private readonly string chuoiKetNoi =
            @"Data Source=DESKTOP-K35USV3;Initial Catalog=QuanLyNguoiDung;Integrated Security=True;TrustServerCertificate=True;";

        public FrmMain(User user)
        {
            currentUser = user;

            InitializeComponent();
            LoadUserInfo();
        }

        private void InitializeComponent()
        {
            Text = "Form Chính";
            Size = new Size(520, 350);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            lblWelcome = new Label
            {
                Text = "Xin chào, " + currentUser.FullName + "!",
                Location = new Point(25, 20),
                Size = new Size(450, 35),
                Font = new Font("Arial", 16, FontStyle.Bold)
            };

            Label lblTitle = new Label
            {
                Text = "THÔNG TIN TÀI KHOẢN",
                Location = new Point(25, 75),
                Size = new Size(450, 30),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };

            lblUserId = CreateInfoLabel(25, 120);
            lblUsername = CreateInfoLabel(25, 155);
            lblFullName = CreateInfoLabel(25, 190);
            lblEmail = CreateInfoLabel(25, 225);

            btnLogout = new Button
            {
                Text = "Đăng xuất",
                Location = new Point(350, 265),
                Size = new Size(120, 35)
            };

            btnLogout.Click += BtnLogout_Click;

            Controls.AddRange(new Control[]
            {
                lblWelcome,
                lblTitle,
                lblUserId,
                lblUsername,
                lblFullName,
                lblEmail,
                btnLogout
            });
        }

        private Label CreateInfoLabel(int x, int y)
        {
            return new Label
            {
                Location = new Point(x, y),
                Size = new Size(450, 25),
                Font = new Font("Arial", 10)
            };
        }

        private void LoadUserInfo()
        {
            try
            {
                using (SqlConnection conn =
                       new SqlConnection(chuoiKetNoi))
                {
                    conn.Open();

                    string sql = @"
                        SELECT UserId, TenDangNhap, HoTen, Email
                        FROM dbo.Users
                        WHERE UserId = @UserId";

                    using (SqlCommand cmd =
                           new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(
                            "@UserId",
                            System.Data.SqlDbType.Int
                        ).Value = currentUser.UserId;

                        using (SqlDataReader reader =
                               cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblUserId.Text =
                                    "Mã người dùng: "
                                    + reader["UserId"];

                                lblUsername.Text =
                                    "Tên đăng nhập: "
                                    + reader["TenDangNhap"];

                                lblFullName.Text =
                                    "Họ và tên: "
                                    + reader["HoTen"];

                                lblEmail.Text =
                                    "Email: "
                                    + (reader["Email"] == DBNull.Value
                                        ? "Chưa cập nhật"
                                        : reader["Email"].ToString());

                                lblWelcome.Text =
                                    "Xin chào, "
                                    + reader["HoTen"] + "!";
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Không tìm thấy thông tin tài khoản.");

                                Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tải dữ liệu: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất không?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
