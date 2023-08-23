namespace NAudioTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend10 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series15 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart_FFT = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_Wave = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_Pause = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_CursorInfo = new System.Windows.Forms.Label();
            this.label_Info = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.chart_FFT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Wave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_FFT
            // 
            chartArea9.AxisX.Minimum = 0D;
            chartArea9.AxisY.Maximum = 150D;
            chartArea9.AxisY.Minimum = 0D;
            chartArea9.CursorX.IsUserEnabled = true;
            chartArea9.CursorX.IsUserSelectionEnabled = true;
            chartArea9.InnerPlotPosition.Auto = false;
            chartArea9.InnerPlotPosition.Height = 86.36968F;
            chartArea9.InnerPlotPosition.Width = 92.43568F;
            chartArea9.InnerPlotPosition.X = 6.29306F;
            chartArea9.InnerPlotPosition.Y = 3.35106F;
            chartArea9.Name = "ChartArea1";
            this.chart_FFT.ChartAreas.Add(chartArea9);
            this.chart_FFT.Dock = System.Windows.Forms.DockStyle.Fill;
            legend9.Name = "Legend1";
            this.chart_FFT.Legends.Add(legend9);
            this.chart_FFT.Location = new System.Drawing.Point(0, 3);
            this.chart_FFT.Name = "chart_FFT";
            this.chart_FFT.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series13.ChartArea = "ChartArea1";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series13.Legend = "Legend1";
            series13.Name = "Series1";
            series13.YValuesPerPoint = 4;
            series14.ChartArea = "ChartArea1";
            series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series14.Legend = "Legend1";
            series14.Name = "Series2";
            this.chart_FFT.Series.Add(series13);
            this.chart_FFT.Series.Add(series14);
            this.chart_FFT.Size = new System.Drawing.Size(1048, 331);
            this.chart_FFT.TabIndex = 0;
            this.chart_FFT.Text = "chart1";
            this.chart_FFT.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart_FFT_MouseClick);
            // 
            // chart_Wave
            // 
            chartArea10.AxisX.Minimum = 0D;
            chartArea10.InnerPlotPosition.Auto = false;
            chartArea10.InnerPlotPosition.Height = 82.18086F;
            chartArea10.InnerPlotPosition.Width = 93.75224F;
            chartArea10.InnerPlotPosition.X = 5.19775F;
            chartArea10.InnerPlotPosition.Y = 4.46808F;
            chartArea10.Name = "ChartArea1";
            chartArea10.Position.Auto = false;
            chartArea10.Position.Height = 94F;
            chartArea10.Position.Width = 98F;
            chartArea10.Position.Y = 3F;
            this.chart_Wave.ChartAreas.Add(chartArea10);
            this.chart_Wave.Dock = System.Windows.Forms.DockStyle.Fill;
            legend10.Enabled = false;
            legend10.Name = "Legend1";
            this.chart_Wave.Legends.Add(legend10);
            this.chart_Wave.Location = new System.Drawing.Point(0, 0);
            this.chart_Wave.Name = "chart_Wave";
            series15.ChartArea = "ChartArea1";
            series15.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series15.Legend = "Legend1";
            series15.Name = "Series1";
            this.chart_Wave.Series.Add(series15);
            this.chart_Wave.Size = new System.Drawing.Size(1048, 219);
            this.chart_Wave.TabIndex = 1;
            this.chart_Wave.Text = "chart1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(5, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chart_Wave);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.trackBar1);
            this.splitContainer1.Panel2.Controls.Add(this.btn_Pause);
            this.splitContainer1.Panel2.Controls.Add(this.chart_FFT);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.splitContainer1.Size = new System.Drawing.Size(1048, 560);
            this.splitContainer1.SplitterDistance = 222;
            this.splitContainer1.TabIndex = 2;
            // 
            // btn_Pause
            // 
            this.btn_Pause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Pause.Location = new System.Drawing.Point(935, 62);
            this.btn_Pause.Name = "btn_Pause";
            this.btn_Pause.Size = new System.Drawing.Size(75, 24);
            this.btn_Pause.TabIndex = 1;
            this.btn_Pause.Text = "暂停";
            this.btn_Pause.UseVisualStyleBackColor = true;
            this.btn_Pause.Click += new System.EventHandler(this.btn_Pause_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_CursorInfo);
            this.panel1.Controls.Add(this.label_Info);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(5, 570);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1048, 24);
            this.panel1.TabIndex = 3;
            // 
            // label_CursorInfo
            // 
            this.label_CursorInfo.AutoSize = true;
            this.label_CursorInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.label_CursorInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_CursorInfo.Location = new System.Drawing.Point(985, 0);
            this.label_CursorInfo.Name = "label_CursorInfo";
            this.label_CursorInfo.Size = new System.Drawing.Size(63, 16);
            this.label_CursorInfo.TabIndex = 4;
            this.label_CursorInfo.Text = "光标：;";
            // 
            // label_Info
            // 
            this.label_Info.AutoSize = true;
            this.label_Info.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_Info.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Info.Location = new System.Drawing.Point(0, 0);
            this.label_Info.Name = "label_Info";
            this.label_Info.Size = new System.Drawing.Size(55, 16);
            this.label_Info.TabIndex = 0;
            this.label_Info.Text = "label1";
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Location = new System.Drawing.Point(951, 103);
            this.trackBar1.Maximum = 200;
            this.trackBar1.Minimum = 10;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 196);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 120;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 599);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "监听音频FFT";
            ((System.ComponentModel.ISupportInitialize)(this.chart_FFT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Wave)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_FFT;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Wave;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_Info;
        private System.Windows.Forms.Label label_CursorInfo;
        private System.Windows.Forms.Button btn_Pause;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}

