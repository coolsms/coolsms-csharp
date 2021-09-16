using System;

static class SendChingutalk
{
    public static void Run()
    {
        // 한번 요청으로 1만건의 친구톡 발송이 가능합니다.
        // 카카오톡채널 친구로 추가되어 있어야 친구톡 발송이 가능합니다.
        // 템플릿 등록없이 버튼을 포함하여 자유롭게 메시지 전송이 가능합니다.
        // 버튼은 5개까지 입력 가능합니다.
        // 버튼 종류(AL: 앱링크, WL: 웹링크, BK: 키워드, MD: 전달)

        MessagingLib.Messages messages = new MessagingLib.Messages();

        // 텍스트 내용만 있는 간단한 알림톡
        messages.Add(new MessagingLib.Message()
        {
            to = "01000000001",
            from = "029302266",
            text = "광고를 포함하여 어떤 내용이든 입력 가능합니다.",
            kakaoOptions = new MessagingLib.KakaoOptions()
            {
                pfId = "KA01PF190626020502205cl0mYSoplA2" // 카카오톡채널 연동 후 발급받은 값을 사용해 주세요
            }
        });

        // 친구톡 이미지 업로드
        MessagingLib.Response imgResp = MessagingLib.UploadKakaoImage("testImage.jpg", "https://example.com"); // 파일명, 이미지 클릭 시 이동할 링크 URL
        string imageId = imgResp.Data.SelectToken("fileId").ToString();

        // 친구톡 이미지 발송
        messages.Add(new MessagingLib.Message()
        {
            to = "01000000002",
            from = "029302266",
            text = "광고를 포함하여 어떤 내용이든 입력 가능합니다.",
            kakaoOptions = new MessagingLib.KakaoOptions()
            {
                pfId = "KA01PF190626020502205cl0mYSoplA2",
                imageId = imageId
            }
        });

        // 모든 종류의 버튼 예시
        messages.Add(new MessagingLib.Message()
        {
            to = "01000000003",
            from = "029302266",
            text = "광고를 포함하여 어떤 내용이든 입력 가능합니다.",
            kakaoOptions = new MessagingLib.KakaoOptions()
            {
                pfId = "KA01PF190626020502205cl0mYSoplA2",
                buttons = new MessagingLib.KakaoButton[]
                {
                    new MessagingLib.KakaoButton()
                    {
                        buttonType = "WL",
                        buttonName = "시작하기",
                        linkMo = "https://m.example.com",
                        linkPc = "https://example.com"
                    },
                    new MessagingLib.KakaoButton()
                    {
                        buttonType = "AL",
                        buttonName = "앱 실행",
                        linkAnd = "examplescheme://",
                        linkIos = "examplescheme://"
                    },
                    new MessagingLib.KakaoButton()
                    {
                        buttonType = "BK",
                        buttonName = "봇키워드"
                    },
                    new MessagingLib.KakaoButton()
                    {
                        buttonType = "MD",
                        buttonName = "상담요청하기"
                    }
                }
            }
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
