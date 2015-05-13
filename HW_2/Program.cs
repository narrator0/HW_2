using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_2
{
    class Program
    {
        static void Main(string[] args)
        {
            answer_finder activity = new answer_finder();

            int k = 0;
            bool is_wrong = false;
            do
            {
                do
                {
                    try
                    {
                        is_wrong = false;
                        Console.Write("請輸入小隊數:");
                        activity.Num_of_teams = int.Parse(Console.ReadLine());
                        Console.Write("請輸入關卡數:");
                        activity.Num_of_stages = int.Parse(Console.ReadLine());

                        if (activity.Num_of_teams % 2 == 1 || activity.Num_of_teams / 2 > activity.Num_of_stages
                            || answer_finder.c_take_two(activity.Num_of_teams) < (Math.Pow(activity.Num_of_stages, 2) - (activity.Num_of_stages - activity.Num_of_teams / 2) * activity.Num_of_stages))
                        {
                            is_wrong = true;
                            Console.WriteLine("此條件無法解出任何解\n");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("錯誤!!請重新輸入\n");
                        is_wrong = true;
                    }
                } while (is_wrong);

                //執行找尋工作
                activity.do_in_one();

                Console.WriteLine("haha");
            } while (k == 0);  
  
        }
    }
}
