using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewBridge.UMengPush
{
 public    class UMengPushMessage
    {
        #region 内部字段

        private RestClient requestClient;

        private volatile static Dictionary<Type, PropertyInfo[]> cacheParamType = null;

        private ReaderWriterLock lockrw = null;

        private MD5CryptionUMeng md5 = new MD5CryptionUMeng();

        private Encoding encoder = Encoding.UTF8;

        private string requestProtocol = "http";

        private string requestMethod = "POST";

        private string hostUrl = "msg.umeng.com";

        private string postPath = "api/send";

        private string apiFullUrl = null;

        private string appkey = null;

        private string appMasterSecret = null;

        protected const string USER_AGENT = "Push-Server/4.5";

        #endregion
        public  ReturnJsonClass Send(UmengNotification msg)
        {
            var request = CreateHttpRequest(msg);
            var resultResponse = requestClient.Execute(request);
            ReturnJsonClass rjs = SimpleJson.DeserializeObject<ReturnJsonClass>(resultResponse.Content);
            return rjs;
        }
        public void AsynSendMessage(UmengNotification paramsJsonObj, Action<ReturnJsonClass> callback)
        {
            var request = CreateHttpRequest(paramsJsonObj);

            requestClient.ExecuteAsync(request, resultResponse =>
            {
                if (callback != null)
                {
                    callback(SimpleJson.DeserializeObject<ReturnJsonClass>(resultResponse.Content));
                }
            });
        }

        #region 私有辅助方法

        private RestRequest CreateHttpRequest(UmengNotification paramsJsonObj)
        {
            string bodyJson = InitParamsAndUrl(paramsJsonObj);

            if (requestClient == null)
            {
                requestClient = new RestClient(apiFullUrl);
                requestClient.Encoding = Encoding.UTF8;
                requestClient.UserAgent = USER_AGENT;
            }
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json", bodyJson, ParameterType.RequestBody);
            //request.AddJsonBody(paramsJsonObj);
            return request;
        }

        private string InitParamsAndUrl(UmengNotification paramsJsonObj)
        {
            //重置url
            this.apiFullUrl = string.Concat(requestProtocol, "://", hostUrl, "/", postPath, "/");

            Newtonsoft.Json.JsonSerializerSettings jssetting = new Newtonsoft.Json.JsonSerializerSettings();
            jssetting.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(paramsJsonObj.root, jssetting);

            string calcSign = md5.GenerateMD5(requestMethod + apiFullUrl + json + paramsJsonObj.appMasterSecret).ToLower();

            this.apiFullUrl = string.Format("{0}?sign={1}", this.apiFullUrl, calcSign);

            return json;
        }

        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        private uint GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return Convert.ToUInt32(ts.TotalSeconds);
        }

        /// <summary>
        /// 多线程安全缓存参数类型集合
        /// </summary>
        private PropertyInfo[] GetCacheParamType<T>(T pb)
        {
            Type pbtype = pb.GetType();
            try
            {
                if (cacheParamType.ContainsKey(pbtype))
                {
                    return cacheParamType[pbtype];
                }
                else
                {
                    lockrw.AcquireWriterLock(1000);
                    if (!cacheParamType.ContainsKey(pbtype))
                    {
                        PropertyInfo[] pis = pbtype.GetProperties().OrderBy(p => p.Name).ToArray();
                        cacheParamType.Add(pbtype, pis);
                        return pis;
                    }
                    else
                    {
                        return cacheParamType[pbtype];
                    }
                }
            }
            finally
            {
                if (lockrw.IsReaderLockHeld)
                {
                    lockrw.ReleaseReaderLock();
                }
                if (lockrw.IsWriterLockHeld)
                {
                    lockrw.ReleaseWriterLock();
                }
            }

        }

        #endregion
    }
}
