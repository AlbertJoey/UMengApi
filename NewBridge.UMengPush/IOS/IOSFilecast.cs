using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.UMengPush
{
   public class IOSFilecast:IOSNotification
    {
        public IOSFilecast(string appkey, string appMasterSecret)
        {
            setAppMasterSecret(appMasterSecret);
            setPredefinedKeyValue("appkey", appkey);
			this.setPredefinedKeyValue("type", "filecast");
        }

        public void setFileId(string fileId)
        {
            setPredefinedKeyValue("file_id", fileId);
        }
    }
}
