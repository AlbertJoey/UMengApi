using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.UMengPush
{
    public class AndroidBroadcast : AndroidNotification
    {
        public AndroidBroadcast(string appkey, string appMasterSecret)
        {
            setAppMasterSecret(appMasterSecret);
            setPredefinedKeyValue("appkey", appkey);
				this.setPredefinedKeyValue("type", "broadcast");
        }
    }
}
