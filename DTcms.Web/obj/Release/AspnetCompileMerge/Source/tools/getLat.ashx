<%@ WebHandler Language="C#" Class="getLat" %>

using System;
using System.Web;
using System.Net;
using System.Xml;
using System.IO;

public class getLat : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string add = "";
        string bk = "";
        try
        {
            add = context.Server.UrlDecode(context.Request.QueryString["add"].ToString());
            bk = getlocation(add);
        }
        catch { }

        context.Response.Write(bk);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    private string getlocation(string cityname)
    {
        string bk = "";
        string url = "http://maps.google.com/maps/api/geocode/xml?address=" + cityname + "&sensor=false";
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        String strConfig = String.Empty;
        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
            strConfig = reader.ReadToEnd();
        }
        XmlDocument node = new XmlDocument();
        node.LoadXml(strConfig);
        bk += node.SelectSingleNode("GeocodeResponse/result/geometry/location/lat").InnerText;
        bk += "|";
        bk += node.SelectSingleNode("GeocodeResponse/result/geometry/location/lng").InnerText;
        return bk;
    }

}