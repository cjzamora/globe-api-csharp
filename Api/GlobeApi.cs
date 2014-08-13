using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globe.Api;

/**
 * Globe API Wrapper Class
 * 
 * @author      Charles Zamora <charles.andacc@gmail.com>
 * @since       08-12-2014 02:13:01 PHT
 */
namespace Globe.Api
{
    public class GlobeApi
    {
        /**
         * Returns Auth Class
         * 
         * @return Globe.Api.Auth
         */
        public static Auth Auth(string applicationId, string applicationSecret)
        {
            return new Auth(applicationId, applicationSecret);
        }

        /**
         * Returns Sms Class
         * 
         * @return Globe.Api.Sms
         */
        public static Sms Sms(string shortCode)
        {
            return new Sms(shortCode);
        }

        /**
         * Returns Payment Class
         * 
         * @return Globe.Api.Payment
         */
        public static Payment Payment(string accessToken, string subscriber)
        {
            return new Payment(accessToken, subscriber);
        }
    }
}
