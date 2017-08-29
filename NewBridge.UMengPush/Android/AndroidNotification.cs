using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.UMengPush
{
    public class AndroidNotification : UmengNotification
    {
        // Keys can be set in the payload level
        protected static string[] PAYLOAD_KEYS = new string[] { "display_type" };

        // Keys can be set in the body level
        protected static string[] BODY_KEYS = new string[] {"ticker", "title", "text", "builder_id", "icon", "largeIcon", "img", "play_vibrate", "play_lights", "play_sound",
            "sound", "after_open", "url", "activity", "custom"};
        public override bool setPredefinedKeyValue(string key, object value)
        {
            if (ROOT_KEYS.Contains(key))
            {
                // This key should be in the root level
                root.Add(key, value);
            }
            else if (PAYLOAD_KEYS.Contains(key))
            {
                // This key should be in the payload level
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
            }
            else if (BODY_KEYS.Contains(key))
            {
                // This key should be in the body level
                Dictionary<string, object> body = null;
                Dictionary<string, object> payload = null;
                // 'body' is under 'payload', so build a payload if it doesn't exist
                if (root.ContainsKey("payload"))
                {
                    payload = root["payload"] as Dictionary<string, object>;
                }
                else
                {
                    payload = new Dictionary<string, object>();
                    root.Add("payload", payload);
                }
                // Get body JSONObject, generate one if not existed
                if (payload.ContainsKey("body"))
                {
                    body = payload["body"] as Dictionary<string, object>;
                }
                else
                {
                    body = new Dictionary<string, object>();
                    payload.Add("body", body);
                }
                body.Add(key, value);
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
                if (key == "payload" || key == "body" || key == "policy" || key == "extra")
                {
                    throw new Exception("You don't need to set value for " + key + " , just set values for the sub keys in it.");
                }
                else
                {
                    throw new Exception("Unknown key: " + key);
                }
            }
            return true;
        }
        // Set extra key/value for Android notification
        public bool setExtraField(string key, string value)
        {
            Dictionary<string, object> payload = null;
            Dictionary<string, object> extra = null;
            if (root.ContainsKey("payload"))
            {
                payload = root["payload"] as Dictionary<string, object>;
            }
            else
            {
                payload = new Dictionary<string, object>();
                root.Add("payload", payload);
            }
            if (payload.ContainsKey("extra"))
            {
                extra = payload["extra"] as Dictionary<string, object>;
            }
            else
            {
                extra = new Dictionary<string, object>();
                payload.Add("extra", extra);
            }
            extra.Add(key, value);
            return true;
        }
        public enum DisplayType
        {
            notification,
            message
        }///通知:消息送达到用户设备后，由友盟SDK接管处理并在通知栏上显示通知内容。
        public enum AfterOpenAction
        {
            go_app,//打开应用
            go_url,//跳转到URL
            go_activity,//打开特定的activity
            go_custom//用户自定义内容。
        }
        public void setDisplayType(DisplayType d)
        {
            setPredefinedKeyValue("display_type", d.ToString().ToLower());
        }
        ///通知栏提示文字
        public void setTicker(string ticker)
        {
            setPredefinedKeyValue("ticker", ticker);
        }
        ///通知标题
        public void setTitle(string title)
        {
            setPredefinedKeyValue("title", title);
        }
        ///通知文字描述
        public void setText(string text)
        {
            setPredefinedKeyValue("text", text);
        }
        ///用于标识该通知采用的样式。使用该参数时, 必须在SDK里面实现自定义通知栏样式。
        public void setBuilderId(int builder_id)
        {
            setPredefinedKeyValue("builder_id", builder_id);
        }
        ///状态栏图标ID, R.drawable.[smallIcon],如果没有, 默认使用应用图标。
        public void setIcon(string icon)
        {
            setPredefinedKeyValue("icon", icon);
        }
        ///通知栏拉开后左侧图标ID
        public void setLargeIcon(string largeIcon)
        {
            setPredefinedKeyValue("largeIcon", largeIcon);
        }
        ///通知栏大图标的URL链接。该字段的优先级大于largeIcon。该字段要求以http或者https开头。
        public void setImg(string img)
        {
            setPredefinedKeyValue("img", img);
        }
        ///收到通知是否震动,默认为"true"
        public void setPlayVibrate(bool play_vibrate)
        {
            setPredefinedKeyValue("play_vibrate", play_vibrate.ToString());
        }
        ///收到通知是否闪灯,默认为"true"
        public void setPlayLights(bool play_lights)
        {
            setPredefinedKeyValue("play_lights", play_lights.ToString());
        }
        ///收到通知是否发出声音,默认为"true"
        public void setPlaySound(bool play_sound)
        {
            setPredefinedKeyValue("play_sound", play_sound.ToString());
        }
        ///通知声音，R.raw.[sound]. 如果该字段为空，采用SDK默认的声音
        public void setSound(string sound)
        {
            setPredefinedKeyValue("sound", sound);
        }
        ///收到通知后播放指定的声音文件
        public void setPlaySound(string sound)
        {
            setPlaySound(true);
            setSound(sound);
        }

        ///点击"通知"的后续行为，默认为打开app。
        public void goAppAfterOpen()
        {
            setAfterOpenAction(AfterOpenAction.go_app);
        }
        public void goUrlAfterOpen(String url)
        {
            setAfterOpenAction(AfterOpenAction.go_url);
            setUrl(url);
        }
        public void goActivityAfterOpen(String activity)
        {
            setAfterOpenAction(AfterOpenAction.go_activity);
            setActivity(activity);
        }
        public void goCustomAfterOpen(object custom)
        {
            setAfterOpenAction(AfterOpenAction.go_custom);
            setCustomField(custom);
        }

        ///点击"通知"的后续行为，默认为打开app。原始接口
        public void setAfterOpenAction(AfterOpenAction action)
        {
            setPredefinedKeyValue("after_open", action.ToString().ToLower());
        }
        public void setUrl(string url)
        {
            setPredefinedKeyValue("url", url);
        }
        public void setActivity(string activity)
        {
            setPredefinedKeyValue("activity", activity);
        }
        public void setCustomField(object custom)
        {
            setPredefinedKeyValue("custom", custom);
        }
    }
}
