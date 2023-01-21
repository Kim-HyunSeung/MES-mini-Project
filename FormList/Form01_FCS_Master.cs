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
using Assemble;


namespace FormList
{
    public partial class Form01_FCS_Master : BaseChildForm
    {
        public Form01_FCS_Master()
        {
            InitializeComponent();
        }

        private void Form01_FCS_Master_Load(object sender, EventArgs e)
        {
            // DataTable : 클래스 = 참조형
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

            
            SqlConnection Connect = new SqlConnection(Common.sConn);

            dgvGrid.Columns["BROKENDOWN"].ReadOnly = true;

            try
            {
                Connect.Open();

                string sSqlSelect = " SELECT '' AS MACHCODE,'ALL' AS MACHNAME            ";
                sSqlSelect       += " UNION ALL                                          ";
                sSqlSelect       += " SELECT USEFLAG  AS MACHID  , USEFLAG AS MACHNAME   ";
                sSqlSelect       += " FROM TB_FCSMaster                                  ";
                sSqlSelect       += " GROUP BY USEFLAG                                   ";


                SqlDataAdapter Adapter = new SqlDataAdapter(sSqlSelect, Connect);
                DataTable dtTemp1 = new DataTable();
                Adapter.Fill(dtTemp1);

                cboF.DataSource = dtTemp1;
                cboF.ValueMember = "MACHCODE";  // 로직상 처리될 코드가 있는 컬럼.
                cboF.DisplayMember = "MACHNAME";// 사용자에게 보여줄 컬럼.

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            finally
            {
                Connect.Close();
            }

        }

        public override void Dolnquire()
        {
            // base.Dolnquire(); 이게 있으면 전에 있던거 상속 받는다.
            //조회 버튼 클릭시 사용자 정보 조회.
            string sFCC = txtFCC.Text;
            string sFN = txtFN.Text;
            string sUSEF = Convert.ToString(cboF.SelectedValue);

            // 데이터 베이스 오픈.
            DBHelper helper = new DBHelper();
            try
            {
                SqlDataAdapter Adapter = new SqlDataAdapter("SP_FCSMaster_S1", helper.sCon);

                // Adapter 에게 저장 프로시져 형식의 sql 을 실행 할것을 등록함.
                Adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                // 저장 프로시저가 받을 파라매터(인자) 값 결정
                Adapter.SelectCommand.Parameters.AddWithValue("@MACHCODE", sFCC);
                Adapter.SelectCommand.Parameters.AddWithValue("@MACHNAME", sFN);
                Adapter.SelectCommand.Parameters.AddWithValue("@USEFLAG", sUSEF);

                // 기본적으로 모든 프로시져에 적용 될 내용.
                Adapter.SelectCommand.Parameters.AddWithValue("@LANG", "KO");
                Adapter.SelectCommand.Parameters.AddWithValue("@RS_CODE", "").Direction = ParameterDirection.Output;
                Adapter.SelectCommand.Parameters.AddWithValue("@RS_MSG", "").Direction = ParameterDirection.Output;

                DataTable dtTemp = new DataTable();
                Adapter.Fill(dtTemp);

                dgvGrid.DataSource = dtTemp;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                helper.Close();
            }
        }

        public override void DoSave()
        {
            // 표 형식의 데이터 (DataGridView) 의 데이터를 등록하는 방법으로 주로 사용 되는 방법.
            // 그리드에 표현된 데이터를 일괄 추가, 수정, 삭제, 로직 적용.

            // 1. 데이터 베이스 Open
            DBHelper helper = new DBHelper();
            // 3. 일괄 승인 및 일괄 복구 트랜잭션 설정.
            SqlTransaction sTran = helper.sCon.BeginTransaction();
            try
            {
                // 2. 데이터베이스 갱신 명령 전달 클래스 객체 생성.
                SqlCommand cmd = new SqlCommand();

                // 4. 트랜잭션 커맨드에 연결
                cmd.Transaction = sTran;
                // 5. 커맨드에 접속 정보 연결.
                cmd.Connection = helper.sCon;

                // 6 . 커맨드에 저장 프로시저 형식 선언
                cmd.CommandType = CommandType.StoredProcedure;
                // 6. 그리드의 행 중에 갱신 이력이 있는 행만 추출 하여 DataTable 에 담기.


                dgvGrid.Update(); // 그리드에 갱신 된 데이터 확정.
                DataTable dtTemp = ((DataTable)dgvGrid.DataSource).GetChanges();

                string sMessage = string.Empty;
                // 7. 갱신된 행만 추출한 데이터 테이블의 행 수만큼 반복하여
                // update, insert, delete 분기 짓기 
                foreach (DataRow dr in dtTemp.Rows)
                {
                    // 데이터 테이블에서 추출 한 행의 상태 비교. 
                    switch (dr.RowState)
                    {
                        case DataRowState.Deleted:
                            dr.RejectChanges();
                            cmd.CommandText = "SP_FCSMaster_D2";

                            cmd.Parameters.AddWithValue("@MACHCODE", Convert.ToString(dr["MACHCODE"]));
                            cmd.Parameters.AddWithValue("@LANG", "KO");
                            cmd.Parameters.AddWithValue("@RS_CODE", "").Direction = ParameterDirection.Output;
                            cmd.Parameters.AddWithValue("@RS_MSG", "").Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            break;
                        case DataRowState.Added:
                            // 지금 추출한 행의 상태가 신규 추가 상태 라면
                            // 1. 필수 입력 데이터 확인.

                            if (Convert.ToString(dr["MACHCODE"]) == "") sMessage = "설비코드";
                            else if (Convert.ToString(dr["USEFLAG"]) == "") sMessage = "설비사용여부";

                            if (sMessage != "")
                            {
                                MessageBox.Show(sMessage + "를 입력하지 않았습니다.");
                                throw new Exception();
                            }

                            // 2. 사용자 등록 로직 구현.
                            cmd.CommandText = "SP_FCSMaster_I2";
                            cmd.Parameters.AddWithValue("@MACHCODE", Convert.ToString(dr["MACHCODE"]));
                            cmd.Parameters.AddWithValue("@MACHNAME", Convert.ToString(dr["MACHNAME"]));
                            cmd.Parameters.AddWithValue("@USEFLAG", Convert.ToString(dr["USEFLAG"]));
                            cmd.Parameters.AddWithValue("@BROKENDOWN", Convert.ToString(dr["BROKENDOWN"]));


                            cmd.Parameters.AddWithValue("@LANG", "KO");
                            cmd.Parameters.AddWithValue("@RS_CODE", "").Direction = ParameterDirection.Output;
                            cmd.Parameters.AddWithValue("@RS_MSG", "").Direction = ParameterDirection.Output;

                            // 커맨드의 실행.
                            cmd.ExecuteNonQuery();


                            break;
                        case DataRowState.Modified:
                            // 지금 추출한 행의 상태가 수정 상태라면


                            // 1. 필수 입력 데이터 확인.

                            if (Convert.ToString(dr["USEFLAG"]) == "") sMessage = "설비 사용 여부";
                            else if (sMessage != "")
                            {
                                throw new Exception(sMessage + " 를 입력하지 않았습니다.");
                            }

                            // 2. 사용자 정보수정 로직 구현.
                            cmd.CommandText = "SP_FCSMaster_U1";
                            cmd.Parameters.AddWithValue("@MACHCODE", Convert.ToString(dr["MACHCODE"]));
                            cmd.Parameters.AddWithValue("@USEFLAG", Convert.ToString(dr["USEFLAG"]));

                            cmd.Parameters.AddWithValue("@LANG", "KO");
                            cmd.Parameters.AddWithValue("@RS_CODE", "").Direction = ParameterDirection.Output;
                            cmd.Parameters.AddWithValue("@RS_MSG", "").Direction = ParameterDirection.Output;

                            // 커맨드의 실행.
                            cmd.ExecuteNonQuery();

                            break;
                    }

                    string sRs_Code = Convert.ToString(cmd.Parameters["@RS_CODE"].Value);
                    string sRs_Msg = Convert.ToString(cmd.Parameters["@RS_Msg"].Value);

                    if (sRs_Code != "S")
                    {
                        sTran.Rollback();
                        MessageBox.Show(sRs_Msg);
                        return;
                    }
                    // 처리한 커랜드의 파라매터(인자) 정보 삭제
                    // 다음 커맨드에서 변수를 지정 할 수 있도록 초기화 함.
                    cmd.Parameters.Clear();
                }


                // INSERT


                // UPDATE


                // DELETE

                sTran.Commit();
                MessageBox.Show("정상적으로 데이터를 등록 하였습니다.");
                Dolnquire();
            }

            catch (Exception ex)
            {
                sTran.Rollback();
                MessageBox.Show("데이터 등록에 실패 하였습니다.");
            }

            finally
            {
                helper.Close();
            }

           }

       
    }
     }


