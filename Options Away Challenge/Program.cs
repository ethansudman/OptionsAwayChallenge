using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Options_Away_Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var bucketData = Program.BuildBucketData();

            Program.PopulateBucket(bucketData);

            Program.Serialize(bucketData);
        }

        private static void Serialize(Dictionary<string, Dictionary<string, Dictionary<string, List<Flight>>>> bucket)
        {
            using (var fstream = new FileStream(Directory.GetCurrentDirectory() + @"\results.json", FileMode.Create))
            {
                var settings = new DataContractJsonSerializerSettings()
                {
                    UseSimpleDictionaryFormat = true,
                    DateTimeFormat = new DateTimeFormat("yyyy-MM-dd HH:mm:ss")
                };

                var serializer = new DataContractJsonSerializer(typeof(Dictionary<string, Dictionary<string, Dictionary<string, List<Flight>>>>), settings);
                serializer.WriteObject(fstream, bucket);
            }
        }

        private static void PopulateBucket(Dictionary<string, Dictionary<string, Dictionary<string, List<Flight>>>> bucket)
        {
            // Hardcoding this isn't goood
            using (var reader = new StreamReader(Directory.GetCurrentDirectory() + @"\flight_data.json"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    var flight = new Flight(line);

                    string key = bucket.ContainsKey(flight.Airline) ?
                        flight.Airline :
                        (bucket.ContainsKey("*") ? "*" : bucket.Keys.First());

                    string key2 = bucket[key].ContainsKey(flight.cabin_class.ToString()) ?
                        flight.cabin_class.ToString() :
                        (bucket[key].ContainsKey("*") ? "*" : bucket[key].Keys.First());

                    string key3 = bucket[key][key2].ContainsKey(flight.OptionDuration) ?
                        flight.OptionDuration :
                        (bucket[key][key2].ContainsKey("*") ? "*" : bucket[key][key2].Keys.First());

                    bucket[key][key2][key3].Add(flight);
                } // Finish looping through the lines
            }
        }

        private static Dictionary<string, Dictionary<string, Dictionary<string, List<Flight>>>> BuildBucketData()
        {
            // Admittedly this type is a bit ugly
            var dictionary = new Dictionary<string, Dictionary<string, Dictionary<string, List<Flight>>>>();

            // We could just do File.ReadAllLines here but we take somewhat less of a memory hit this way; the sample file is small but a real data file could
            // potentially be much larger
            using (var reader = new StreamReader(Directory.GetCurrentDirectory() + @"\flight_buckets.json"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    string[] split = line.Split(',');

                    if (dictionary.ContainsKey(split[0]))
                    {
                        if (dictionary[split[0]].ContainsKey(split[1]))
                        {
                            if (!dictionary[split[0]][split[1]].ContainsKey(split[2]))
                                dictionary[split[0]][split[1]][split[2]] = new List<Flight>();
                        }
                        else
                        {
                            dictionary[split[0]][split[1]] = new Dictionary<string, List<Flight>>();
                            dictionary[split[0]][split[1]][split[2]] = new List<Flight>();
                        }
                    }

                    else
                    {
                        dictionary[split[0]] = new Dictionary<string, Dictionary<string, List<Flight>>>();
                        dictionary[split[0]][split[1]] = new Dictionary<string, List<Flight>>();
                        dictionary[split[0]][split[1]][split[2]] = new List<Flight>();
                    }

                    //dictionary[split[0]][split[1]][split[2]]
                } // Finish looping through the lines
            } // Dispose of file stream

            return dictionary;
        } // BuildBucketData
    }
}
