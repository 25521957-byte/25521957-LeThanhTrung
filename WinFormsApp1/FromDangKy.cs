using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
namespace WinFormsApp1
{
    public partial class FromDangKy : Form
    {
        string chuoiKetNoi =
    @"Data Source=AWGUSTORYA\MSSQLSERVER01;Initial Catalog=QuanLyNguoiDung;Integrated Security=True;TrustServerCertificate=True;";
        public FromDangKy()
        {
            InitializeComponent();
        }

       
        private void FromDangKy_Load(object sender, EventArgs e)
        {
        }

        private string BamMatKhau(string matKhau)
        {
            byte[] salt = new byte[16];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                matKhau, salt, 600000,
                HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                byte[] ketQua = new byte[48];

                Buffer.BlockCopy(salt, 0, ketQua, 0, 16);
                Buffer.BlockCopy(hash, 0, ketQua, 16, 32);

                return Convert.ToBase64String(ketQua);
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

                    string sql = "INSERT INTO Users (TenDangNhap, HoTen, Email, MatKhau) " +
                                 "VALUES (@TenDangNhap, @HoTen, @Email, @MatKhau)";

                    using (SqlCommand lenh = new SqlCommand(sql, ketNoi))
                    {
                        lenh.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        lenh.Parameters.AddWithValue("@HoTen", hoTen);
                        lenh.Parameters.AddWithValue("@Email",
                            email == "" ? (object)DBNull.Value : email);
                        string matKhauDaBam = BamMatKhau(matKhau);
                        lenh.Parameters.AddWithValue("@MatKhau", matKhauDaBam);

                        lenh.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Đăng ký tài khoản thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}