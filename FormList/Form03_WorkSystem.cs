using Assemble;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormList
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
            dtTemp.Columns.Add("MACHNAME", typeof(string));
            dtTemp.Columns.Add("MODELNAME", typeof(string));
            dtTemp.Columns.Add("PDNAME", typeof(string));
            dtTemp.Columns.Add("BROKENDOWN", typeof(string));

            dgvGrid.DataSource = dtTemp;

            // 컬럼 이름 한글명으로 지정.
            dgvGrid.Columns["MACHNAME"].HeaderText = " 설비명 ";
            dgvGrid.Columns["MODELNAME"].HeaderText = " 모델명 ";
            dgvGrid.Columns["PDNAME"].HeaderText = " 제조사 ";
            dgvGrid.Columns["BROKENDOWN"].HeaderText = " 고장유무 ";


            // 컬럼의 폭 지정.
            dgvGrid.Columns[0].Width = 200;  // 설비명
            dgvGrid.Columns[1].Width = 300;  // 모델명
            dgvGrid.Columns[2].Width = 200;  // 제조사
            dgvGrid.Columns[3].Width = 100;  // 고장유무

            // 컬럼의 수정 여부 설정.
            dgvGrid.Columns["MACHNAME"].ReadOnly = true;
            dgvGrid.Columns["MODELNAME"].ReadOnly = true;
            dgvGrid.Columns["PDNAME"].ReadOnly = true;
            dgvGrid.Columns["BROKENDOWN"].ReadOnly = true;

        }

        private void btnSerach_Click(object sender, EventArgs e)
        {
            // 조회조건 받아올 변수 파라매터 선언 및 값 등록.
            string sFcName = txtFCN.Text;        // 설비명 입력 정보.  

            DBHelper helper = new DBHelper();
            try
            {

                SqlDataAdapter Adapter = new SqlDataAdapter("SP_WsMaster_S1", helper.sCon);

                // Adapter 에게 지정 프로시저 형식의 SQL 을 실행할것을 등록함.
                Adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                // 저장 프로시저가 받을 파라매터(인자) 값 설정.      
                Adapter.SelectCommand.Parameters.AddWithValue("@MACHNAME", sFcName);

                // 기본적으로 모든 프로시저에 적용될 내용.
                Adapter.SelectCommand.Parameters.AddWithValue("@LANG", "KO");
                Adapter.SelectCommand.Parameters.AddWithValue("@RS_CODE", "").Direction = ParameterDirection.Output;
                Adapter.SelectCommand.Parameters.AddWithValue("@RS_MSG", "").Direction = ParameterDirection.Output;


                DataTable dtTemp = new DataTable();
                Adapter.Fill(dtTemp);

                // 조회 결과를 그리드에 매핑(바인딩)   // 12-13 콤보박스 마저 해야댐.
                dgvGrid.DataSource = dtTemp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }

        private void dgvGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtFCN.Text = Convert.ToString(dgvGrid.CurrentRow.Cells["MACHNAME"].Value);
        }

        private void btnFB_Click(object sender, EventArgs e)
        {
            if ("E" == Convert.ToString(dgvGrid.CurrentRow.Cells["BROKENDOWN"].Value))
            {
                MessageBox.Show("이미 고장중인건 선택 할수 없습니다.");
                return;
            }
            if (txtFCN.Text == "")
            {
                MessageBox.Show("설비명을 선택 하여 주십시오.");
                return;
            }
            else if (txtregistrant.Text == "")
            {
                MessageBox.Show("설비 고장 등록자를 입력하십시오.");
                return;
            }
            else if (txtfixer.Text != "")
            {
                MessageBox.Show("설비 수리 담당자에 이름을 넣지 마십시오.");
                return;
            }
            string sFCN = txtFCN.Text;
            string sRST = txtregistrant.Text;

            DBHelper helper = new DBHelper();
            SqlConnection sCon = new SqlConnection(Common.sConn);
            sCon.Open();
            DataTable dtTemp = new DataTable();
            string sUpInSQL = string.Empty;
            string sCODE = string.Empty;



            SqlTransaction sTran = sCon.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Transaction = sTran;// 1. 커맨드에 트랜잭션 연결
                cmd.Connection = sCon;   // 2. 커맨드에 접속정보 연결



                sUpInSQL = "    DECLARE @MACHCODE VARCHAR(10)    ";
                sUpInSQL += "    select @MACHCODE = MACHCODE      ";
                sUpInSQL += "    from TB_FCSMaster                ";
                sUpInSQL += $"   where MACHNAME = '{sFCN}'        ";

                sUpInSQL += "INSERT INTO TB_BKMMT (MACHCODE, WKNAME, BORKENDATE) " +
                          $"                VALUES( @MACHCODE, '{sRST}', getdate())";

                sUpInSQL += "UPDATE TB_FCSMaster         ";
                sUpInSQL += "  SET   BROKENDOWN = 'E'    ";
                sUpInSQL += " WHERE MACHCODE = @MACHCODE;";




                cmd.CommandText = sUpInSQL; // 3. 커맨드에 SQL 구문 등록.
                cmd.ExecuteNonQuery(); // 커맨드 실행.
                sTran.Commit();

                MessageBox.Show("정상적으로 등록을 완료 하였습니다.");
                // 재 조 회

            }
            catch (Exception ex)
            {
                sTran.Rollback();
            }
            finally
            {
                sCon.Close();
            }
            // 재 조 회
            btnSerach_Click(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtfixer.Text == "")
            {
                MessageBox.Show("설비 수리 담당자 이름을 입력하십시오.");
                return;
            }

            string sFCN = txtFCN.Text;
            string sFXR = txtfixer.Text;

            DBHelper helper = new DBHelper();
            SqlConnection sCon = new SqlConnection(Common.sConn);
            sCon.Open();
            DataTable dtTemp = new DataTable();
            string sUpInSQL = string.Empty;
            


            SqlTransaction sTran = sCon.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Transaction = sTran;// 1. 커맨드에 트랜잭션 연결
                cmd.Connection = sCon;   // 2. 커맨드에 접속정보 연결

                sUpInSQL = "    DECLARE @MACHCODE VARCHAR(10)    ";
                sUpInSQL += "    select @MACHCODE = MACHCODE      ";
                sUpInSQL += "    from TB_FCSMaster                ";
                sUpInSQL += $"   where MACHNAME = '{sFCN}'        ";
                sUpInSQL +=  "UPDATE TB_BKMMT         ";
                sUpInSQL +=  "  SET REPDATE    =  getdate()    ";
                sUpInSQL += $"     ,REPNAME    =  '{sFXR}'    ";
                sUpInSQL += $" WHERE MACHCODE  =  @MACHCODE     ";
                sUpInSQL += $" AND FINISHFLAG  =  'N'      ;";

                cmd.CommandText = sUpInSQL; // 3. 커맨드에 SQL 구문 등록.
                cmd.ExecuteNonQuery(); // 커맨드 실행.
                sTran.Commit();

                MessageBox.Show("정상적으로 등록을 완료 하였습니다.");

            }

            catch (Exception ex)
            {
                sTran.Rollback();

            }

            finally
            {
                sCon.Close();
            }

        }

        private void btnREPC_Click(object sender, EventArgs e)
        {
            if (txtfixer.Text == "")
            {
                MessageBox.Show("설비 수리 담당자 이름을 입력하십시오.");
                return;
            }

            string sFCN = txtFCN.Text;
            

            DBHelper helper = new DBHelper();
            SqlConnection sCon = new SqlConnection(Common.sConn);
            sCon.Open();
            DataTable dtTemp = new DataTable();
            string sUpInSQL = string.Empty;



            SqlTransaction sTran = sCon.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Transaction = sTran;// 1. 커맨드에 트랜잭션 연결
                cmd.Connection = sCon;   // 2. 커맨드에 접속정보 연결

                sUpInSQL = "    DECLARE @MACHCODE VARCHAR(10)    ";
                sUpInSQL += "    select @MACHCODE = MACHCODE      ";
                sUpInSQL += "    from TB_FCSMaster                ";
                sUpInSQL += $"   where MACHNAME = '{sFCN}'        ";
                sUpInSQL += "  UPDATE TB_BKMMT            ";
                sUpInSQL += "  SET REPCDATE    =  getdate()    ";
                sUpInSQL += "      ,FINISHFLAG = 'Y'  ";
                sUpInSQL += " WHERE FINISHFLAG = 'N'  ";
                sUpInSQL += " AND MACHCODE = @MACHCODE";
                sUpInSQL += " UPDATE TB_FCSMASTER              ";
                sUpInSQL += "  SET BROKENDOWN   = 'Y'  ";
                sUpInSQL += " WHERE MACHCODE  =  @MACHCODE     ";
                

                cmd.CommandText = sUpInSQL; // 3. 커맨드에 SQL 구문 등록.
                cmd.ExecuteNonQuery(); // 커맨드 실행.
                sTran.Commit();

                MessageBox.Show("정상적으로 등록을 완료 하였습니다.");

            }

            catch (Exception ex)
            {
                sTran.Rollback();

            }

            finally
            {
                sCon.Close();
            }
        }
    }
}

