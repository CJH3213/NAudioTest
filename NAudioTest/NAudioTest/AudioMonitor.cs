using NAudio.Dsp;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace NAudioTest
{
    internal class AudioMonitor
    {
        private Func<float[], int> mCallBack = null;

        private WasapiLoopbackCapture cap = new WasapiLoopbackCapture();

        public WaveFormat WaveFormat
        {
            get { return cap.WaveFormat; }
        }

        // 构造函数
        public AudioMonitor(Func<float[], int> callBack ) 
        {
            mCallBack = callBack;
            StartRecording();
        }

        // 开始录制输出音频
        private void StartRecording()
        {
            cap.DataAvailable += (sender, e) =>      // 录制数据可用时触发此事件, 参数中包含音频数据
            {
                float[] allSamples = Enumerable      // 提取数据中的采样
                    .Range(0, e.BytesRecorded / 4)   // 除以四是因为, 缓冲区内每 4 个字节构成一个浮点数, 一个浮点数是一个采样
                    .Select(i => BitConverter.ToSingle(e.Buffer, i * 4))  // 转换为 float
                    .ToArray();    // 转换为数组

                // 用户回调
                mCallBack(allSamples);
            };

            cap.StartRecording();   // 开始录制
        }

        // 分离声道
        public float[][] SplitChannels(float[] allSamples)
        {
            // 设定我们已经将刚刚的采样保存到了变量 AllSamples 中, 类型为 float[]
            int channelCount = cap.WaveFormat.Channels;   // WasapiLoopbackCapture 的 WaveFormat 指定了当前声音的波形格式, 其中包含就通道数
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
        public float[] AverageChannels(float[][] channelSamples)
        {
            // 设定我们已经将分开了的采样保存到了变量 ChannelSamples 中, 类型为 float[][]
            // 例如通道数为2, 那么左声道的采样为 ChannelSamples[0], 右声道为 ChannelSamples[1]
            int channelCount = cap.WaveFormat.Channels;
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
        public Complex[] FFT(float[] samples, out double baseFreq)
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
            baseFreq = cap.WaveFormat.SampleRate / complexSrc.Length;
            return complexSrc;
        }

    }
}
