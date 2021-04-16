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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

static class SendSMS
{
    public static void Run()
    {
        MessagingLib.Messages messages = new MessagingLib.Messages();
        messages.Add(new MessagingLib.Message()
        {
            to = "01000000001",
            from = "029302266",
            text = "한글 45자, 영자 90자 이하 입력되면 자동으로 SMS타입의 메시지가 추가됩니다."
        });
        messages.Add(new MessagingLib.Message()
        {
            to = "01000000002",
            from = "029302266",
            text = "한글 45자, 영자 90자 이상 입력되면 자동으로 LMS타입의 문자메시자가 발송됩니다. 0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        });
        // 타입을 명시할 경우 text 길이가 한글 45 혹은 영자 90자를 넘을 경우 오류가 발생합니다.
        messages.Add(new MessagingLib.Message()
        {
            to = "01000000003",
            from = "029302266",
            text = "SMS 타입에 한글 45자, 영자 90자 이상 입력되면 오류가 발생합니다. 0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZ"
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
