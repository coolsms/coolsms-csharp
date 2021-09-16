using System;

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
