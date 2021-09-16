// 간단한 목록 읽어오기 예제
using System;

static class SimpleList
{
    public static void Run()
    {
        MessagingLib.Response response = MessagingLib.Request("/messages/v4/list", "GET");

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine("조회 결과");
            Console.WriteLine("startKey: " + response.Data.SelectToken("startKey").ToString());
            Console.WriteLine("limit: " + response.Data.SelectToken("limit").ToString());
            Console.WriteLine("messageList: " + response.Data.SelectToken("messageList").ToString());
        }
        else
        {
            Console.WriteLine("Error Code:" + response.ErrorCode);
            Console.WriteLine("Error Message:" + response.ErrorMessage);
        }
    }
}
