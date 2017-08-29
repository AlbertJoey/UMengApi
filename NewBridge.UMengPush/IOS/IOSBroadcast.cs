using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.UMengPush
{
    public class IOSBroadcast : IOSNotification
    {
        public IOSBroadcast(string appkey, string appMasterSecret) 
        {
            setAppMasterSecret(appMasterSecret);
            setPredefinedKeyValue("appkey", appkey);
			this.setPredefinedKeyValue("type", "broadcast");

        }
    }
}
