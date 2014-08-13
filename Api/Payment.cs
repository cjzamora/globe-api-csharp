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
    public class Payment
    {
        // request url for transaction
        const string TRANSACTION_URL = "http://devapi.globelabs.com.ph/payment/{0}/transactions/amount";
        
        // access token
        protected string accessToken = "";
        // subscriber
        protected string subscriber  = "";
        // api version
        protected string version     = "v1";
        
        /**
         * Class Construct
         * 
         * @param   string
         * @param   string
         * @return  this
         */
        public Payment(string accessToken, string subscriber)
        {
            // set access token
            this.accessToken = accessToken;
            // set subscriber
            this.subscriber  = subscriber;
        }

        /**
         * Charge a specific amount
         * using reference code
         * 
         * @param   string
         * @param   string
         */
        public dynamic charge(string amount, string referenceCode)
        {
            // request parameters
            NameValueCollection parameter = new NameValueCollection();

            // add access token to request paramter
            parameter.Add("access_token", this.accessToken);

            // build request url
            string requestUrl = String.Format(TRANSACTION_URL, this.version);

            // add paramter to request url
            requestUrl = requestUrl + Base.toQueryString(parameter);

            // create HttpWebRequest
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);

            // post data to be send over request
            string post = "transactionOperationStatus=charged" +
                          "&access_token=" + this.accessToken +
                          "&endUserId=" + this.subscriber +
                          "&amount=" + amount +
                          "&referenceCode=" + referenceCode;

            // convert data to bytes array
            byte[] data = Encoding.UTF8.GetBytes(post);

            // set request type as post
            request.Method          = "POST";
            // set request content type as form-urlencoded
            request.ContentType     = "application/x-www-form-urlencoded";
            // set request content length
            request.ContentLength   = data.Length;

            // write request data using stream
            using (Stream stream = request.GetRequestStream())
            {
                // write data to stream
                stream.Write(data, 0, data.Length);
            }

            // create HttpWebResponse
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // read response string
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            // parse response as dynamic object
            dynamic responseObject = Base.jsonDeserialize(responseString);

            return responseObject;
        }

        /**
         * Sets the API Version
         * 
         * @param   string
         * @return  this
         */
        public Payment setApiVersion(string version)
        {
            // set api version
            this.version = version;

            return this;
        }
    }
}
