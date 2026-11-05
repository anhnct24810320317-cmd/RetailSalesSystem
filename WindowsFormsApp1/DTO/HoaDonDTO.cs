
using System.Collections.Generic;

namespace RetailSalesSystem.DTO
{
    public class HoaDonDTO
    {
        public int MaNV { get; set; }
        public int? MaKH { get; set; }
        public decimal TongTien { get; set; }

        public List<ChiTietHoaDonDTO> ChiTiet
            = new List<ChiTietHoaDonDTO>();
    }
}
