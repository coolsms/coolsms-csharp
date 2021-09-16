using System;

static class SendMMS
{
    public static void Run()
    {
        MessagingLib.Response imgResp = MessagingLib.UploadImage("testImage.jpg");
        string imageId = imgResp.Data.SelectToken("fileId").ToString();

        Console.WriteLine(imageId);

        MessagingLib.Messages messages = new MessagingLib.Messages();

        messages.Add(new MessagingLib.Message()
        {
            to = "01000000001",
            from = "029302266",
            imageId = imageId,
            subject = "MMS 제목",
            text = "이미지 아이디가 입력되면 MMS로 발송됩니다."
        });
        messages.Add(new MessagingLib.Message()
        {
            to = "01000000002",
            from = "029302266",
            imageId = imageId,
            subject = "MMS 제목",
            text = "동일한 이미지 아이디가 입력되면 동일한 이미지가 MMS로 발송됩니다."
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
