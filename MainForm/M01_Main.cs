using Assemble;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ClassLibrary1
{
    public partial class M01_Main : Form
    {
        public M01_Main()
        {
            InitializeComponent();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            // 오픈 되어 있는 페이지가 없을 때는 닫기 기능 종료.
            //닫기 버튼 클릭시 활성화 되어 있는 페이지 닫기.
            if (myTabControl1.TabPages.Count > 0)
            {
                myTabControl1.SelectedTab.Dispose();
            }
        }

        private void M01_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 창의 x 버튼 클릭 또는 alt + F4 기능 실행, 종료 버튼 클릭 했을때 실행 이벤트.

            // 프로그램 종료 여부를 확인 하거나 
            // 실행 되고 있는 스레드를 안전하게 삭제 후 종료 할 수 있다.


            // 1. 프로그램 종료 확인 메세지.
            if (MessageBox.Show("프로그램을 종료 하시겠습니까?", "프로그램 종료", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
            
        }

        private void 사용자프로그램ToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        
            Assembly assembly = Assembly.LoadFrom($"{Application.StartupPath}\\FormList.DLL");
          
            Type typeform = assembly.GetType($"FormList.{e.ClickedItem.Name}", true);
          
            Form FormMdi = (Form)Activator.CreateInstance(typeform);

            bool b1 = false;
            for (int i = 0; i <= myTabControl1.TabPages.Count - 1; i++)
            {
               
                if (myTabControl1.TabPages[i].Name == e.ClickedItem.Name)
                {
                    myTabControl1.SelectedTab = myTabControl1.TabPages[i];
                    b1 = true;
                    break;
                }
            }
            if (b1 == false)
            {
                myTabControl1.AddForm(FormMdi);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnFunction_Click(object sender, EventArgs e)
        {
            ToolStripButton tsFuncting = (ToolStripButton)sender;
            DoFuncting(tsFuncting.Text);
        }

        void DoFuncting(string sStatue)
        {
            
            if (myTabControl1.TabPages.Count == 0) return;

      
            BaseChildForm Child = myTabControl1.SelectedTab.Controls[0] as BaseChildForm;
            if (Child == null) return;
       

            if (sStatue == "조회") Child.Dolnquire();
            else if (sStatue == "저장") Child.DoSave();
        }
    }
}
