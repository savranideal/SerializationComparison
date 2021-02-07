using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Benchmark.Models
{

    public class TwitterTimelineRootObject
    {
        public TwitterTimeline[] Property1 { get; set; }
    }

    public class TwitterTimeline
    {
        public int retweet_count { get; set; }
        public object in_reply_to_user_id { get; set; }
        public bool favorited { get; set; }
        public string created_at { get; set; }
        public object in_reply_to_screen_name { get; set; }
        public object in_reply_to_status_id { get; set; }
        public object in_reply_to_status_id_str { get; set; }
        public User user { get; set; }
        public bool retweeted { get; set; }
        public bool truncated { get; set; }
        public object in_reply_to_user_id_str { get; set; }
        public Entities entities { get; set; }
        public object place { get; set; }
        public object geo { get; set; }
        public string source { get; set; }
        public object contributors { get; set; }
        public object coordinates { get; set; }
        public long id { get; set; }
        public string id_str { get; set; }
        public string text { get; set; }
        public bool possibly_sensitive { get; set; }
    }
     

   
    public class Hashtag
    {
        public int[] indices { get; set; }
        public string text { get; set; }
    }

   
    public class Medium
    {
        public string type { get; set; }
        public string display_url { get; set; }
        public string id_str { get; set; }
        public string media_url_https { get; set; }
        public int[] indices { get; set; }
        public string expanded_url { get; set; }
        public string url { get; set; }
        public long id { get; set; }
        public string media_url { get; set; }
        public Sizes sizes { get; set; }
    }

    public class Sizes
    {
        public Small small { get; set; }
        public Large large { get; set; }
        public Thumb thumb { get; set; }
        public Medium1 medium { get; set; }
    }

    public class Small
    {
        public int h { get; set; }
        public int w { get; set; }
        public string resize { get; set; }
    }

    public class Large
    {
        public int h { get; set; }
        public int w { get; set; }
        public string resize { get; set; }
    }

    public class Thumb
    {
        public int h { get; set; }
        public int w { get; set; }
        public string resize { get; set; }
    }

    public class Medium1
    {
        public int h { get; set; }
        public int w { get; set; }
        public string resize { get; set; }
    }

}
