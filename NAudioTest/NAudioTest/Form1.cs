using NAudio.Dsp;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NAudioTest
{
    public partial class Form1 : Form
    {
        private AudioMonitor mMonitor;
        private const int n = 4096;
        private List<float> srcSamples = new List<float>(n);

        public Form1()
        {
            InitializeComponent();

            // 启动音频监听
            mMonitor = new AudioMonitor(OnAudioCapture);
        }

        // 找出数组最大值下标
        private int MaxIndex<T>(T[] arr) where T : IComparable
        {
            int maxIndex = 0;
            T max = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i].CompareTo(max) > 0)
                {
                    max = arr[i];
                    maxIndex = i;
                }
            }
            return maxIndex;
        }

        // 滑动滤波（n是滑动窗口半侧大小）
        private double[] MovingFilter(double[] srcArr, int n)
        {
            double[] dstArr = new double[srcArr.Length];

            // 左侧平均（随便整几个数填充一下不为0就行）
            //for (int i = 0, w = 1; i < n; i++)
            //{
            //    double avg = 0;
            //    for (int j = 0; j < w; j++)
            //        avg += srcArr[j + i];
            //    avg /= w;
            //    dstArr[i] = srcArr[i];
            //    w = i*2 +1;
            //}

            // 中间段滑动平均
            int winLen = n * 2 + 1;
            for(int  i=0; i<=srcArr.Length- winLen; i++)
            {
                double avg = 0;
                for(int j=0; j< winLen; j++)
                    avg += srcArr[j+i];
                avg /= winLen;
                dstArr[i + n] = avg; 
            }

            // 右侧懒得搞

            return dstArr;
        }

        // 更新音频波形曲线图
        private void UpdateWaveChart(float[] d)
        {

            if (chart_Wave.InvokeRequired == false)
            {
                chart_Wave.Series[0].Points.Clear();
                for (int i = 0; i < d.Length; i++)
                {
                    chart_Wave.Series[0].Points.AddXY(i, d[i]);
                }
            }
            else
            {
                Action<float[]> action = UpdateWaveChart;
                this.Invoke(action, new object[] { d});
            }
        }

        double[] mKeepData = new double[4096 / 2];
        // 更新FFT曲线图
        private void UpdateFFTChart(double[] d, double baseFreq)
        {
            // 对傅里叶结果滑动滤波，窗口3
            var d2 = MovingFilter(d, 1);

            int minLen = Math.Min(mKeepData.Length, d.Length);
            for(int i=0; i<minLen; i++)
            {
                // 新值大于旧值马上赋值，否则等下落
                //if (d[i] > mKeepData[i])
                //    mKeepData[i] = d[i];

                // 上升和下降使用不同的速率
                if (d2[i] >= mKeepData[i])
                    mKeepData[i] += (d2[i] - mKeepData[i]) * 0.7;
                else
                    mKeepData[i] += (d2[i] - mKeepData[i]) * 0.25;
            }

            // 对绘制结果滑动滤波，窗口5
            var d3 = MovingFilter(mKeepData, 3);

            // 图表UI更新
            if (chart_FFT.InvokeRequired == false)
            {
                chart_FFT.Series[0].Points.Clear();
                for (int i = 0; i < minLen; i++)
                {
                    chart_FFT.Series[0].Points.AddXY(i * baseFreq, d2[i]);
                }

                chart_FFT.Series[1].Points.Clear();
                for (int i = 0; i < minLen; i++)
                {
                    chart_FFT.Series[1].Points.AddXY(i * baseFreq, d3[i]);
                }

                // 游标
                //chart_FFT.ChartAreas[0].CursorX.Interval = baseFreq;    // 游标步进距以基频为单位
                //UpdateCursorLabel();

                // 显示参数
                ////Console.WriteLine("{0}, {1}; ", d.Length, baseFreq);
                //int maxFreqIndex = MaxIndex(mKeepData);
                ////Console.WriteLine("最大值：{0}, {1}; ", maxFreqIndex * baseFreq, d[maxFreqIndex]);
                //label_Info.Text = String.Format("最大值：频率{0:N3}, 幅度{1:N3}; ", maxFreqIndex * baseFreq, mKeepData[maxFreqIndex])
                //    + String.Format("FFT参数：点数{0}, 基频{1};", mKeepData.Length, baseFreq);
            }
            else
            {
                // 若在其他线程调用，委托到主线程更新UI
                Action<double[], double> action = UpdateFFTChart;
                this.BeginInvoke(action, new object[] {d, baseFreq});
            }
        }

        // 光标信息文本
        private void UpdateCursorLabel()
        {
            // 游标
            var points = chart_FFT.Series["Series1"].Points;
            double xPos = chart_FFT.ChartAreas[0].CursorX.Position; // x轴的值
            if (!Double.IsNaN(xPos))
            {
                double xPosMax = points.Last().XValue;  // x轴最大值
                // 限定光标必须在当前图表x坐标内，否则UI会卡死
                if(xPos < 0) xPos = 0;
                else if(xPos > xPosMax) xPos = xPosMax;

                int index = (int)(xPos / xPosMax * (points.Count - 1)); // 换算出所在下标
                double yPos = points[index].YValues[0]; // 获取出y的值
                label_CursorInfo.Text = String.Format("光标：{0:N3}, {1:N3};", points[index].XValue, yPos);
            }
        }

        // 音频捕获回调
        private int OnAudioCapture(float[] allSamples)
        {
            var channelSamples = mMonitor.SplitChannels(allSamples);
            var averageSamples = mMonitor.AverageChannels(channelSamples);
            srcSamples.AddRange(averageSamples);

            if (srcSamples.Count >= n)   // 到达一定的采样量后再处理一次
            {
                var srcSamples2 = srcSamples.Take(n).ToArray();
                srcSamples.Clear();

                if (mIsPause == false)
                {
                    // 显示音频波形
                    UpdateWaveChart(srcSamples2);

                    double baseFreq;
                    var fftResult = mMonitor.FFT(srcSamples2, out baseFreq);
                    double[] fftAbs = fftResult.Take(fftResult.Length / 2)
                    .Select(c => Math.Sqrt(c.X * c.X + c.Y * c.Y))
                    .ToArray();

                    // 计算2500Hz频率在数组的下标
                    int index_2k5Hz = (int)(2500 / baseFreq);
                    UpdateFFTChart(fftAbs.Take(index_2k5Hz).ToArray(), baseFreq);
                }
            }

            return 0;
        }

        #region UI回调
        bool mIsPause = false;
        private void SetPauseFlag(bool b)
        {
            mIsPause = b;
            btn_Pause.Text = mIsPause ? "继续" : "暂停";
        }

        private void btn_Pause_Click(object sender, EventArgs e)
        {
            SetPauseFlag(!mIsPause);
        }

        private void chart_FFT_MouseClick(object sender, MouseEventArgs e)
        {
            UpdateCursorLabel();
        }

        #endregion

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            chart_FFT.ChartAreas[0].AxisY.Maximum = trackBar1.Value;
        }
    }
}
