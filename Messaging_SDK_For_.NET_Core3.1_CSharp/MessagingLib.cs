using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

static class MessagingLib
{
    public class Message
    {
        public string type;
        public string to;
        public string from;
        public string subject;
        public string text;
        public string imageId;
        public KakaoOptions kakaoOptions;
    }

    public class Messages
    {
        public List<Message> messages = new List<Message>();

        public void Add(Message message)
        {
            messages.Add(message);
        }
    }

    class GroupInfo
    {
        public string appId;
        public bool strict;
        public string sdkVersion;
        public string osPlatform;
    }

    class Image
    {
        public string type;
        public string file;
        public string name;
        public string link;
    }

    public class KakaoButton
    {
        public string buttonType;
        public string buttonName;
        public string linkMo;
        public string linkPc;
        public string linkAnd;
        public string linkIos;
    }
    public class KakaoOptions
    {
        public string pfId;
        public string templateId;
        public bool disableSms;
        public string imageId;
        public KakaoButton[] buttons;
    }

    public class Response
    {
        public System.Net.HttpStatusCode StatusCode;
        public string ErrorCode;
        public string ErrorMessage;
        public JObject Data;
    }

    private static JsonSerializerSettings JsonSettings = new JsonSerializerSettings()
    {
        NullValueHandling = NullValueHandling.Ignore
    };

    public static string GetSignature(string apiKey, string data, string apiSecret)
    {
        System.Security.Cryptography.HMACSHA256 sha256 = new System.Security.Cryptography.HMACSHA256(System.Text.Encoding.UTF8.GetBytes(apiSecret));
        byte[] hashValue = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data));
        string hash = BitConverter.ToString(hashValue).Replace("-", "");
        return hash.ToLower();
    }

    public static string GetSalt(int len = 32)
    {
        string s = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random r = new Random();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 1; i <= len; i++)
        {
            int idx = r.Next(0, 35);
            sb.Append(s.Substring(idx, 1));
        }
        return sb.ToString();
    }
    public static string GetAuth(string apiKey, string apiSecret)
    {
        string salt = GetSalt();
        string dateStr = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        string data = dateStr + salt;

        return "HMAC-SHA256 apiKey=" + apiKey + ", date=" + dateStr + ", salt=" + salt + ", signature=" + GetSignature(apiKey, data, apiSecret);
    }

    public static string GetUrl(object path)
    {
        string url = Config.protocol + "://" + Config.domain;
        if (!string.IsNullOrEmpty(Config.prefix))
        {
            url += Config.prefix;
        }
        url += path;
        return url;
    }

    public static Response Request(string path, string method, string data = null)
    {
        string auth = GetAuth(Config.apiKey, Config.apiSecret);

        try
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(GetUrl(path));
            req.Method = method;
            req.Headers.Add("Authorization", auth);
            // req.ContentType = "application/json; charset=utf-8"; // .NetFrameWork 호환성으로 인한 오류 발생 시 이 구문 사용. 아래 구문은 주석처리 
            req.Headers.Add("Content-type", "application/json; charset=utf-8");

            if (!string.IsNullOrEmpty(data))
            {
                using (var writer = new System.IO.StreamWriter(req.GetRequestStream()))
                {
                    writer.Write(data);
                    writer.Close();
                }
            }

            using (System.Net.WebResponse response = req.GetResponse())
            {
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    var jsonResponseText = streamReader.ReadToEnd();
                    JObject jsonObj = JObject.Parse(jsonResponseText);
                    return new Response()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Data = jsonObj,
                        ErrorCode = null,
                        ErrorMessage = null
                    };
                }
            }
        }
        catch (System.Net.WebException ex)
        {
            using (System.IO.StreamReader streamReader = new System.IO.StreamReader(ex.Response.GetResponseStream()))
            {
                var jsonResponseText = streamReader.ReadToEnd();
                JObject jsonObj = JObject.Parse(jsonResponseText);
                string ErrorCode = jsonObj.SelectToken("errorCode").ToString();
                string ErrorMessage = jsonObj.SelectToken("errorMessage").ToString();
                System.Net.HttpWebResponse httpResp = (System.Net.HttpWebResponse)ex.Response;
                return new Response()
                {
                    StatusCode = httpResp.StatusCode,
                    Data = jsonObj,
                    ErrorCode = ErrorCode,
                    ErrorMessage = ErrorMessage
                };
            }
        }
        catch (Exception ex)
        {
            string ErrorCode = "Unknown Exception";
            string ErrorMessage = ex.Message;

            return new Response()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Data = null,
                ErrorCode = ErrorCode,
                ErrorMessage = ErrorMessage
            };
        }
    }

    class Group
    {
        private string groupId;

        public Group()
        {
        }


        public Group(string groupId)
        {
            this.groupId = groupId;
        }

        public Response AddMessages(Messages msgs)
        {
            if (string.IsNullOrEmpty(groupId))
                throw new System.Exception("그룹아이디가 설정되지 않았습니다.");
            return Request("/messages/v4/groups/" + groupId + "/messages", "POST", JsonConvert.SerializeObject(msgs, Formatting.None, JsonSettings));
        }

        public Response Create()
        {
            GroupInfo groupInfo = new GroupInfo()
            {
                osPlatform = Environment.OSVersion.VersionString,
                sdkVersion = "VB.NET v1"
            };
            return Request("/messages/v4/groups", "POST", JsonConvert.SerializeObject(groupInfo, Formatting.None, JsonSettings));
        }

        public Response GetList()
        {
            return Request("/messages/v4/groups", "GET");
        }
    }


    public static Response SendMessages(Messages messages)
    {
        return Request("/messages/v4/send-many", "POST", JsonConvert.SerializeObject(messages, Formatting.None, JsonSettings));
    }

    public static Response UploadImage(string path)
    {
        byte[] bytes = System.IO.File.ReadAllBytes(path);
        Image img = new Image();
        img.type = "MMS";
        img.file = Convert.ToBase64String(bytes);
        return Request("/storage/v1/files", "POST", JsonConvert.SerializeObject(img, Formatting.None, JsonSettings));
    }

    public static Response UploadKakaoImage(string path, string url)
    {
        byte[] bytes = System.IO.File.ReadAllBytes(path);
        Image img = new Image();
        img.type = "KAKAO";
        img.file = Convert.ToBase64String(bytes);
        img.name = System.IO.Path.GetFileName(path);
        img.link = url;
        return Request("/storage/v1/files", "POST", JsonConvert.SerializeObject(img, Formatting.None, JsonSettings));
    }

    public static Response GetBalance()
    {
        return Request("/cash/v1/balance", "GET");
    }

    public static Response GetGroupList()
    {
        return Request("/messages/v4/groups", "GET");
    }
}
