﻿using System;

namespace BCBot.Core.Models
{
    public class AccessToken
    {
        public string access_token { get; set; }
        public long expires_in { get; set; }
        public int group_id { get; set; }
        public string group_name { get; set; }
        public string scope { get; set; }
        public string token_type { get; set; }
    }

    public class ExpiringAccessToken
    {
        public AccessToken Token { get; set; }

        public DateTime ExpirationTimeStamp { get; set; }
    }
}