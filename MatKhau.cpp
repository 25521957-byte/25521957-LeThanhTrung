
#include "MatKhau.h"

#include <openssl/evp.h>
#include <openssl/rand.h>

#include <stdexcept>
#include <vector>

using namespace std;

MatKhau::MatKhau() : soVongLap(600000) {
}

string MatKhau::TaoSalt(size_t kichThuoc) {
    vector<unsigned char> buffer(kichThuoc);

    if (RAND_bytes(buffer.data(), buffer.size()) != 1) {
        throw runtime_error("Khong tao duoc salt");
    }

    return string(
        reinterpret_cast<char*>(buffer.data()),
        buffer.size()
    );
}

string MatKhau::PBKDF2(
    const string& matKhau,
    const string& salt,
    int soVongLap
) {
    unsigned char output[32];

    if (PKCS5_PBKDF2_HMAC(
            matKhau.data(),
            static_cast<int>(matKhau.size()),
            reinterpret_cast<const unsigned char*>(
                salt.data()
            ),
            static_cast<int>(salt.size()),
            soVongLap,
            EVP_sha256(),
            sizeof(output),
            output
        ) != 1) {
        throw runtime_error("Khong tao duoc hash");
    }

    return string(
        reinterpret_cast<char*>(output),
        sizeof(output)
    );
}

void MatKhau::TaoMatKhau(const string& matKhau) {
    salt = TaoSalt();
    hash = PBKDF2(matKhau, salt, soVongLap);
}

bool MatKhau::KiemTraMatKhau(
    const string& matKhauNhap
) const {
    if (salt.empty() || hash.empty()) {
        return false;
    }

    string hashNhap = PBKDF2(
        matKhauNhap,
        salt,
        soVongLap
    );

    unsigned char ketQua = 0;

    for (size_t i = 0; i < hash.size(); ++i) {
        ketQua |= static_cast<unsigned char>(
            hash[i] ^ hashNhap[i]
        );
    }

    return ketQua == 0;
}
