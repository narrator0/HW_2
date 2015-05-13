using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_2
{
    public class answer_finder
    {
         private int num_of_stages, num_of_teams;
        private bool is_over = false;
        private bool[,] test_row, test_column;
        private int[, ,] search;

        //initialze method
        public answer_finder(int num_of_stages, int num_of_teams)
        {
            this.num_of_stages = num_of_stages;
            this.num_of_teams = num_of_teams;
        }
        public answer_finder()
        {

        }

        //set getter and setter
        public int Num_of_stages
        {
            set { num_of_stages = value; }
            get { return num_of_stages; }
        }
        public int Num_of_teams
        {
            set { num_of_teams = value; }
            get { return num_of_teams; }
        }
        public int[, ,] Search
        {
            get { return search; }
        }

        //calculate c take 2
        public static int c_take_two(int n)
        {
            int f;
            f = n * (n - 1) / 2;

            return f;
        }


        //set search
        private void set_search(ref int[, ,] search)
        {
            test_row = new bool[num_of_stages, num_of_teams];
            test_column = new bool[num_of_stages, num_of_teams];

            //沒有多餘的關就直接返回
            if (num_of_stages == num_of_teams / 2)
                return;

            //判斷適合之處給予負數
            //計算每行需要的空白
            int count = 0;
            int n = num_of_stages - num_of_teams / 2;
            int k;

            for (; count < num_of_stages; count++)
            {
                for (k = count; k < count + n; k++)
                {
                    if (k < Num_of_stages)
                    {
                        search[count, k, 0] = -1;
                        search[count, k, 1] = -1;
                    }
                    if (k >= Num_of_stages)
                    {
                        search[count, k - num_of_stages, 0] = -1;
                        search[count, k - num_of_stages, 1] = -1;
                    }
                }
            }
        }

        //print search
        private void print_comb(ref int[, ,] search)
        {
            if (!is_over)
            {
                Console.WriteLine("你給的條件無法解出任何解");
            }
            else
            {
                //印出結果
                int row, column, num;

                for (row = 0; row < num_of_stages; row++)
                {
                    for (column = 0; column < num_of_stages; column++)
                    {
                        for (num = 0; num < 2; num++)
                        {
                            if (search[row, column, num] < 0)
                                Console.Write("x");
                            else
                                Console.Write(search[row, column, num]);
                        }
                        Console.Write(" ");
                    }
                    Console.Write("\n");
                }
            }
        }

        //set first row
        private void set_first_row(ref int[, ,] search)
        {
            int k, putnum = 0;
            for (k = 0; k < num_of_stages; k++)
            {
                if (search[0, k, 0] == 0)
                {
                    search[0, k, 0] = putnum;
                    test_row[0, putnum] = true;
                    test_column[k, putnum] = true;
                    putnum++;

                    search[0, k, 1] = putnum;
                    test_row[0, putnum] = true;
                    test_column[k, putnum] = true;
                    putnum++;
                }
            }
        }

        //找尋解答 save in search
        private void find_the_comb(int count, ref int[, ,] search)
        {
            is_over = false;

            //全部找完結束
            if (count == Math.Pow(num_of_stages, 2))
            {
                is_over = true;  //方便print_comb()辨認是否是無解
                return;
            }

            // 計算所在的位置
            int row = count / num_of_stages;
            int column = count % num_of_stages;

            int row_now, column_now, fnum, snum;
            bool is_conflict = true;

            //負數直接下一層
            if (search[row, column, 0] < 0)
                find_the_comb(count + 1, ref search);

            //是負數就不用找了
            if (search[row, column, 0] >= 0 && !is_over)
            {
                //set the number to try
                for (fnum = 0; fnum < num_of_teams - 1; fnum++)
                    for (snum = fnum + 1; snum < num_of_teams; snum++)
                    {
                        is_conflict = false;

                        //結束就不用看了
                        if (is_over)
                            return;

                        //check row and column
                        if (test_row[row, fnum] || test_row[row, snum] || test_column[column, fnum]
                            || test_column[column, snum] || search[row, column, 0] < 0)
                        {
                            is_conflict = true;
                            continue;
                        }


                        //start trying
                        for (row_now = 0; row_now <= row && !is_conflict; row_now++)
                            for (column_now = 0; column_now < Num_of_stages && !is_conflict; column_now++)
                            {
                                //search over
                                if (row_now == row && column_now == column)
                                    break;

                                //NULL place
                                if (search[row_now, column_now, 0] < 0)
                                    continue;

                                //same combination
                                if (search[row_now, column_now, 0] == fnum && search[row_now, column_now, 1] == snum)
                                    is_conflict = true;
                            }

                        //pass the test to enter the next count
                        if (!is_conflict)
                        {
                            search[row, column, 0] = fnum;
                            search[row, column, 1] = snum;
                            test_row[row, fnum] = true;
                            test_row[row, snum] = true;
                            test_column[column, fnum] = true;
                            test_column[column, snum] = true;
                            find_the_comb(count + 1, ref search);
                        }
                    }
            }
            //fix test if going back
            if (is_conflict)
            {
                if (count == 0)
                    return;
                int fixrow = (count - 1) / num_of_stages;
                int fixcolumn = (count - 1) % num_of_stages;

                if (search[fixrow, fixcolumn, 0] != -1)
                {
                    test_row[fixrow, search[fixrow, fixcolumn, 0]] = false;
                    test_row[fixrow, search[fixrow, fixcolumn, 1]] = false;
                    test_column[fixcolumn, search[fixrow, fixcolumn, 0]] = false;
                    test_column[fixcolumn, search[fixrow, fixcolumn, 1]] = false;
                }
            }

        }

        //整合上面所有動作
        public void do_in_one()
        {
            search = new int[num_of_stages, num_of_stages, 2];

            //set search and find from second row
            set_search(ref search);
            set_first_row(ref search);
            find_the_comb(num_of_stages, ref search);

            //print the result
            Console.WriteLine("\n");
            print_comb(ref search);
        }
    }
}
