
#ifndef MATKHAU_H
#define MATKHAU_H

#include <string>

class MatKhau {
private:
    std::string salt;
    std::string hash;
    int soVongLap;

    static std::string TaoSalt(size_t kichThuoc = 16);

    static std::string PBKDF2(
        const std::string& matKhau,
        const std::string& salt,
        int soVongLap
    );

public:
    MatKhau();

    void TaoMatKhau(const std::string& matKhau);

    bool KiemTraMatKhau(
        const std::string& matKhauNhap
    ) const;
};

#endif

