using System;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using System.Data;
using System.Text;

namespace MNAIS.Utility
{
    public static class Utility
    {
        public static bool Serialize<T>(T value, String filename)
        {
            if (value == null)
            {
                return false;
            }
            try
            {
                XmlSerializer _xmlserializer = new XmlSerializer(typeof(T));
                Stream stream = new FileStream(filename, FileMode.Create);
                _xmlserializer.Serialize(stream, value);
                stream.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static T Deserialize<T>(String filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return default(T);
            }
            try
            {
                XmlSerializer _xmlSerializer = new XmlSerializer(typeof(T));
                Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                var result = (T)_xmlSerializer.Deserialize(stream);
                stream.Close();
                return result;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static DateTime GetCurrentTime()
        {
            DateTime dateTime = DateTime.Now;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://nist.time.gov/timezone.cgi?UTC/s/0");
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore); 
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader stream = new StreamReader(response.GetResponseStream());
                string html = stream.ReadToEnd().ToUpper();
                string time = Regex.Match(html, @">\d+:\d+:\d+<").Value; 
                string date = Regex.Match(html, @">\w+,\s\w+\s\d+,\s\d+<").Value; 
                dateTime = DateTime.Parse((date + " " + time).Replace(">", "").Replace("<", ""));
            }
            return dateTime.ToLocalTime();
        }
                
        public static IEnumerable<KeyValuePair<T, U>> Zip<T, U>(IEnumerable<T> first, IEnumerable<U> second)
        {
            IEnumerator<T> firstEnumerator = first.GetEnumerator();
            IEnumerator<U> secondEnumerator = second.GetEnumerator();

            while (firstEnumerator.MoveNext())
            {
                if (secondEnumerator.MoveNext())
                {
                    yield return new KeyValuePair<T, U>(firstEnumerator.Current, secondEnumerator.Current);
                }
                else
                {
                    yield return new KeyValuePair<T, U>(firstEnumerator.Current, default(U));
                }
            }
            while (secondEnumerator.MoveNext())
            {
                yield return new KeyValuePair<T, U>(default(T), secondEnumerator.Current);
            }
        }

        public static void ExportToCSV(DataTable dt, string csvFile)
        {
            StringBuilder sb = new StringBuilder();

            var columnNames = dt.Columns.Cast<DataColumn>().Select(column => "\"" + column.ColumnName.Replace("\"", "\"\"") + "\"").ToArray();
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                var fields = row.ItemArray.Select(field => "\"" + field.ToString().Replace("\"", "\"\"") + "\"").ToArray();
                sb.AppendLine(string.Join(",", fields));
            }

            File.WriteAllText(csvFile, sb.ToString(), Encoding.Default);
        }

        public static int GetIntegerDigitCount(int? valueInt)
        {
            return valueInt.ToString().Length;
        }
    }    
}
