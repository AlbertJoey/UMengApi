using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NewBridge.UMengPush;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var androidAppkey = "your anroid appkey ";
            var androidAppMasterSecret = "your android appMasterSecret";
            var iosAppkey = "you ios appkey";
            var iosAppMasterSecret = "your ios appMasterSecret";
            Program p = new Program();
            #region ===================Android 测试=======================
            //广播测试
            //p.sendAndroidBroadcast(androidAppkey, androidAppMasterSecret);
            #endregion

            #region ======================IOS 测试========================
            //广播测试
            p.sendIOSBroadcast(iosAppkey, iosAppMasterSecret);
            #endregion

            Console.ReadKey();
        }

        #region ================Android 推送方法======================

        /// <summary>
        /// 广播
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appMasterSecret"></param>
        public void sendAndroidBroadcast(string appKey, string appMasterSecret)
        {
            AndroidBroadcast broadcast = new AndroidBroadcast(appKey, appMasterSecret);
            broadcast.setTicker("Android broadcast ticker");
            broadcast.setTitle("小江江"+DateTime.Now.ToString());
            broadcast.setText("Android broadcast text");
            broadcast.setTimeSpan();
            broadcast.goAppAfterOpen();
            broadcast.setDisplayType(AndroidNotification.DisplayType.notification);
            // TODO Set 'production_mode' to 'false' if it's a test device. 
            // For how to register a test device, please see the developer doc.
            broadcast.setProductionMode();
            // Set customized fields
            broadcast.setExtraField("test", "helloworld");
            //var data = JsonConvert.SerializeObject(broadcast.root);
            NewBridge.UMengPush.ReturnJsonClass resu = new UMengPushMessage().Send(broadcast);
            Console.WriteLine("sendAndroidBroadcast: " + resu.ret + resu.data.error_code);
        }
        /// <summary>
        /// 单播
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appMasterSecret"></param>
        public void sendAndroidUnicast(string appKey, string appMasterSecret)
        {
            AndroidUnicast unicast = new AndroidUnicast(appKey, appMasterSecret);
            // TODO Set your device token
            unicast.setDeviceToken("your device token");
            unicast.setTicker("Android unicast ticker");
            unicast.setTitle("中文的title");
            unicast.setText("Android unicast text");
            unicast.goAppAfterOpen();
            unicast.setDisplayType(AndroidNotification.DisplayType.notification);
            // TODO Set 'production_mode' to 'false' if it's a test device. 
            // For how to register a test device, please see the developer doc.
            unicast.setProductionMode();
            // Set customized fields
            unicast.setExtraField("test", "helloworld");
            NewBridge.UMengPush.ReturnJsonClass resu = new UMengPushMessage().Send(unicast);
            Console.WriteLine(resu.ret + resu.data.error_code);
            Console.ReadKey();
        }
        /// <summary>
        /// 组播
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appMasterSecret"></param>
        public void sendAndroidGroupcast(string appKey, string appMasterSecret)
        {
            AndroidGroupcast groupcast = new AndroidGroupcast(appKey, appMasterSecret);
            /*  TODO
             *  Construct the filter condition:
             *  "where": 
             *	{
             *		"and": 
             *		[
             *			{"tag":"test"},
             *			{"tag":"Test"}
             *		]
             *	}
             */
            Dictionary<string, object> filter = new Dictionary<string, object>();
            Dictionary<string, object> where = new Dictionary<string, object>();
            Dictionary<string, object> and = new Dictionary<string, object>();
            and.Add("tag", "tag1");
            and.Add("tag", "tag2");
            where.Add("and", and);
            groupcast.setFilter(filter);
            groupcast.setTicker("Android groupcast ticker");
            groupcast.setTitle("中文的title");
            groupcast.setText("Android groupcast text");
            groupcast.goAppAfterOpen();
            groupcast.setDisplayType(AndroidNotification.DisplayType.notification);
            // TODO Set 'production_mode' to 'false' if it's a test device. 
            // For how to register a test device, please see the developer doc.
            groupcast.setProductionMode();
            NewBridge.UMengPush.ReturnJsonClass resu = new UMengPushMessage().Send(groupcast);
            Console.WriteLine(resu.ret + resu.data.error_code);
            Console.ReadKey();
        }
        /// <summary>
        /// 用户自定义推送
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appMasterSecret"></param>
        public void sendAndroidCustomizedcast(string appKey, string appMasterSecret)
        {
            AndroidCustomizedcast customizedcast = new AndroidCustomizedcast(appKey, appMasterSecret);
            // TODO Set your alias here, and use comma to split them if there are multiple alias.
            // And if you have many alias, you can also upload a file containing these alias, then 
            // use file_id to send customized notification.
            customizedcast.setAlias("alias", "alias_type");
            customizedcast.setTicker("Android customizedcast ticker");
            customizedcast.setTitle("中文的title");
            customizedcast.setText("Android customizedcast text");
            customizedcast.goAppAfterOpen();
            customizedcast.setDisplayType(AndroidNotification.DisplayType.notification);
            // TODO Set 'production_mode' to 'false' if it's a test device. 
            // For how to register a test device, please see the developer doc.
            customizedcast.setProductionMode();
            NewBridge.UMengPush.ReturnJsonClass resu = new UMengPushMessage().Send(customizedcast);
            Console.WriteLine(resu.ret + resu.data.error_code);
            Console.ReadKey();
        }
        /// <summary>
        /// 用户自定义上传文件推送
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appMasterSecret"></param>
        public void sendAndroidCustomizedcastFile(string appKey, string appMasterSecret)
        {
            AndroidCustomizedcast customizedcast = new AndroidCustomizedcast(appKey, appMasterSecret);
            // TODO Set your alias here, and use comma to split them if there are multiple alias.
            // And if you have many alias, you can also upload a file containing these alias, then 
            // use file_id to send customized notification.

            //上传文件
            //String fileId = client.uploadContents(appKey, appMasterSecret, "aa" + "\n" + "bb" + "\n" + "alias");
            //customizedcast.setFileId(fileId, "alias_type");
            customizedcast.setTicker("Android customizedcast ticker");
            customizedcast.setTitle("中文的title");
            customizedcast.setText("Android customizedcast text");
            customizedcast.goAppAfterOpen();
            customizedcast.setDisplayType(AndroidNotification.DisplayType.notification);
            // TODO Set 'production_mode' to 'false' if it's a test device. 
            // For how to register a test device, please see the developer doc.
            customizedcast.setProductionMode();
            NewBridge.UMengPush.ReturnJsonClass resu = new UMengPushMessage().Send(customizedcast);
            Console.WriteLine(resu.ret + resu.data.error_code);
            Console.ReadKey();
        }
        /// <summary>
        /// 上传文件推送
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appMasterSecret"></param>
        public void sendAndroidFilecast(string appKey, string appMasterSecret)
        {
            AndroidFilecast filecast = new AndroidFilecast(appKey, appMasterSecret);
            // TODO upload your device tokens, and use '\n' to split them if there are multiple tokens 
            //String fileId = client.uploadContents(appKey, appMasterSecret, "aa" + "\n" + "bb");
            //filecast.setFileId(fileId);
            filecast.setTicker("Android filecast ticker");
            filecast.setTitle("中文的title");
            filecast.setText("Android filecast text");
            filecast.goAppAfterOpen();
            filecast.setDisplayType(AndroidNotification.DisplayType.notification);
            NewBridge.UMengPush.ReturnJsonClass resu = new UMengPushMessage().Send(filecast);
            Console.WriteLine(resu.ret + resu.data.error_code);
            Console.ReadKey();
        }
        #endregion

        #region================= IOS推送方法=========================
        /// <summary>
        /// 广播
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appMasterSecret"></param>
        public void sendIOSBroadcast(string appKey, string appMasterSecret)
        {
            IOSBroadcast broadcast = new IOSBroadcast(appKey, appMasterSecret);

            broadcast.setAlert("小江江" + DateTime.Now.ToString());
            broadcast.setBadge(1);
            broadcast.setSound("default");
            broadcast.setTimeSpan();
            // TODO set 'production_mode' to 'true' if your app is under production mode
            broadcast.setTestMode();
            // Set customized fields
            broadcast.setCustomizedField("after_open_type", "0");
            broadcast.setCustomizedField("VehicleId", Guid.NewGuid().ToString());
            //var data = JsonConvert.SerializeObject(broadcast.root);
            NewBridge.UMengPush.ReturnJsonClass resu = new UMengPushMessage().Send(broadcast);
            Console.WriteLine("sendIOSBroadcast: " + resu.ret + resu.data.error_code);
        }
        /// <summary>
        /// 单播
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appMasterSecret"></param>
        public void sendIOSUnicast(string appKey, string appMasterSecret)
        {
            IOSUnicast unicast = new IOSUnicast(appKey, appMasterSecret);
            // TODO Set your device token
            unicast.setDeviceToken("xx");
            unicast.setAlert("IOS 单播测试");
            unicast.setBadge(0);
            unicast.setSound("default");
            // TODO set 'production_mode' to 'true' if your app is under production mode
            unicast.setTestMode();
            // Set customized fields
            unicast.setCustomizedField("test", "helloworld");
            NewBridge.UMengPush.ReturnJsonClass resu = new UMengPushMessage().Send(unicast);
            Console.WriteLine(resu.ret + resu.data.error_code);
            Console.ReadKey();
        }
        /// <summary>
        /// 组播
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appMasterSecret"></param>
        public void sendIOSGroupcast(string appKey, string appMasterSecret)
        {
            IOSGroupcast groupcast = new IOSGroupcast(appKey, appMasterSecret);
            /*  TODO
            *  Construct the filter condition:
            *  "where": 
            *	{
            *		"and": 
            *		[
            *			{"tag":"test"},
            *			{"tag":"Test"}
            *		]
            *	}
            */
            Dictionary<string, object> filter = new Dictionary<string, object>();
            Dictionary<string, object> where = new Dictionary<string, object>();
            Dictionary<string, object> and = new Dictionary<string, object>();
            and.Add("tag", "tag1");
            and.Add("tag", "tag2");
            where.Add("and", and);
            groupcast.setFilter(filter);
            // Set filter condition into rootJson
            groupcast.setAlert("IOS 组播测试");
            groupcast.setBadge(0);
            groupcast.setSound("default");
            // TODO set 'production_mode' to 'true' if your app is under production mode
            groupcast.setTestMode();
            NewBridge.UMengPush.ReturnJsonClass resu = new UMengPushMessage().Send(groupcast);
            Console.WriteLine(resu.ret + resu.data.error_code);
            Console.ReadKey();
        }
        /// <summary>
        /// 用户自定义推送
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appMasterSecret"></param>
        public void sendIOSCustomizedcast(string appKey, string appMasterSecret)
        {
            IOSCustomizedcast customizedcast = new IOSCustomizedcast(appKey, appMasterSecret);
            // TODO Set your alias and alias_type here, and use comma to split them if there are multiple alias.
            // And if you have many alias, you can also upload a file containing these alias, then 
            // use file_id to send customized notification.
            customizedcast.setAlias("alias", "alias_type");
            customizedcast.setAlert("IOS 个性化测试");
            customizedcast.setBadge(0);
            customizedcast.setSound("default");
            // TODO set 'production_mode' to 'true' if your app is under production mode
            customizedcast.setTestMode();
            NewBridge.UMengPush.ReturnJsonClass resu = new UMengPushMessage().Send(customizedcast);
            Console.WriteLine(resu.ret + resu.data.error_code);
            Console.ReadKey();
        }
        /// <summary>
        /// 文件播
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appMasterSecret"></param>
        public void sendIOSFilecast(string appKey, string appMasterSecret)
        {
            IOSFilecast filecast = new IOSFilecast(appKey, appMasterSecret);
            // TODO upload your device tokens, and use '\n' to split them if there are multiple tokens 
            //String fileId = client.uploadContents(appkey, appMasterSecret, "aa" + "\n" + "bb");
            //filecast.setFileId( fileId);
            filecast.setAlert("IOS 文件播测试");
            filecast.setBadge(0);
            filecast.setSound("default");
            // TODO set 'production_mode' to 'true' if your app is under production mode
            filecast.setTestMode();
            NewBridge.UMengPush.ReturnJsonClass resu = new UMengPushMessage().Send(filecast);
            Console.WriteLine(resu.ret + resu.data.error_code);
            Console.ReadKey();
        }
        #endregion
    }
}
