
using System;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp1.DAL;
using WindowsFormsApp1.BUS;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SanPhamBUS bus = new SanPhamBUS();

        public Form1()
        {
            InitializeComponent();

            KiemTraKetNoi();

            Load += Form1_Load;

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnTimKiem.Click += btnTimKiem_Click;

            dgvSanPham.CellClick += dgvSanPham_CellClick;
        }

        // Kiểm tra kết nối
        private void KiemTraKetNoi()
        {
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    MessageBox.Show("Ket noi SalesDB thanh cong!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi ket noi: " + ex.Message);
            }
        }

        // Khi mở Form
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // Hiển thị sản phẩm
        private void LoadData()
        {
            dgvSanPham.DataSource = bus.GetAll();
        }

        // Thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                bool kq = bus.Insert(
                    txtMaSP.Text,
                    txtTenSP.Text,
                    decimal.Parse(txtGiaBan.Text),
                    int.Parse(txtSoLuong.Text));

                if (kq)
                {
                    MessageBox.Show("Them thanh cong!");
                    LoadData();
                    LamMoi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                bool kq = bus.Update(
                    txtMaSP.Text,
                    txtTenSP.Text,
                    decimal.Parse(txtGiaBan.Text),
                    int.Parse(txtSoLuong.Text));

                if (kq)
                {
                    MessageBox.Show("Sua thanh cong!");
                    LoadData();
                    LamMoi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Ban co chac muon xoa?",
                    "Xac nhan",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (bus.Delete(txtMaSP.Text))
                    {
                        MessageBox.Show("Xoa thanh cong!");
                        LoadData();
                        LamMoi();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            dgvSanPham.DataSource = bus.Search(txtTimKiem.Text);
        }

        // Làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
            LoadData();
        }

        private void LamMoi()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtGiaBan.Clear();
            txtSoLuong.Clear();
            txtTimKiem.Clear();
        }

        // Click dòng DataGridView
        private void dgvSanPham_CellClick(object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];

                txtMaSP.Text = row.Cells["MaSP"].Value.ToString();
                txtTenSP.Text = row.Cells["TenSP"].Value.ToString();
                txtGiaBan.Text = row.Cells["GiaBan"].Value.ToString();
                txtSoLuong.Text = row.Cells["SoLuongTon"].Value.ToString();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }
    }
}
