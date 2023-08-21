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
        public Form1()
        {
            InitializeComponent();

            StartRecording();

        }

        // 更新音频波形曲线图
        public void UpdateWaveChart(float[] d)
        {

            if (chart_Wave.InvokeRequired == false)
            {
                chart_Wave.Series["Series1"].Points.Clear();
                for (int i = 0; i < d.Length; i++)
                {
                    chart_Wave.Series["Series1"].Points.AddXY(i, d[i]);
                }
            }
            else
            {
                Action<float[]> action = UpdateWaveChart;
                this.Invoke(action, new object[] { d});
            }
        }

        // 更新FFT曲线图
        public void UpdateFFTChart(double[] d, double baseFreq)
        {

            if(chart_FFT.InvokeRequired == false)
            {
                chart_FFT.Series["Series1"].Points.Clear();
                for(int i = 0; i < d.Length; i++) 
                {
                    chart_FFT.Series["Series1"].Points.AddXY(i* baseFreq, d[i]);            
                }

                Console.WriteLine("{0}, {1}; ", d.Length, baseFreq);
                int maxFreqIndex = MaxIndex(d);
                Console.WriteLine("最大值：{0}, {1}; ", maxFreqIndex * baseFreq, d[maxFreqIndex]);
                label_Info.Text = String.Format("最大值：频率{0:N3}, 幅度{1:N3}; ", maxFreqIndex * baseFreq, d[maxFreqIndex])
                    + String.Format("FFT参数：点数{0}, 基频{1};", d.Length, baseFreq);
            }
            else
            {
                Action<double[], double> action = UpdateFFTChart;
                this.Invoke(action, new object[] {d, baseFreq});
            }
        }



        // 开始录制输出音频
        private void StartRecording()
        {
            WasapiLoopbackCapture cap = new WasapiLoopbackCapture();
            cap.DataAvailable += (sender, e) =>      // 录制数据可用时触发此事件, 参数中包含音频数据
            {
                float[] allSamples = Enumerable      // 提取数据中的采样
                    .Range(0, e.BytesRecorded / 4)   // 除以四是因为, 缓冲区内每 4 个字节构成一个浮点数, 一个浮点数是一个采样
                    .Select(i => BitConverter.ToSingle(e.Buffer, i * 4))  // 转换为 float
                    .ToArray();    // 转换为数组

                // 获取采样后, 在这里进行详细处理
                var channelSamples = SplitChannels(allSamples, cap.WaveFormat.Channels);
                var averageSamples = AverageChannels(channelSamples, cap.WaveFormat.Channels);
                var fftResult = FFT(averageSamples);

                double[] fftAbs = Enumerable.Range(0, fftResult.Length / 2)
                .Select(i => {
                    Complex c = fftResult[i]; 
                    return Math.Sqrt(c.X * c.X + c.Y * c.Y); 
                })
                .ToArray();

                // 计算FFT[1]的基础频率
                double baseFreq = (double)cap.WaveFormat.SampleRate / fftResult.Length;

                UpdateWaveChart(averageSamples);
                UpdateFFTChart(fftAbs, baseFreq);
            };
            cap.StartRecording();   // 开始录制

        }

        // 分离声道
        private float[][] SplitChannels(float[] allSamples, int channelCount)
        {
            // 设定我们已经将刚刚的采样保存到了变量 AllSamples 中, 类型为 float[]
            // int channelCount = cap.WaveFormat.Channels;   // WasapiLoopbackCapture 的 WaveFormat 指定了当前声音的波形格式, 其中包含就通道数
            float[][] channelSamples = Enumerable
                .Range(0, channelCount)
                .Select(channel => Enumerable
                    .Range(0, allSamples.Length / channelCount)
                    .Select(i => allSamples[channel + i * channelCount])
                    .ToArray())
                .ToArray();

            return channelSamples;
        }

        // 对声道求均值
        private float[] AverageChannels(float[][] channelSamples, int channelCount)
        {
            // 设定我们已经将分开了的采样保存到了变量 ChannelSamples 中, 类型为 float[][]
            // 例如通道数为2, 那么左声道的采样为 ChannelSamples[0], 右声道为 ChannelSamples[1]
            float[] averageSamples = Enumerable
                .Range(0, channelSamples[0].Length)
                .Select(index => Enumerable
                    .Range(0, channelCount)
                    .Select(channel => channelSamples[channel][index])
                    .Average())
                .ToArray();

            return averageSamples;
        }

        // 傅里叶变换
        private Complex[] FFT(float[] samples)
        {
            // 我们将对 AverageSamples 进行傅里叶变换, 得到一个复数数组

            // 因为对于快速傅里叶变换算法, 需要数据长度为 2 的 n 次方, 这里进行
            int log = (int)Math.Ceiling(Math.Log(samples.Length, 2));   // 取对数并向上取整
            int newLen = (int)Math.Pow(2, log);                             // 计算新长度
            float[] filledSamples = new float[newLen];
            samples.CopyTo(filledSamples, 0);  // 拷贝到新数组
            Complex[] complexSrc = filledSamples
                .Select(v => new Complex() { X = v })        // 将采样转换为复数
                .ToArray();
            FastFourierTransform.FFT(false, log, complexSrc);   // 进行傅里叶变换

            // 变换之后, complexSrc 则已经被处理过, 其中存储了频域信息
            return complexSrc;
        }

        // 找出数组最大值下标
        private int MaxIndex<T>(T[] arr) where T : IComparable
        {
            int maxIndex = 0;
            T max = arr[0];
            for(int i=1; i<arr.Length; i++)
            {
                if (arr[i].CompareTo(max) > 0)
                {
                    max = arr[i];
                    maxIndex = i;
                }
            }
            return maxIndex;
        }
    }
}
