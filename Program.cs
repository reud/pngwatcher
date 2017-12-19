using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pngwatcher
{
    class Program
    {
        public static uint counter=0,sendcounter=1,st=0;
        static uint s = 0,pi=3,i;
        public static string[] site = new string[8];
        public static int indexnum=0;
        public static int valued = 0;
        public static string parse = "s";
        public static bool knows = true;
        //エントリポイント
        static void Main(string[] args)
        {
            
            //Processオブジェクトを作成
            site[0]= @"ping www.u-tokyo.ac.jp";
            site[1]= @"ping www.edogawa-u.ac.jp";
            site[2]= @"ping www.stanford.edu";
            site[3]= @"ping www.washington.edu";
            site[4]= @"ping www.harvard.edu";
            site[5] = @"ping web.mit.edu";
            site[6] = @"ping www.cam.ac.uk";
            site[7] = @"ping www.ox.ac.uk";
            System.Diagnostics.Process p = new System.Diagnostics.Process();

            //入力できるようにする
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;

            //非同期で出力を読み取れるようにする
            p.StartInfo.RedirectStandardOutput = true;
            p.OutputDataReceived += p_OutputDataReceived;

            p.StartInfo.FileName =
                System.Environment.GetEnvironmentVariable("ComSpec");
            p.StartInfo.CreateNoWindow = true;

            //起動
            p.Start();

            //非同期で出力の読み取りを開始
            p.BeginOutputReadLine();

            //入力のストリームを取得
            System.IO.StreamWriter sw = p.StandardInput;
            if (sw.BaseStream.CanWrite)
            {
                for (s = 0; s < 8; ++s)
                {
                    for(i=0;i<3;++i)
                    {


                        sw.WriteLine(site[s]);

                    }
                    knows = true;
                    counter = 0;
                    
                }
                    sw.WriteLine("exit");
            }
            sw.Close();

            p.WaitForExit();
            p.Close();

            Console.ReadLine();
        }

        //OutputDataReceivedイベントハンドラ
        //行が出力されるたびに呼び出される
        static void p_OutputDataReceived(object sender,
            System.Diagnostics.DataReceivedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Users\otakey\休日昼.txt", true))
            {
                if (counter == 0)
                {
                    sw.WriteLine(DateTime.Now);
                    Console.WriteLine(DateTime.Now);
                    ++counter;
                }
                if (sendcounter > pi+2 && sendcounter < pi+8)
                {
                    if(sendcounter==6&&st==0)
                    {
                        sw.WriteLine(e.Data);
                        Console.WriteLine(e.Data);
                    }
                    indexnum = e.Data.IndexOf("時間");
                    if(indexnum>0)
                    {
                        parse = e.Data.Substring(indexnum + 4, 2);
                        sw.WriteLine(parse);
                        Console.WriteLine(parse);
                        ++st;

                    }
                    else
                    {
                        
                        if (st == 12)
                        {

                            st = 0;
                            sw.WriteLine(e.Data);
                            Console.WriteLine(e.Data);
                        }
                    }


                       
                    

                    
                    ++sendcounter;
                    
                }
                else if(sendcounter==(pi+13))
                {
                   // Console.WriteLine("resetsend");
                    sendcounter = 1;
                    pi = 0;
                }
                else
                {
                    //Console.WriteLine(e.Data);
                    //Console.WriteLine("sendcounter"+sendcounter);
                    ++sendcounter;
                }
            }
                //出力された文字列を表示する
                
        }
    }
}
