using System;

static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length <= 0)
        {
            PrintHelp();
            return;
        }
        switch (args[0])
        {
            case "sms":
                {
                    SendSMS.Run();
                    return;
                }

            case "lms":
                {
                    SendLMS.Run();
                    return;
                }

            case "mms":
                {
                    SendMMS.Run();
                    return;
                }

            case "alimtalk":
                {
                    SendAlimtalk.Run();
                    return;
                }

            case "chingutalk":
                {
                    SendChingutalk.Run();
                    return;
                }

            case "balance":
                {
                    GetBalance.Run();
                    return;
                }
            case "list":
                {
                    SimpleList.Run();
                    // Pagination.Run();
                    // SearchDate.Run();
                    // SearchGroup.Run();
                    // SearchByMessageID.Run();
                    // SearchByStatusCode.Run();
                    // SearchByTo.Run();
                    return;
                }
        }
        PrintHelp();
    }

    public static void PrintHelp()
    {
        Console.WriteLine("Nurigo [sms, lms, mms, alimtalk, chingutalk, balance, list] 형식으로 실행하세요(소문자 주의)");
    }
}
