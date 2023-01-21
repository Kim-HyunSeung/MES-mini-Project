using Assemble;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Assemble;

namespace FormList
{
    public partial class Form04_Chart : Assemble.BaseChildForm
    {
        public Form04_Chart()
        {
            InitializeComponent();
        }

        private void btnSerach_Click(object sender, EventArgs e)
        {
            
            SetCulomnChart();

        }
        void SetCulomnChart()
        {
            string sItemCode = CombCode.Text;
            string sdtpStart = dtpStart.Text;
            string sdtpEnd = dtpEnd.Text;
            DBHelper helper = new DBHelper();

            // 데이터베이스에 접속 해서 일자별 품목 합한 수량을 차트로 나타내는 로직. 
            SqlDataAdapter Adapter = new SqlDataAdapter("SP_CMTTR", helper.sCon);

            // Adapter 에게 저장 프로시져 형식의 SQL 을 실행할것을 등록함.
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            // 저장 프로시저가 받을 파라매터(인자) 값 설정.
            Adapter.SelectCommand.Parameters.AddWithValue("@MACHCODE", sItemCode);
            Adapter.SelectCommand.Parameters.AddWithValue("@STARTDATE", sdtpStart);
            Adapter.SelectCommand.Parameters.AddWithValue("@ENDDATE", sdtpEnd);

            // 기본적으로 모든 프로시져에 적용될 내용.


            DataTable dtTemp = new DataTable();
            Adapter.Fill(dtTemp);


            // 1. 차트의 초기화
            // Series : 차트를 표현 하는 연속된 데이터의 모음.
            chaMTBF.Series.Clear();


            if (dtTemp.Rows.Count == 0) return;

            // 차트에 표현



            // 2. 조회된 DataTable 의 가장 큰 생산수량 을 Y 축 에 셋팅.
            //int iMaxQty = 0;
            //for (int i = 0; i < dtTemp.Rows.Count; i++)
            //{
            //    if (Convert.ToInt32(dtTemp.Rows[i]["PRODQTY"]) > iMaxQty)
            //    {
            //        iMaxQty = Convert.ToInt32(dtTemp.Rows[i]["PRODQTY"]);
            //    }
            //}
            //iMaxQty: 최대수량

            //  DataRow[] dr = dtTemp.Select("REPAIRDATE = MAX(REPAIRDATE)");

            chaMTBF.ChartAreas[0].AxisY.Minimum = 0;

            DataRow[] drRows = dtTemp.Select("MINDIFFAVG = max(MINDIFFAVG)");
            chaMTBF.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(drRows[0]["MINDIFFAVG"]) + 500;


            // 3. 데이터 테이블을 차트에 바인딩 (매핑)
            chaMTBF.DataBindTable(dtTemp.DefaultView, "REPAIRDATE");

            // 4.  막대 차트로 표현해야 하는 데이터 의 이름 과 설정 정보 등록.
            chaMTBF.Series[0].Name = Convert.ToString(dtTemp.Rows[0]["MACHCODE"]); // 표현해야 할 데이터의 이름.
            chaMTBF.Series[0].Color = Color.Green;         // 표현될 차트의 색상. 
            chaMTBF.Series[0].IsValueShownAsLabel = true;  // 컬럼 차트 위에 수량을 숫자료 표기.
        }

        private void Form04_Chart_Load(object sender, EventArgs e)
        {
            SqlConnection Connect = new SqlConnection(Common.sConn);

            try
            {
                Connect.Open();

                string sSqlSelect = "  SELECT MACHCODE   ";
                sSqlSelect       += "   FROM TB_FCSMaster  ";
               

                SqlDataAdapter Adapter = new SqlDataAdapter(sSqlSelect, Connect);
                DataTable dtTemp1 = new DataTable();
                Adapter.Fill(dtTemp1);

                CombCode.DataSource = dtTemp1;
                  // 로직상 처리될 코드가 있는 컬럼.
                CombCode.DisplayMember = "MACHCODE";// 사용자에게 보여줄 컬럼.

                CombCode2.DataSource = dtTemp1;
                CombCode2.DisplayMember = "MACHCODE";


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

        private void button1_Click(object sender, EventArgs e)
        {
            SetCulomnChart1();
        }

         void SetCulomnChart1()
        {
            string sItemCode = CombCode2.Text;
            string sdtpStart = dtpStart1.Text;
            string sdtpEnd = dtpEnd1.Text;
            DBHelper helper = new DBHelper();

            // 데이터베이스에 접속 해서 일자별 품목 합한 수량을 차트로 나타내는 로직. 
            SqlDataAdapter Adapter = new SqlDataAdapter("SP_CPMTBF2", helper.sCon);

            // Adapter 에게 저장 프로시져 형식의 SQL 을 실행할것을 등록함.
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            // 저장 프로시저가 받을 파라매터(인자) 값 설정.
            Adapter.SelectCommand.Parameters.AddWithValue("@MACHCODE", sItemCode);
            Adapter.SelectCommand.Parameters.AddWithValue("@STARTDATE", sdtpStart);
            Adapter.SelectCommand.Parameters.AddWithValue("@ENDDATE", sdtpEnd);

            // 기본적으로 모든 프로시져에 적용될 내용.


            DataTable dtTemp = new DataTable();
            Adapter.Fill(dtTemp);


            // 1. 차트의 초기화
            // Series : 차트를 표현 하는 연속된 데이터의 모음.
            chaMTTP.Series.Clear();


            if (dtTemp.Rows.Count == 0) return;

            // 차트에 표현



            // 2. 조회된 DataTable 의 가장 큰 생산수량 을 Y 축 에 셋팅.
            //int iMaxQty = 0;
            //for (int i = 0; i < dtTemp.Rows.Count; i++)
            //{
            //    if (Convert.ToInt32(dtTemp.Rows[i]["PRODQTY"]) > iMaxQty)
            //    {
            //        iMaxQty = Convert.ToInt32(dtTemp.Rows[i]["PRODQTY"]);
            //    }
            //}
            //iMaxQty: 최대수량

            //  DataRow[] dr = dtTemp.Select("REPAIRDATE = MAX(REPAIRDATE)");

            chaMTTP.ChartAreas[0].AxisY.Minimum = 0;
            DataRow[] drRows = dtTemp.Select("BROKENDAYAVG = max(BROKENDAYAVG)");
            chaMTTP.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(drRows[0]["BROKENDAYAVG"]) + 20;


            // 3. 데이터 테이블을 차트에 바인딩 (매핑)
            chaMTTP.DataBindTable(dtTemp.DefaultView, "BROKENMONTH");

            // 4.  막대 차트로 표현해야 하는 데이터 의 이름 과 설정 정보 등록.
            chaMTTP.Series[0].Name = CombCode2.Text; // 표현해야 할 데이터의 이름.
            chaMTTP.Series[0].Color = Color.Green;         // 표현될 차트의 색상. 
            chaMTTP.Series[0].IsValueShownAsLabel = true;  // 컬럼 차트 위에 수량을 숫자료 표기.
        }

    }

}

