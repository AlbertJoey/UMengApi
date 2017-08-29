using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.UMengPush
{
  public  class IOSUnicast:IOSNotification
    {
        public IOSUnicast(string appkey, string appMasterSecret) 
        {
            setAppMasterSecret(appMasterSecret);
            setPredefinedKeyValue("appkey", appkey);
			this.setPredefinedKeyValue("type", "unicast");
        }

        public void setDeviceToken(string token)
        {
            setPredefinedKeyValue("device_tokens", token);
        }
    }
}
