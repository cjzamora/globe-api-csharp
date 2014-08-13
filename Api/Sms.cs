using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;

using Globe.Api;

/**
 * Globe API Wrapper Class
 * 
 * @author      Charles Zamora <charles.andacc@gmail.com>
 * @since       08-12-2014 02:13:01 PHT
 */
namespace Globe.Api
{
    public class Sms
    {
        // Sms request URL
        const string SMS_URL = "http://devapi.globelabs.com.ph/smsmessaging/{0}/outbound/{1}/requests";

        // short code
        protected string shortCode = "";
        // api version
        protected string version   = "v1";
           
        /**
         * Class Contruct
         * 
         * @param   string
         * @return  this
         */
        public Sms(string shortCode)
        {
            // set short code
            this.shortCode = shortCode;
        }

        /**
         * Send up message to the subscriber
         * 
         * @param   string
         * @param   string
         * @param   string
         * @return  NameValueCollection
         */
        public dynamic sendMessage(string accessToken, string subscriber, string message)
        {
            // request parameter
            NameValueCollection parameter = new NameValueCollection();

            // add access token to the request
            parameter.Add("access_token", accessToken);

            // build request url
            string requestUrl = String.Format(SMS_URL, this.version, this.shortCode);
            
            // append query to request url
            requestUrl = requestUrl + Base.toQueryString(parameter);

            // create HttpWebRerquest
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);

            // post data to be sended over request
            string post = "address=" + subscriber + "&message=" + message;

            // convert the data to byte array
            byte[] data = Encoding.UTF8.GetBytes(post);

            // set request method as post
            request.Method          = "POST";
            // set request content type as form urlencoded
            request.ContentType     = "application/x-www-form-urlencoded";
            // set request content length
            request.ContentLength   = data.Length;

            // write the request using stream
            using (Stream stream = request.GetRequestStream())
            {
                // write data into stream
                stream.Write(data, 0, data.Length);
            }

            // create HttpWebResponse
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // parse response string from the response
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            // Deserialize response string into dynamic object
            dynamic responseObject = Base.jsonDeserialize(responseString);

            return responseObject;
        }

        /**
         * Sets the API Version
         * 
         * @param   string
         * @return  this
         */
        public Sms setApiVersion(string version) {
            // set api version
            this.version = version;

            return this;
        }
    }
}
