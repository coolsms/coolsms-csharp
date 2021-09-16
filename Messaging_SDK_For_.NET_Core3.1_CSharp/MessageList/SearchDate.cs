using System;
using System.Web;

static class SearchDate
{
    public static void Run()
    {
        string path = "/messages/v4/list";
        string query = "?limit=10"; // 목록 10개 불러오기
        query = query + "&dateType=CREATED"; // CREATED(접수일시 기준으로 조회) | UPDATED(업데이트 일시 기준으로 조회)
        query = query + "&startDate=" + HttpUtility.UrlEncode("2021-09-16T15:00:00+09:00"); // 9월 16일 오후 3시부터
        query = query + "&endDate=" + HttpUtility.UrlEncode("2021-09-16T16:00:00+09:00"); // 9월 16일 오후 4시까지 조회

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
