using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIGHTTEAM
{
    public partial class Form02_broken_management : Form
    {
        public Form02_broken_management()
        {
            InitializeComponent(); 
        }

        private void Form2_broken_management_Load(object sender, EventArgs e)
        {
            // 1. 데이터 테이블 생성(그리드에 표현될 컬럼을 셋팅).
            DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("MACHCODE"   , typeof(string)); // 품목 코드
            dtGrid.Columns.Add("MACHNAME"   , typeof(string)); // 품목 명
            dtGrid.Columns.Add("WKNAME"     , typeof(string)); // 고장등록자
            dtGrid.Columns.Add("BORKENDATE" , typeof(string)); // 고장 발생 일시
            dtGrid.Columns.Add("REPDATE"    , typeof(string)); // 수리자 도착 일시
            dtGrid.Columns.Add("REPNAME"    , typeof(string)); // 수리자 명
            dtGrid.Columns.Add("REPCDATE"   , typeof(string)); // 수리자 완료시간
            dtGrid.Columns.Add("FREASON"    , typeof(string)); // 고장사유


            // 2. 셋팅된 컬럼을 그리드에 매핑
            // DataSource : 데이터 베이스에서 가져온 테이블 형식의 데이터를 등록 할때 사용
            dgvGrid.DataSource = dtGrid;

            // 3. 그리드 컬럼에 한글 명칭으로 컬럼 Text 보여주기
            dgvGrid.Columns["MACHCODE"].HeaderText   = "품목 코드";
            dgvGrid.Columns["MACHNAME"].HeaderText   = "품목 명";
            dgvGrid.Columns["WKNAME"].HeaderText     = "고장등록자";
            dgvGrid.Columns["BORKENDATE"].HeaderText = "고장 발생 일시";
            dgvGrid.Columns["REPDATE"].HeaderText    = "수리자 도착 일시";
            dgvGrid.Columns["REPNAME"].HeaderText    = "수리자 명";
            dgvGrid.Columns["REPCDATE"].HeaderText   = "수리자 완료시간";
            dgvGrid.Columns["FREASON"].HeaderText    = "고장사유 ";

            // 4. 고장 사유만 수정 가능하게 변경
            
            dgvGrid.Columns["MACHCODE"].ReadOnly   = true;
            dgvGrid.Columns["MACHNAME"].ReadOnly   = true;
            dgvGrid.Columns["WKNAME"].ReadOnly     = true;
            dgvGrid.Columns["BORKENDATE"].ReadOnly = true;
            dgvGrid.Columns["REPDATE"].ReadOnly    = true;
            dgvGrid.Columns["REPNAME"].ReadOnly    = true;
            dgvGrid.Columns["REPCDATE"].ReadOnly   = true;
            
        }
    }
}
