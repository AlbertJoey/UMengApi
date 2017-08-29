using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.UMengPush
{
  public  class AndroidGroupcast:AndroidNotification
    {
        public AndroidGroupcast(string appkey, string appMasterSecret)
        {
            setAppMasterSecret(appMasterSecret);
            setPredefinedKeyValue("appkey", appkey);
			this.setPredefinedKeyValue("type", "groupcast");
        }

        public void setFilter(Dictionary<string,object> filter) 
        {
            setPredefinedKeyValue("filter", filter);
        }
    }
}
