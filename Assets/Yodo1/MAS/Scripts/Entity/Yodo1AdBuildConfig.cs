
using System.Collections.Generic;

namespace Yodo1.MAS
{
    
    public class Yodo1AdBuildConfig
    {
        
        private bool _enableAdaptiveBanner;

        public Yodo1AdBuildConfig enableAdaptiveBanner(bool adaptiveBanner)
        {
            this._enableAdaptiveBanner = adaptiveBanner;
            return this;
           
        }

        public string toJson()
        {
            System.Collections.Generic.Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("enableAdaptiveBanner", _enableAdaptiveBanner);
            return Yodo1JSON.Serialize(dic);
        }
               

    }
}



