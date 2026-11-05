
using System;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp1.DAL
{
    public class SanPhamDAL
    {
        // Lấy tất cả sản phẩm
        public DataTable GetAll()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = "SELECT * FROM SanPham";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();

                da.Fill(dt);
                return dt;
            }
        }

        // Thêm sản phẩm
        public bool Insert(string maSP, string tenSP,
                           decimal giaBan, int soLuong)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"INSERT INTO SanPham
                (MaSP, TenSP, GiaBan, SoLuongTon)
                VALUES (@MaSP, @TenSP, @GiaBan, @SoLuong)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@MaSP", maSP);
                cmd.Parameters.AddWithValue("@TenSP", tenSP);
                cmd.Parameters.AddWithValue("@GiaBan", giaBan);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Sửa sản phẩm
        public bool Update(string maSP, string tenSP,
                           decimal giaBan, int soLuong)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"UPDATE SanPham
                SET TenSP=@TenSP, GiaBan=@GiaBan,
                    SoLuongTon=@SoLuong
                WHERE MaSP=@MaSP";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@MaSP", maSP);
                cmd.Parameters.AddWithValue("@TenSP", tenSP);
                cmd.Parameters.AddWithValue("@GiaBan", giaBan);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Xóa sản phẩm
        public bool Delete(string maSP)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = "DELETE FROM SanPham WHERE MaSP=@MaSP";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSP", maSP);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Tìm kiếm sản phẩm
        public DataTable Search(string keyword)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"SELECT * FROM SanPham
                WHERE MaSP LIKE @keyword
                OR TenSP LIKE @keyword";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue(
                    "@keyword", "%" + keyword + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);
                return dt;
            }
        }
    }
}
