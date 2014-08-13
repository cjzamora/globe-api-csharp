using System;
using System.IO;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;

using Globe.Api;

/**
 * Globe API Wrapper Class
 * 
 * @author      Charles Zamora <charles.andacc@gmail.com>
 * @since       08-12-2014 02:13:01 PHT
 */
namespace Globe.Api
{
    public class Auth
    {
        // Access Token Request URL
        const string ACCESS_TOKEN_URL = "http://developer.globelabs.com.ph/oauth/access_token";
        // Oauth Dialog URL
        const string DIALOG_OAUTH_URL = "http://developer.globelabs.com.ph/dialog/oauth";

        // Oauth Application Id
        protected string applicationId     = "";
        // Oauth Application Secret
        protected string applicationSecret = "";
           
        /**
         * Class Construct
         * 
         * @param   string
         * @param   string
         * @return  this
         */
        public Auth(string applicationId, string applicationSecret)
        {
            // set application id
            this.applicationId      = applicationId;
            // set application secret
            this.applicationSecret  = applicationSecret;
        }

        /**
         * Build and Returns Oauth
         * Login Url
         * 
         * @return string
         */
        public string getLoginUrl()
        {
            // request paramter
            NameValueCollection parameter = new NameValueCollection();
            
            // add app id to request parameter
            parameter.Add("app_id", this.applicationId);

            // request query
            string query = DIALOG_OAUTH_URL + Base.toQueryString(parameter);
            
            return query;
        }

        /**
         * Request Access Token to
         * Oauth Server
         * 
         * @param  string
         * @return this
         */
        public dynamic getAccessToken(string code)
        {
            // request parameters
            NameValueCollection parameter = new NameValueCollection();

            // add application id to request parameters
            parameter.Add("app_id", this.applicationId);
            // add application secret to request parameters
            parameter.Add("app_secret", this.applicationSecret);
            // add code to request parameters
            parameter.Add("code", code);

            // request query
            string query = ACCESS_TOKEN_URL + Base.toQueryString(parameter);

            // Create HTTPWebRequest
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query);

            // Set Request Method to POST
            request.Method       = "POST";
            // Set Request Content Type to form-urlencoded
            request.ContentType = "application/x-www-form-urlencoded";

            // Create HttpWebResponse
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Parse Response from Request
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            // Deserialize JSON Response String as dynamic object
            dynamic responseObject = Base.jsonDeserialize(responseString);

            return responseObject;
        }
    }
}
