using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.UMengPush
{
   public abstract class UmengNotification
    {
        public Dictionary<string, object> root = new Dictionary<string, object>();


        // The app master secret
        public string appMasterSecret;

        // Keys can be set in the root level
        protected static string[] ROOT_KEYS = new string[] {
            "appkey", "timestamp", "type", "device_tokens", "alias", "alias_type", "file_id",
            "filter", "production_mode", "feedback", "description", "thirdparty_id" };

        // Keys can be set in the policy level
        protected static string[] POLICY_KEYS = new string[] 
        { "start_time", "expire_time", "max_send_num" };

        public abstract bool setPredefinedKeyValue(string key, object value);
        public void setAppMasterSecret(string secret)
        {
            appMasterSecret = secret;
        }

        public string getPostBody()
        {
            return JsonConvert.SerializeObject(root);
        }

        protected string getAppMasterSecret()
        {
            return appMasterSecret;
        }

        public void setProductionMode(bool prod) 
        {
            setPredefinedKeyValue("production_mode", prod.ToString().ToLower());
        }

        ///正式模式
        public void setProductionMode() 
        {
            setProductionMode(true);
        }

        ///测试模式
        public void setTestMode()
        {
            setProductionMode(false);
        }

        ///发送消息描述，建议填写。
        public void setDescription(string description) 
        {
            setPredefinedKeyValue("description", description);
        }

        ///定时发送时间，若不填写表示立即发送。格式: "YYYY-MM-DD hh:mm:ss"。
        public void setStartTime(DateTime startTime) 
        {
            setPredefinedKeyValue("start_time", startTime.ToString("yyyy-MM-dd hh:mm:ss"));
        }
        ///消息过期时间,格式: "YYYY-MM-DD hh:mm:ss"。
        public void setExpireTime(DateTime expireTime) 
        {
            setPredefinedKeyValue("expire_time", expireTime.ToString("yyyy-MM-dd hh:mm:ss"));
        }
        ///发送限速，每秒发送的最大条数。
        public void setMaxSendNum(int num) 
        {
            setPredefinedKeyValue("max_send_num", num);
        }
        public void setTimeSpan()
        {
            setPredefinedKeyValue("timestamp", GetTimeStamp());
        }
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        private uint GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return Convert.ToUInt32(ts.TotalSeconds);
        }

    }
}
