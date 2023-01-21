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
    public partial class Form02_broken_management : BaseChildForm
    {
        public Form02_broken_management()
        {
            InitializeComponent();
        }

        private void Form2_broken_management_Load(object sender, EventArgs e)
        {
            // 1. 데이터 테이블 생성(그리드에 표현될 컬럼을 셋팅).
            DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("BEOKENSEQ", typeof(string)); // 고장 순번
            dtGrid.Columns.Add("MACHCODE", typeof(string)); // 설비 코드
            dtGrid.Columns.Add("MACHNAME", typeof(string)); // 설비 명
            dtGrid.Columns.Add("WKNAME", typeof(string)); // 고장등록자
            dtGrid.Columns.Add("BORKENDATE", typeof(string)); // 고장 발생 일시
            dtGrid.Columns.Add("REPDATE", typeof(string)); // 수리자 도착 일시
            dtGrid.Columns.Add("REPNAME", typeof(string)); // 수리자 명
            dtGrid.Columns.Add("REPCDATE", typeof(string)); // 수리자 완료시간
            dtGrid.Columns.Add("FINISHFLAG", typeof(string)); // 수리 완료 여부
            dtGrid.Columns.Add("FREASON", typeof(string)); // 고장사유


            // 2. 셋팅된 컬럼을 그리드에 매핑
            // DataSource : 데이터 베이스에서 가져온 테이블 형식의 데이터를 등록 할때 사용
            dgvGrid.DataSource = dtGrid;

            // 3. 그리드 컬럼에 한글 명칭으로 컬럼 Text 보여주기
            dgvGrid.Columns["BEOKENSEQ"].HeaderText = "고장순번";
            dgvGrid.Columns["MACHCODE"].HeaderText = "설비 코드";
            dgvGrid.Columns["MACHNAME"].HeaderText = "설비 명";
            dgvGrid.Columns["WKNAME"].HeaderText = "고장등록자";
            dgvGrid.Columns["BORKENDATE"].HeaderText = "고장 발생 일시";
            dgvGrid.Columns["REPDATE"].HeaderText = "수리자 도착 일시";
            dgvGrid.Columns["REPNAME"].HeaderText = "수리자 명";
            dgvGrid.Columns["REPCDATE"].HeaderText = "수리자 완료시간";
            dgvGrid.Columns["FINISHFLAG"].HeaderText = "수리완료여부 ";
            dgvGrid.Columns["FREASON"].HeaderText = "고장사유 ";


            dgvGrid.Columns["BEOKENSEQ"].Visible = false; // 컬럼을 숨김
            // 4. 고장 사유만 수정 가능하게 변경

            dgvGrid.Columns["MACHCODE"].ReadOnly = true;
            dgvGrid.Columns["MACHNAME"].ReadOnly = true;
            dgvGrid.Columns["WKNAME"].ReadOnly = true;
            dgvGrid.Columns["BORKENDATE"].ReadOnly = true;
            dgvGrid.Columns["REPDATE"].ReadOnly = true;
            dgvGrid.Columns["REPNAME"].ReadOnly = true;
            dgvGrid.Columns["REPCDATE"].ReadOnly = true;

        }

        public override void Dolnquire()
        {
            // base.Dolnquire(); 이게 있으면 전에 있던거 상속 받는다.
            //조회 버튼 클릭시 사용자 정보 조회.
            string sFCC = TxtMachCode.Text;

            // 데이터 베이스 오픈.
            DBHelper helper = new DBHelper();

            

            try
            {
                SqlDataAdapter Adapter = new SqlDataAdapter("SP_BKMaster_S1", helper.sCon);



                string sStartDate = DtpStart.Text;          // 고장 일자 시작 일자.
                string sEndDate = DtpEnd.Text;              // 고장 일자   끝 일자.

                string sEndFlag = "N";
                if (rdoProd.Checked == true) sEndFlag = "Y"; // 라디오 정상 및 고장 버튼


                // Adapter 에게 저장 프로시져 형식의 sql 을 실행 할것을 등록함.
                Adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                // 저장 프로시저가 받을 파라매터(인자) 값 결정
                Adapter.SelectCommand.Parameters.AddWithValue("@MACHCODE", sFCC);
                Adapter.SelectCommand.Parameters.AddWithValue("@STARTDATE", sStartDate);
                Adapter.SelectCommand.Parameters.AddWithValue("@ENDDATE", sEndDate);
                Adapter.SelectCommand.Parameters.AddWithValue("@FINISHFLAG", sEndFlag);


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
                            cmd.CommandText = "SP_BKMasterD2";

                            cmd.Parameters.AddWithValue("@MACHCODE", Convert.ToString(dr["MACHCODE"]));
                            cmd.Parameters.AddWithValue("@STARTDATE", Convert.ToString(dr["STARTDATE"]));
                            cmd.Parameters.AddWithValue("@LANG", "KO");
                            cmd.Parameters.AddWithValue("@RS_CODE", "").Direction = ParameterDirection.Output;
                            cmd.Parameters.AddWithValue("@RS_MSG", "").Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            break;
                        case DataRowState.Added:
                            // 지금 추출한 행의 상태가 신규 추가 상태 라면
                            // 1. 필수 입력 데이터 확인.

                            if (Convert.ToString(dr["MACHCODE"]) == "") sMessage = "설비코드";
                            else if (Convert.ToString(dr["MACHNAME"]) == "") sMessage = "설비명";
                            else if (Convert.ToString(dr["WKNAME"]) == "") sMessage = "고장등록자";
                            else if (Convert.ToString(dr["BORKENDATE"]) == "") sMessage = "고장발생일시";
                            else if (Convert.ToString(dr["REPDATE"]) == "") sMessage = "수리자도착일시";
                            else if (Convert.ToString(dr["REPNAME"]) == "") sMessage = "수리자명";
                            else if (Convert.ToString(dr["REPCDATE"]) == "") sMessage = "수리자완료시간";
                            else if (Convert.ToString(dr["FINISHFLAG"]) == "") sMessage = "수리완료여부";
                            else if (Convert.ToString(dr["FREASON"]) == "") sMessage = "고장사유";

                            if (sMessage != "")
                            {
                                MessageBox.Show(sMessage + "를 입력하지 않았습니다.");
                                throw new Exception();
                            }

                            // 2. 사용자 등록 로직 구현.
                            cmd.CommandText = "SP_BKMaster_I2";
                            cmd.Parameters.AddWithValue("@MACHCODE", Convert.ToString(dr["MACHCODE"]));
                            cmd.Parameters.AddWithValue("@MACHNAME", Convert.ToString(dr["MACHNAME"]));
                            cmd.Parameters.AddWithValue("@WKNAME", Convert.ToString(dr["WKNAME"]));
                            cmd.Parameters.AddWithValue("@BROKENDOWN", Convert.ToString(dr["BROKENDOWN"]));
                            cmd.Parameters.AddWithValue("@REPDATE", Convert.ToString(dr["REPDATE"]));
                            cmd.Parameters.AddWithValue("@REPNAME", Convert.ToString(dr["REPNAME"]));
                            cmd.Parameters.AddWithValue("@REPCDATE", Convert.ToString(dr["REPCDATE"]));
                            cmd.Parameters.AddWithValue("@FINISHFLAG", Convert.ToString(dr["FINISHFLAG"]));
                            cmd.Parameters.AddWithValue("@FREASON", Convert.ToString(dr["FREASON"]));


                            cmd.Parameters.AddWithValue("@LANG", "KO");
                            cmd.Parameters.AddWithValue("@RS_CODE", "").Direction = ParameterDirection.Output;
                            cmd.Parameters.AddWithValue("@RS_MSG", "").Direction = ParameterDirection.Output;

                            // 커맨드의 실행.
                            cmd.ExecuteNonQuery();


                            break;
                        case DataRowState.Modified:
                            // 지금 추출한 행의 상태가 수정 상태라면


                            // 1. 필수 입력 데이터 확인.

                            if (Convert.ToString(dr["MACHCODE"]) == "") sMessage = "설비코드";
                            else if (Convert.ToString(dr["MACHNAME"]) == "") sMessage = "설비명";
                            else if (Convert.ToString(dr["WKNAME"]) == "") sMessage = "고장등록자";
                            else if (Convert.ToString(dr["BORKENDATE"]) == "") sMessage = "고장발생일시";
                            else if (Convert.ToString(dr["REPDATE"]) == "") sMessage = "수리자도착일시";
                            else if (Convert.ToString(dr["REPNAME"]) == "") sMessage = "수리자명";
                            else if (Convert.ToString(dr["REPCDATE"]) == "") sMessage = "수리자완료시간";
                            else if (Convert.ToString(dr["FINISHFLAG"]) == "") sMessage = "수리완료여부";
                            else if (Convert.ToString(dr["FREASON"]) == "") sMessage = "고장사유";
                            if (sMessage != "")
                            {
                                throw new Exception(sMessage + " 를 입력하지 않았습니다.");
                            }

                            // 2. 사용자 정보수정 로직 구현.
                            cmd.CommandText = "SP_BKMaster_U2";
                            cmd.Parameters.AddWithValue("@BEOKENSEQ", Convert.ToString(dr["BEOKENSEQ"]));
                            //cmd.Parameters.AddWithValue("@MACHCODE", Convert.ToString(dr["MACHCODE"]));
                            //cmd.Parameters.AddWithValue("@BORKENDATE", Convert.ToString(dr["BORKENDATE"]));
                            cmd.Parameters.AddWithValue("@FREASON", Convert.ToString(dr["FREASON"]));
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