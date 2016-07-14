using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Net;

namespace Bot_Application1
{
    public class UV
    {
        public static async Task<string> GetUVAsync(string site)
        {
            string url = $"http://opendata.epa.gov.tw/ws/Data/UV/?&$format=csv&$filter=SiteName%20eq%20%27{site}%27&$select=SiteName,UVI,PublishTime";
            string csv = string.Empty;
            using (WebClient client = new WebClient())
            {
                csv = await client.DownloadStringTaskAsync(url).ConfigureAwait(false);
            }
            string sent = string.Empty;
            if (String.IsNullOrEmpty(csv))
            {
                return "null";
            }
            else
            {
                string UVI = csv.Split(',')[3];
                string line = csv.Split(',')[4];
                string Publishtime = line.Split('\r')[0];
                sent = UVI + "," + Publishtime;
                return sent;
            }

        }
    }
}