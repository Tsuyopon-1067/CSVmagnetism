using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CSVmagnetism
{
    public class MagArray
    {
        private double[,] degrees;
        public int type { get; } = 0; // 1:必須 2:発展
        public int h { get; } = 0;
        public int w { get; } = 0;
        public MagArray(String path)
        {
            StreamReader sr = new StreamReader(path);
            string text = sr.ReadToEnd();
            text = text.Replace("\r\n", "\n");
            string[] lines = text.Split('\n');
            // 行数から行列の大きさを判断
            if (lines.Length >= 44)
            {
                h = 44;
                w = 22;
                type = 2;
            }
            else if (lines.Length >= 10)
            {
                h = 10;
                w = 10;
                type = 1;
            }
            degrees = new double[h, w];

            w = Math.Min(w, lines[0].Split(',').Length); // 横方向の要素数不足によるエラー防止
            for (int i = 0; i < h; i++)
            {
                string[] line = lines[i].Split(',');
                for (int j = 0; j < w; j++)
                {
                    degrees[i, j] = Convert.ToDouble(line[j]);
                    degrees[i, j] = degrees[i, j] * 360;
                }
            }
        }
        public double Radians(int i, int j)
        {
            return degrees[i, j];
        }
    }
}
