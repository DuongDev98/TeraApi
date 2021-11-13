using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TT
{
    public class TeraAppConnect
    {
        string baseUrl = @"http://teraapp.net:12000/v1/";
        string apiKey = "";
        string secretKey = "";

        public TeraAppConnect(string apiKey, string secretKey)
        {
            this.apiKey = apiKey;
            this.secretKey = secretKey;
            this.secretKey = secretKey;
        }

        public List<TeraOrder> GetOrderList()
        {
            string url = baseUrl + "synchonizeData/getOrderList";
            List<TeraOrder> lst = new List<TeraOrder>();
            string dataJson = PostDataToServer(url);
            if (dataJson.Length > 0)
            {
                JObject data = JObject.Parse(dataJson);
                JArray arr = JArray.Parse(data.SelectToken("orders").ToString());
                foreach (JObject item in arr)
                {
                    TeraOrder order = JsonConvert.DeserializeObject<TeraOrder>(item.ToString());
                    lst.Add(order);
                }
            }
            return lst;
        }

        public List<TeraCustomer> GetCustomerList()
        {
            string url = baseUrl + "synchonizeData/getCustomerList";
            List<TeraCustomer> lst = new List<TeraCustomer>();
            string dataJson = PostDataToServer(url);
            if (dataJson.Length > 0)
            {
                JObject data = JObject.Parse(dataJson);
                if (Convert.ToInt32(data.SelectToken("resultCode")) == (int)HttpStatusCode.OK)
                {
                    JArray arr = JArray.Parse(data.SelectToken("customers").ToString());
                    foreach (JObject item in arr)
                    {
                        TeraCustomer pro = JsonConvert.DeserializeObject<TeraCustomer>(item.ToString());
                        lst.Add(pro);
                    }
                }
            }
            return lst;
        }

        public List<TeraProduct> GetItemList()
        {
            string url = baseUrl + "synchonizeData/getItemList";
            List<TeraProduct> lst = new List<TeraProduct>();
            string dataJson = PostDataToServer(url);
            if (dataJson.Length > 0)
            {
                JObject data = JObject.Parse(dataJson);
                if (Convert.ToInt32(data.SelectToken("resultCode")) == (int)HttpStatusCode.OK)
                {
                    JArray arr = JArray.Parse(data.SelectToken("items").ToString());
                    foreach (JObject item in arr)
                    {
                        TeraProduct pro = JsonConvert.DeserializeObject<TeraProduct>(item.ToString());
                        lst.Add(pro);
                    }
                }
            }
            return lst;
        }

        public string PostDataToServer(string currentUrl, string jsonData = "")
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("apikey", apiKey);
            dic.Add("secretkey", secretKey);
            string data = "";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(currentUrl);
            httpRequest.ProtocolVersion = HttpVersion.Version11;
            httpRequest.ContentType = "application/json; charset=utf-8";
            httpRequest.Method = "POST";
            if (dic != null && dic.Count > 0)
            {
                foreach (string key in dic.Keys)
                {
                    httpRequest.Headers.Add(key, dic[key]);
                }
            }

            try
            {
                if (jsonData != "")
                {
                    byte[] dataArr = Encoding.UTF8.GetBytes(jsonData);
                    httpRequest.ContentLength = dataArr.Length;
                    using (Stream writer = httpRequest.GetRequestStream())
                    {
                        writer.Write(dataArr, 0, dataArr.Length);
                    }
                }

                using (StreamReader reader = new StreamReader(httpRequest.GetResponse().GetResponseStream()))
                {
                    data = reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
            }
            return data;
        }
    }

    public class TeraOrderDetail
    {
        public string companyId { set; get; }
        public int cooperatorid { set; get; }
        public int discounttotal { set; get; }
        public string id { set; get; }
        public string itemId { set; get; }
        public string name { set; get; }
        public string orderId { set; get; }
        public string price { set; get; }
        public int quantity { set; get; }
        public int sellingprice { set; get; }
        public string title { set; get; }
    }

    public class TeraOrder
    {
        public string address { set; get; }
        public string addressLevelOneName { set; get; }
        public string addressLevelThreeName { set; get; }
        public string addressLevelTwoName { set; get; }
        public string addresslevelone { set; get; }
        public string addresslevelthree { set; get; }
        public string addressleveltwo { set; get; }
        public string companyId { set; get; }
        public string couponcode { set; get; }
        public int coupondiscount { set; get; }
        public int coupontype { set; get; }
        public string customerId { set; get; }
        public int deliverystatus { set; get; }
        public string description { set; get; }
        public string deviceId { set; get; }
        public int discount { set; get; }
        public int discounttotal { set; get; }
        public string fullname { set; get; }
        public string email { set; get; }
        public string id { set; get; }
        public string name { set; get; }
        public List<TeraOrderDetail> orderItems { set; get; }
        public int paymentstatus { set; get; }
        public int paymenttype { set; get; }
        public string phone { set; get; }
        public int status { set; get; }
        public string total { set; get; }
        public string totalorigin { set; get; }
    }

    public class TeraProduct
    {
        public string id { set; get; }
        public int available { set; get; }
        public int price { set; get; }
        public int discount { set; get; }
        public int discounttype { set; get; }
        public int weight { set; get; }
        public string code { set; get; }
        public string categoryName { set; get; }
        public string categoryId { set; get; }
        public string companyId { set; get; }
        public string imageId { set; get; }
        public string imageIds { set; get; }
        public int originalPrice { set; get; }
        public bool hasDiscount { set; get; }
    }

    public class TeraCustomer
    {
        public string address { set; get; }
        public string birth { set; get; }
        public string email { set; get; }
        public string fullName { set; get; }
        public string id { set; get; }
        public string note { set; get; }
        public string phone { set; get; }
        public string qrcodeImage { set; get; }
        public int sex { set; get; }
        public string password { set; get; }
        public int status { set; get; }
        public string userName { set; get; }
    }
}