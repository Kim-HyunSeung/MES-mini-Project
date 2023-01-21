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
    public partial class Form03_WorkSystem : Form
    {
        public Form03_WorkSystem()
        {
            InitializeComponent();
        }

        private void Form03_WorkSystem_Load(object sender, EventArgs e)
        {
            // 컬럼 이름 지정
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("MACHNAME"   , typeof(string));
            dtTemp.Columns.Add("MODELNAME"  , typeof(string));
            dtTemp.Columns.Add("PDNAME"     , typeof(string));
            dtTemp.Columns.Add("BROKENDOWN" , typeof(string));

            dgvGrid.DataSource = dtTemp;

            // 컬럼 이름 한글명으로 지정.
            dgvGrid.Columns["MACHNAME"].HeaderText   = " 설비명 ";
            dgvGrid.Columns["MODELNAME"].HeaderText  = " 모델명 ";
            dgvGrid.Columns["PDNAME"].HeaderText     = " 제조사 ";
            dgvGrid.Columns["BROKENDOWN"].HeaderText = " 고장유무 ";


            // 컬럼의 폭 지정.
            dgvGrid.Columns[0].Width = 200;  // 설비명
            dgvGrid.Columns[1].Width = 300;  // 모델명
            dgvGrid.Columns[2].Width = 200;  // 제조사
            dgvGrid.Columns[3].Width = 100;  // 고장유무

            // 컬럼의 수정 여부 설정.
            dgvGrid.Columns["MACHNAME"].ReadOnly   = true;
            dgvGrid.Columns["MODELNAME"].ReadOnly  = true;
            dgvGrid.Columns["PDNAME"].ReadOnly     = true;
            dgvGrid.Columns["BROKENDOWN"].ReadOnly = true;

        }
    }
}
