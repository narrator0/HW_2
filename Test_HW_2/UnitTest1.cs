using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HW_2;
using System.Diagnostics;

namespace Test_HW_2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_c_find_two()
        {
            Debug.Assert(answer_finder.c_take_two(6) == 15);
        }

        [TestMethod]
        public void Test_Search()
        {
            answer_finder test = new answer_finder(3, 6);
            test.do_in_one();

            int[, ,] expect = { { { 0, 1 }, { 2, 3 }, { 4, 5 } }, { { 2, 4 }, { 0, 5 }, { 1, 3 } }, { { 3, 5 }, { 1, 4 }, { 0, 2 } } };

            CollectionAssert.AreEqual(expect, test.Search);

        }
    }
}
