using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BT_B6.Models;

namespace BT_B6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateComboBox();
            LoadDataToDataGridView();

        }
        private void PopulateComboBox()
        {
            cmbNganh.Items.AddRange(new string[]
            {
                "Công nghệ thông tin",
                "Ngôn Ngữ Anh",
                "Quản trị Kinh Doanh"
            });
            cmbNganh.SelectedIndex = 0; // Chọn giá trị mặc định
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var context = new Model1())
            {
                var sinhvien = new SINHVIEN
                {
                    MSSV = txtMSSV.Text,
                    HoTen = txtHoTen.Text,
                    Nghanh = cmbNganh.SelectedItem.ToString(),
                    DTB = decimal.Parse(txtDTB.Text)
                };

                context.SINHVIENs.Add(sinhvien);
                context.SaveChanges();
                MessageBox.Show("Thêm sinh viên thành công!");
                LoadDataToDataGridView();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            using (var context = new Model1())
            {
                var mssv = txtMSSV.Text;
                var sinhvien = context.SINHVIENs.SingleOrDefault(s => s.MSSV == mssv);

                if (sinhvien != null)
                {
                    sinhvien.HoTen = txtHoTen.Text;
                    sinhvien.Nghanh = cmbNganh.SelectedItem.ToString();
                    sinhvien.DTB = decimal.Parse(txtDTB.Text);

                    context.SaveChanges();
                    MessageBox.Show("Sửa thông tin sinh viên thành công!");
                    LoadDataToDataGridView();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy MSSV cần sửa!");
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (var context = new Model1())
            {
                var mssv = txtMSSV.Text;
                var sinhvien = context.SINHVIENs.SingleOrDefault(s => s.MSSV == mssv);

                if (sinhvien != null)
                {
                    context.SINHVIENs.Remove(sinhvien);
                    context.SaveChanges();
                    MessageBox.Show("Xóa sinh viên thành công!");
                    LoadDataToDataGridView();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy MSSV cần xóa!");
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadDataToDataGridView()
        {
            using (var context = new Model1())
            {
                var sinhvienList = context.SINHVIENs.ToList();
                dataGridView1.DataSource = sinhvienList;
            }
        }
    }
}
