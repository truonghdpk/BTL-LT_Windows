﻿using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_LT_Windows
{
    public partial class QLMuonTra : Form
    {
        PhieuMuonBUS phieuMuonBUS = new PhieuMuonBUS();
        NhanVienBUS nhanVienBUS = new NhanVienBUS();
        DocGiaBUS docGiaBUS = new DocGiaBUS();
        TaiLieuBUS taiLieuBUS = new TaiLieuBUS();
        List<PhieuTaiLieuDTO> danhSachPhieuTaiLieu = new List<PhieuTaiLieuDTO>();
        public QLMuonTra()
        {
            InitializeComponent();
        }

        public void LoadInit()
        {
            dgvTaiLieuMuon.Rows.Clear();

            foreach(PhieuTaiLieuDTO phieuTaiLieu in danhSachPhieuTaiLieu)
            {
                dgvTaiLieuMuon.Rows.Add(phieuTaiLieu.taiLieu.MaTaiLieu, phieuTaiLieu.taiLieu.TenTaiLieu, phieuTaiLieu.soLuongMuon);
            }
            dgvTaiLieuDangMuon.DataSource = phieuMuonBUS.getDataFromSource();

        }

        private void QLMuonTra_Load(object sender, EventArgs e)
        {
            LoadInit();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(dgvTaiLieuDangMuon.Width, dgvTaiLieuDangMuon.Height);

            dgvTaiLieuDangMuon.DrawToBitmap(bm, new Rectangle(0, 0, this.dgvTaiLieuDangMuon.Width, this.dgvTaiLieuDangMuon.Height));

            e.Graphics.DrawImage(bm, 0, 0);
        }

        private void dgvTaiLieuDangMuon_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            txtMaPhieuMuon.Text = dgvTaiLieuDangMuon.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
            txtMaDocGia.Text = dgvTaiLieuDangMuon.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
            txtNgayMuon.Text = dgvTaiLieuDangMuon.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
            txtMaNhanVien.Text = dgvTaiLieuDangMuon.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();
            txtMaTaiLieu.Text = dgvTaiLieuDangMuon.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();
            txtSoLuongMuon.Text = dgvTaiLieuDangMuon.Rows[e.RowIndex].Cells[5].Value.ToString().Trim();

            //dgvTaiLieuMuon.Rows.Clear();
            //PhieuTaiLieuDTO newPhieuTaiLieu = new PhieuTaiLieuDTO();

            //newPhieuTaiLieu.taiLieu.MaTaiLieu = txtMaTaiLieu.Text;
            //newPhieuTaiLieu.taiLieu.TenTaiLieu = txtTenTaiLieu.Text;
            //newPhieuTaiLieu.soLuongMuon = short.Parse(txtSoLuongMuon.Text);

            //danhSachPhieuTaiLieu.Add(newPhieuTaiLieu);
            //foreach (PhieuTaiLieuDTO phieuTaiLieu in danhSachPhieuTaiLieu)
            //{
            //    dgvTaiLieuMuon.Rows.Add(phieuTaiLieu.taiLieu.MaTaiLieu, phieuTaiLieu.taiLieu.TenTaiLieu, phieuTaiLieu.soLuongMuon);
            //}
        }

        private void txtMaDocGia_TextChanged(object sender, EventArgs e)
        {
            txtTenDocGia.Text = docGiaBUS.searchTenDocGia(txtMaDocGia.Text.ToString());
        }

        private void txtMaNhanVien_TextChanged_1(object sender, EventArgs e)
        {
            txtTenNhanVien.Text = nhanVienBUS.searchTenNhanVien(txtMaNhanVien.Text.ToString());
        }

        private void txtMaTaiLieu_TextChanged_1(object sender, EventArgs e)
        {
            txtTenTaiLieu.Text = taiLieuBUS.searchTenTaiLieu(txtMaTaiLieu.Text.ToString());
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            TaiLieuDTO taiLieu = taiLieuBUS.searchTaiLieu(txtMaTaiLieu.Text.ToString());
            if (taiLieu == null) return;
            PhieuTaiLieuDTO newPhieuTaiLieu = new PhieuTaiLieuDTO();
            newPhieuTaiLieu.soLuongMuon = short.Parse(txtSoLuongMuon.Text);
            newPhieuTaiLieu.taiLieu.MaTaiLieu = txtMaTaiLieu.Text;
            newPhieuTaiLieu.taiLieu.TenTaiLieu = txtTenTaiLieu.Text;
            danhSachPhieuTaiLieu.Add(newPhieuTaiLieu);
            this.LoadInit();
        }

        private void btn_Click_1(object sender, EventArgs e)
        {
            // Lưu button
            string maPhieuMuon = txtMaPhieuMuon.Text.ToString();
            string maDocGia = txtMaDocGia.Text.ToString();
            DateTime ngayMuon = txtNgayMuon.Value;
            string maNhanVien = txtMaNhanVien.Text.ToString();

            PhieuMuonDTO newPhieuMuon = new PhieuMuonDTO(maPhieuMuon, maDocGia, maNhanVien, ngayMuon);
            newPhieuMuon.DanhSachPhieuTaiLieu = danhSachPhieuTaiLieu;
            phieuMuonBUS.addNewData(newPhieuMuon);
            this.LoadInit();
        }

        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            txtMaPhieuMuon.Text = "";
            txtMaDocGia.Text = "";
            txtNgayMuon.Value = DateTime.Today;
            txtMaNhanVien.Text = "";
            txtMaTaiLieu.Text = "";
            txtSoLuongMuon.Text = "";

            dgvTaiLieuMuon.Rows.Clear();
            danhSachPhieuTaiLieu.Clear();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void btnTraSach_Click(object sender, EventArgs e)
        {
            PhieuMuonDTO currentPhieuMuuon = phieuMuonBUS.getPhieuByMa(txtMaPhieuMuon.Text, txtMaTaiLieu.Text);
            for( int i = 0; i < currentPhieuMuuon.DanhSachPhieuTaiLieu.Count; i++)
            {
                currentPhieuMuuon.DanhSachPhieuTaiLieu[i].ngayTra = DateTime.Now;
            }
            phieuMuonBUS.updateData(currentPhieuMuuon);
            this.LoadInit();
        }
    }
}
