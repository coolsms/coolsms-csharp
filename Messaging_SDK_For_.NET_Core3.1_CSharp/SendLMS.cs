using System;

static class SendLMS
{
    public static void Run()
    {
        // TextingLib.GetGroupList()
        // Dim group = New TextingLib.Group()
        // Console.WriteLine(group.GetList())

        MessagingLib.Messages messages = new MessagingLib.Messages();
        messages.Add(new MessagingLib.Message()
        {
            to = "01000000001",
            from = "029302266",
            text = "한글 45자, 영자 90자 이상 입력되면 자동으로 LMS타입의 문자메시자가 발송됩니다. 0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        });
        messages.Add(new MessagingLib.Message()
        {
            to = "01000000002",
            from = "029302266",
            subject = "LMS 제목",
            text = "한글 45자, 영자 90자 이상 입력되면 자동으로 LMS타입의 문자메시자가 발송됩니다. 0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        });
        messages.Add(new MessagingLib.Message()
        {
            type = "LMS",
            to = "01000000003",
            from = "029302266",
            text = "내용이 짧아도 LMS로 발송됩니다."
        });
        messages.Add(new MessagingLib.Message()
        {
            to = "01000000004",
            from = "029302266",
            text = "한글 45자, 영자 90자 이하는 자동으로 SMS타입의 문자가 발송됩니다."
        });

        // 1만건까지 추가 가능

        MessagingLib.Response response = MessagingLib.SendMessages(messages);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine("전송 결과");
            Console.WriteLine("Group ID:" + response.Data.SelectToken("groupId").ToString());
            Console.WriteLine("Status:" + response.Data.SelectToken("status").ToString());
            Console.WriteLine("Count:" + response.Data.SelectToken("count").ToString());
        }
        else
        {
            Console.WriteLine("Error Code:" + response.ErrorCode);
            Console.WriteLine("Error Message:" + response.ErrorMessage);
        }
    }
}
