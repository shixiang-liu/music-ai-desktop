using Microsoft.Reporting.WinForms;
using MusicAI.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Controls
{
    public partial class ReportPanel : UserControl
    {
        public IReportService ReportService { get; set; }

        public ReportPanel()
        {
            InitializeComponent();
        }

        public void LoadReport()
        {
            if (ReportService == null) return;
            try
            {
                var reportData = ReportService.GenerateUserSongUploadReport();
                this.reportViewer1.LocalReport.DataSources.Clear();
                var rds = new ReportDataSource("DataSet1", reportData);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "UI.UserSongUploadReport.rdlc";
                this.reportViewer1.LocalReport.DataSources.Add(rds);
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载报表失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
