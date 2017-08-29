using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.UMengPush
{
  public  class AndroidCustomizedcast:AndroidNotification
    {
        public AndroidCustomizedcast(string appkey, string appMasterSecret) 
        {
            setAppMasterSecret(appMasterSecret);
            setPredefinedKeyValue("appkey", appkey);
			this.setPredefinedKeyValue("type", "customizedcast");
        }

        public void setAlias(string alias, string aliasType) 
        {
            setPredefinedKeyValue("alias", alias);
            setPredefinedKeyValue("alias_type", aliasType);
        }

        public void setFileId(string fileId, string aliasType)
        {
            setPredefinedKeyValue("file_id", fileId);
            setPredefinedKeyValue("alias_type", aliasType);
        }
    }
}
