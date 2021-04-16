using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

static class GetBalance
{
    public static void Run()
    {
        MessagingLib.Response response = MessagingLib.GetBalance();

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine("조회 결과");
            Console.WriteLine("현재 잔액: " + response.Data.SelectToken("balance").ToString());
            Console.WriteLine("현재 포인트: " + response.Data.SelectToken("point").ToString());
        }
        else
        {
            Console.WriteLine("Error Code:" + response.ErrorCode);
            Console.WriteLine("Error Message:" + response.ErrorMessage);
        }
    }
}
