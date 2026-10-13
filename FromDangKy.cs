using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
namespace WinFormsApp1
{
    public partial class FromDangKy : Form
    {
        string chuoiKetNoi =
    @"Data Source=DESKTOP-K35USV3;Initial Catalog=QuanLyNguoiDung;Integrated Security=True;TrustServerCertificate=True;";
        public FromDangKy()
        {
            InitializeComponent();
        }

       
        private void FromDangKy_Load(object sender, EventArgs e)
        {
        }


        private byte[] BamMatKhau(string matKhau, byte[] salt)
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

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            string matKhau = txtMatKhau.Text;
            string xacNhanMatKhau = txtXacNhanMatKhau.Text;

            if (tenDangNhap == "" || hoTen == "" ||
                matKhau == "" || xacNhanMatKhau == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc!");
                return;
            }

            if (tenDangNhap.Length < 3 || tenDangNhap.Length > 30)
            {
                MessageBox.Show("Tên đăng nhập phải từ 3 đến 30 ký tự!");
                return;
            }

            if (matKhau.Length < 8)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 8 ký tự!");
                return;
            }

            if (matKhau != xacNhanMatKhau)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!");
                return;
            }

            try
            {
                using (SqlConnection ketNoi = new SqlConnection(chuoiKetNoi))
                {
                    ketNoi.Open();

                    string sqlKiemTra =
                        "SELECT COUNT(*) FROM Users WHERE TenDangNhap = @TenDangNhap";

                    using (SqlCommand lenhKiemTra =
                        new SqlCommand(sqlKiemTra, ketNoi))
                    {
                        lenhKiemTra.Parameters.AddWithValue(
                            "@TenDangNhap", tenDangNhap);

                        int soLuong = (int)lenhKiemTra.ExecuteScalar();

                        if (soLuong > 0)
                        {
                            MessageBox.Show("Tên đăng nhập đã tồn tại!");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối SQL Server: " + ex.Message);
                return;
            }
            if (email != "" &&
                (!email.Contains("@") || !email.Contains(".")))
            {
                MessageBox.Show("Email không hợp lệ!");
                return;
            }


            try
            {
                using (SqlConnection ketNoi = new SqlConnection(chuoiKetNoi))
                {
                    ketNoi.Open();

                    // Tao salt ngau nhien 16 byte
                    byte[] salt = new byte[16];

                    using (var rng = RandomNumberGenerator.Create())
                    {
                        rng.GetBytes(salt);
                    }

                    // Bam mat khau bang PBKDF2-SHA256
                    byte[] matKhauBam = BamMatKhau(matKhau, salt);

                    string sql = @"
            INSERT INTO dbo.Users
                (TenDangNhap, HoTen, Email, MatKhau, Salt)
            VALUES
                (@TenDangNhap, @HoTen, @Email, @MatKhau, @Salt)";

                    using (SqlCommand lenh = new SqlCommand(sql, ketNoi))
                    {
                        lenh.Parameters.Add(
                            "@TenDangNhap",
                            System.Data.SqlDbType.NVarChar, 50
                        ).Value = tenDangNhap;

                        lenh.Parameters.Add(
                            "@HoTen",
                            System.Data.SqlDbType.NVarChar, 100
                        ).Value = hoTen;

                        lenh.Parameters.Add(
                            "@Email",
                            System.Data.SqlDbType.NVarChar, 254
                        ).Value = email == ""
                            ? DBNull.Value
                            : email;

                        lenh.Parameters.Add(
                            "@MatKhau",
                            System.Data.SqlDbType.VarBinary, 32
                        ).Value = matKhauBam;

                        lenh.Parameters.Add(
                            "@Salt",
                            System.Data.SqlDbType.VarBinary, 16
                        ).Value = salt;

                        lenh.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Đăng ký tài khoản thành công!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL Server: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }
    }
}