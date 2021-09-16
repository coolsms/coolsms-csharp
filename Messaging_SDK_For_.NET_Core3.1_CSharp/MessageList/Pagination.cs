// 메시지 목록 페이지 처리 예제
using System;

static class Pagination
{
    public static void Run()
    {
        MessagingLib.Response page1 = GetList(null);
        // nextKey란? 다음 페이지 목록 읽어올 첫번째 핸들키
        string nextKey = page1.Data.SelectToken("nextKey").ToString();
        MessagingLib.Response page2 = GetList(nextKey);
    }

    public static MessagingLib.Response GetList(string nextKey = null)
    {
        string query = "/messages/v4/list";
        if (nextKey != null) query = query + "?startKey=" + nextKey;
        Console.WriteLine(query);
        MessagingLib.Response response = MessagingLib.Request(query, "GET");

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine("조회 결과");
            Console.WriteLine("startKey: " + response.Data.SelectToken("startKey").ToString());
            Console.WriteLine("목록 개수: " + response.Data.SelectToken("limit").ToString());
            // 메시지 목록 출력
            // Console.WriteLine("messageList: " + response.Data.SelectToken("messageList").ToString());
            Console.WriteLine("nextKey: " + response.Data.SelectToken("nextKey").ToString());

        }
        else
        {
            Console.WriteLine("Error Code:" + response.ErrorCode);
            Console.WriteLine("Error Message:" + response.ErrorMessage);
        }

        return response;
    }
}
