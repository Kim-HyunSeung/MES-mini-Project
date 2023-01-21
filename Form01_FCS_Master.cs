using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EZ_FCS
{
    public partial class Form01_FCS_Master : Form
    {
        public Form01_FCS_Master()
        {
            InitializeComponent();
        }

        private void Form01_FCS_Master_Load(object sender, EventArgs e)
        {
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("MACHCODE",   typeof(string));   // 설비 코드
            dtTemp.Columns.Add("MACHNAME",   typeof(string));   // 설비명
            dtTemp.Columns.Add("USEFLAG",    typeof(string));   // 사용여부
            dtTemp.Columns.Add("BROKENDOWN", typeof(string));   // 고장 유무

            dgvGrid.DataSource = dtTemp;

            // 컬럼 이름 한글명으로 지정.
            dgvGrid.Columns["MACHCODE"].HeaderText = " 설비 코드 ";
            dgvGrid.Columns["MACHNAME"].HeaderText = " 설비명 ";
            dgvGrid.Columns["USEFLAG"].HeaderText   = " 사용여부 ";
            dgvGrid.Columns["BROKENDOWN"].HeaderText  = " 고장 유무 ";

            dgvGrid.Columns[0].Width = 100;  // 설비 코드
            dgvGrid.Columns[1].Width = 200;  // 설비명
            dgvGrid.Columns[2].Width = 100;  // 사용여부
            dgvGrid.Columns[3].Width = 100;  // 고장 유무


        }
    }
}
