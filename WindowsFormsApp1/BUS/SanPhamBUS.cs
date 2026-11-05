
using System.Data;
using WindowsFormsApp1.DAL;

namespace WindowsFormsApp1.BUS
{
    public class SanPhamBUS
    {
        SanPhamDAL dal = new SanPhamDAL();

        // Lấy danh sách sản phẩm
        public DataTable GetAll()
        {
            return dal.GetAll();
        }

        // Thêm sản phẩm
        public bool Insert(string maSP, string tenSP,
                           decimal giaBan, int soLuong)
        {
            return dal.Insert(maSP, tenSP, giaBan, soLuong);
        }

        // Sửa sản phẩm
        public bool Update(string maSP, string tenSP,
                           decimal giaBan, int soLuong)
        {
            return dal.Update(maSP, tenSP, giaBan, soLuong);
        }

        // Xóa sản phẩm
        public bool Delete(string maSP)
        {
            return dal.Delete(maSP);
        }

        // Tìm kiếm sản phẩm
        public DataTable Search(string keyword)
        {
            return dal.Search(keyword);
        }
    }
}
