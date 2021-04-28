using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model
{
        /// <summary>
        /// 微信手机号解密
        /// </summary>
        public class DecodePhoneModel
        {
            public string EncryptedData { get; set; }
            public string IV { get; set; }
            public string SessionKey { get; set; }
            public string OpenID { get; set; }
        }
}
