using System.Text;

namespace UTF8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string chineseText = "报名尚未开始，开始时间为：2023-09-22 05:00"; // 中文字符串
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(chineseText); // 将中文字符串转换为 UTF-8 编码的字节数组
            string utf8Text = Encoding.UTF8.GetString(utf8Bytes); // 将 UTF-8 编码的字节数组转换为字符串

            Console.WriteLine("中文文本：{0}", chineseText); // 输出中文文本
            Console.WriteLine("UTF-8 编码：{0}", BitConverter.ToString(utf8Bytes)); // 输出 UTF-8 编码的字节数组
            Console.WriteLine("UTF-8 文本：{0}", utf8Text); // 输出 UTF-8 编码的字符串
        }
    }
}