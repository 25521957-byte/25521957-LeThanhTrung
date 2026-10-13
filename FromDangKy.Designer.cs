namespace WinFormsApp1
{
    partial class FromDangKy
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnHuy = new Button();
            btnDangKy = new Button();
            lable1 = new Label();
            lable2 = new Label();
            lable3 = new Label();
            lable4 = new Label();
            lable5 = new Label();
            txtHoTen = new TextBox();
            txtMatKhau = new TextBox();
            txtXacNhanMatKhau = new TextBox();
            txtEmail = new TextBox();
            txtTenDangNhap = new TextBox();
            lblTieuDe = new Label();
            SuspendLayout();
            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(333, 402);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(94, 29);
            btnHuy.TabIndex = 0;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            btnHuy.Click += btnHuy_Click;
            // 
            // btnDangKy
            // 
            btnDangKy.Location = new Point(144, 402);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(94, 29);
            btnDangKy.TabIndex = 1;
            btnDangKy.Text = "Đăng Ký";
            btnDangKy.UseVisualStyleBackColor = true;
            btnDangKy.Click += btnDangKy_Click;
            // 
            // lable1
            // 
            lable1.AutoSize = true;
            lable1.Location = new Point(144, 58);
            lable1.Name = "lable1";
            lable1.Size = new Size(107, 20);
            lable1.TabIndex = 2;
            lable1.Text = "Tên đăng nhập";
            // 
            // lable2
            // 
            lable2.AutoSize = true;
            lable2.Location = new Point(144, 130);
            lable2.Name = "lable2";
            lable2.Size = new Size(54, 20);
            lable2.TabIndex = 3;
            lable2.Text = "Họ tên";
            // 
            // lable3
            // 
            lable3.AutoSize = true;
            lable3.Location = new Point(144, 197);
            lable3.Name = "lable3";
            lable3.Size = new Size(46, 20);
            lable3.TabIndex = 4;
            lable3.Text = "Email";
            // 
            // lable4
            // 
            lable4.AutoSize = true;
            lable4.Location = new Point(144, 265);
            lable4.Name = "lable4";
            lable4.Size = new Size(72, 20);
            lable4.TabIndex = 5;
            lable4.Text = "Mật Khẩu";
            // 
            // lable5
            // 
            lable5.AutoSize = true;
            lable5.Location = new Point(144, 331);
            lable5.Name = "lable5";
            lable5.Size = new Size(139, 20);
            lable5.TabIndex = 6;
            lable5.Text = "Xác Nhận Mật Khẩu";
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(144, 153);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(125, 27);
            txtHoTen.TabIndex = 7;
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(144, 288);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(125, 27);
            txtMatKhau.TabIndex = 8;
            // 
            // txtXacNhanMatKhau
            // 
            txtXacNhanMatKhau.Location = new Point(144, 354);
            txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            txtXacNhanMatKhau.PasswordChar = '*';
            txtXacNhanMatKhau.Size = new Size(125, 27);
            txtXacNhanMatKhau.TabIndex = 9;
            txtXacNhanMatKhau.UseSystemPasswordChar = true;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(144, 220);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(125, 27);
            txtEmail.TabIndex = 10;
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Location = new Point(144, 81);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(125, 27);
            txtTenDangNhap.TabIndex = 11;
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Location = new Point(221, 9);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(132, 20);
            lblTieuDe.TabIndex = 12;
            lblTieuDe.Text = "Đăng Ký Tài Khoản";
            // 
            // FromDangKy
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 453);
            Controls.Add(lblTieuDe);
            Controls.Add(txtTenDangNhap);
            Controls.Add(txtEmail);
            Controls.Add(txtXacNhanMatKhau);
            Controls.Add(txtMatKhau);
            Controls.Add(txtHoTen);
            Controls.Add(lable5);
            Controls.Add(lable4);
            Controls.Add(lable3);
            Controls.Add(lable2);
            Controls.Add(lable1);
            Controls.Add(btnDangKy);
            Controls.Add(btnHuy);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FromDangKy";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng Ký Tài Khoản";
            Load += FromDangKy_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnHuy;
        private Button btnDangKy;
        private Label lable1;
        private Label lable2;
        private Label lable3;
        private Label lable4;
        private Label lable5;
        private TextBox txtHoTen;
        private TextBox txtMatKhau;
        private TextBox txtXacNhanMatKhau;
        private TextBox txtEmail;
        private TextBox txtTenDangNhap;
        private Label lblTieuDe;
    }
}