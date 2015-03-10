using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetCommon.Helper
{
    public class CommonHelper
    {
        public static void CatchAll(Action callback)
        {
            IfCatchException(true, callback);
        }

        public static void IfCatchException(bool catchException, Action callback)
        {
            if (catchException)
            {
                try
                {
                    callback();
                }
                catch { }
            }
            else
            {
                callback();
            }
        }

        /// <summary>
        /// 将指定的金额转化为人民币的大写
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string GetRMBCapitalLetter(decimal money)
        {
            string NumList = "";
            string RmbList = "";
            int NumLen = 0;
            int NumChar = 0;
            string N1 = "";
            string N2 = "";
            decimal Mnu = 0;
            int I = 0;
            string numStr = "";
            string Change = "";

            Mnu = money;
            if (money > 0)
            {
                money = Convert.ToDecimal(money.ToString("#.00"));
            }
            else
            {
                money = Convert.ToDecimal(money.ToString("#.00")) * -1;
            }

            NumList = "零壹贰叁肆伍陆柒捌玖";
            RmbList = "分角元拾佰仟万拾佰仟亿拾佰仟万";

            if (money > 9999999999999.99M)
            {
                return "-------";     //超出范围的人民币值,无法显示!; 
            }

            numStr = Convert.ToString(Convert.ToDecimal(money * 100).ToString("#"));
            NumLen = numStr.Length;

            I = 0;
            while (I <= NumLen - 1)
            {
                NumChar = Convert.ToInt32(numStr.Substring(I, 1));
                N1 = NumList.Substring(NumChar, 1);
                N2 = RmbList.Substring(NumLen - I - 1, 1);

                if (N1 != "零")
                {
                    Change += N1 + N2;
                }
                else
                {
                    if (N2 == "亿" || N2 == "万" || N2 == "元" || N1 == "零")
                    {
                        while (Change.Substring(Change.Length - 1, 1) == "零")
                        {
                            Change = Change.Substring(0, Change.Length - 1);
                        }
                    }
                    if (N2 == "亿" || (N2 == "万" && Change.Substring(Change.Length - 1, 1) != "亿") || N2 == "元")
                    {
                        Change += N2;
                    }
                    else
                    {
                        if (Change.Substring(Change.Length - 2, 2).Substring(0, 1) == "零" || Change.Substring(Change.Length - 1, 1) != "亿")
                        {
                            Change += N1;
                        }
                    }
                }
                I++;
            }
            if (Change.Length > 1)
            {
                while (Change.Substring(Change.Length - 1, 1) == "零")
                {
                    Change = Change.Substring(0, Change.Length - 1);
                }
                if (Change.Substring(Change.Length - 1, 1) == "元" || Change.Substring(Change.Length - 1, 1) == "角")
                {
                    Change += "整";
                }
            }

            if (Mnu > 0)
            {
                return Change;
            }
            else
            {
                if (Mnu < 0)
                {
                    return "负" + Change;
                }
                else
                {
                    return "零元整";
                }
            }
        }
    }
}
