using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.UMengPush
{
    public class IOSNotification : UmengNotification
    {
        protected static string[] APS_KEYS = new string[] { "alert", "badge", "sound", "content-available" };
        public void setAlert(string token)
        {
            setPredefinedKeyValue("alert", token);
        }

        public void setBadge(int badge)
        {
            setPredefinedKeyValue("badge", badge);
        }

        public void setSound(string sound)
        {
            setPredefinedKeyValue("sound", sound);
        }

        public void setContentAvailable(int contentAvailable)
        {
            setPredefinedKeyValue("content-available", contentAvailable);
        }
        public bool setCustomizedField(string key, string value)
        {
            //rootJson.put(key, value);
            Dictionary<string, object> payload = null;
            if (root.ContainsKey("payload"))
            {
                payload = root["payload"] as Dictionary<string, object>;
            }
            else
            {
                payload = new Dictionary<string, object>();
                root.Add("payload", payload);
            }
            payload.Add(key, value);
            return true;
        }
        public override bool setPredefinedKeyValue(string key, object value)
        {
            if (ROOT_KEYS.Contains(key))
            {
                // This key should be in the root level
                root.Add(key, value);
            }
            else if (APS_KEYS.Contains(key))
            {
                // This key should be in the aps level
                Dictionary<string, object> aps = null;
                Dictionary<string, object> payload = null;
                if (root.ContainsKey("payload"))
                {
                    payload = root["payload"] as Dictionary<string, object>;
                }
                else
                {
                    payload = new Dictionary<string, object>();
                    root.Add("payload", payload);
                }
                if (payload.ContainsKey("aps"))
                {
                    aps = payload["aps"] as Dictionary<string,object>;
                }
                else
                {
                    aps = new Dictionary<string, object>();
                    payload.Add("aps", aps);
                }
                aps.Add(key, value);
            }
            else if (POLICY_KEYS.Contains(key))
            {
                Dictionary<string, object> policy = null;
                if (root.ContainsKey("policy"))
                {
                    policy = root["policy"] as Dictionary<string, object>;
                }
                else
                {
                    policy = new Dictionary<string, object>();
                    root.Add("policy", policy);
                }
                policy.Add(key, value);
            }
            else
            {
                if (key == "payload" || key == "aps" || key == "policy")
                {
                    throw new Exception("You don't need to set value for " + key + " , just set values for the sub keys in it.");
                }
                else
                {
                    throw new Exception("Unknownd key: " + key);
                }
            }

            return true;
        }
    }
}
