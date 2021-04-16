# C# 용 메시지 발송 SDK

## 사용방법
* Config.cs 와 MessagingLib.cs 파일을 프로젝트로 복사하여 사용합니다.
* Config.cs에 API Key와 API Secret이 올바르게 입력되어 있는지 확인해 주세요.
* Program.cs 예제 메인 모듈을 참고하세요.

## 예제
```
SendSMS.cs          SMS 발송 예제
SendLMS.cs          LMS 발송 예제
SendMMS.cs          MMS 발송 예제
SendAlimtalk.cs     알림톡 발송 예제
SendChingutalk.cs   친구톡 발송 예제
GetBalance.cs       잔액 조회 예제
```

## 예제 실행 방법
아래 형식으로 실행하며 소문자에 주의해 주세요.
```
Messaging.exe [sms, lms, mms, alimtalk, chingutalk, balance]
```

맥에서 실행
```
dotnet Messaging_SDK_For_.NET_Core3.1_CSharp.dll [sms, lms, mms, alimtalk, chingutalk, balance]
```
