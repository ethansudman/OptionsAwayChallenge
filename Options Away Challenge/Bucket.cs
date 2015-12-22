using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Options_Away_Challenge
{
    [DataContract]
    public sealed class Bucket
    {
        [DataMember]
        public string BucketName { get; set; }

        [DataMember]
        public List<string> Flights { get; set; }

        public Bucket(string BucketName, List<string> Flights)
        {
            this.BucketName = BucketName;
            this.Flights = Flights;
        }

        public static List<Bucket> ToBucket(Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> bucket)
        {
            var bucketList = new List<Bucket>();

            foreach (var key in bucket.Keys)
            {
                foreach (var key2 in bucket[key].Keys)
                {
                    foreach (var key3 in bucket[key][key2].Keys)
                    {
                        string bucketStr = String.Format("{0},{1},{2}", key, key2, key3);
                        List<string> buckets = bucket[key][key2][key3];
                        bucketList.Add(new Bucket(bucketStr, buckets));
                    }
                }
            } // End foreach

            return bucketList;
        }
    }
}
