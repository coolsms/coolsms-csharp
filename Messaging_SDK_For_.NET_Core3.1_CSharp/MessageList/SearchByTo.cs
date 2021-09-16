using System;
using System.Web;

static class SearchByTo
{
    public static void Run()
    {
        string path = "/messages/v4/list";
        string query = "?to=01000000001"; // 수신번호로 검색

        MessagingLib.Response response = MessagingLib.Request(path + query, "GET");

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
