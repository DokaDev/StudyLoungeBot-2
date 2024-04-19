namespace BusInfo;

/// <summary>
/// <code>
/// using System;
/// using System.Net;
/// using System.Net.Http;
/// using System.IO;
///
/// namespace ConsoleApp1
/// {
///     class Program
///     {
///         static HttpClient client = new HttpClient();
///         static void Main(string[] args)
///         {
///             string url = "http://ws.bus.go.kr/api/rest/arrive/getArrInfoByRouteAll"; // URL
///             url += "?ServiceKey=" + "서비스키"; // Service Key
///             url += "&busRouteId=100100118";
///
///             var request = (HttpWebRequest)WebRequest.Create(url);
///             request.Method = "GET";
///
///             string results = string.Empty;
///             HttpWebResponse response;
///             using (response = request.GetResponse() as HttpWebResponse)
///             {
///                 StreamReader reader = new StreamReader(response.GetResponseStream());
///                 results = reader.ReadToEnd();
///             }
///
///             Console.WriteLine(results);
///         }
///     }
/// }
/// </code>
/// </summary>
internal sealed class SampleSniffets { }