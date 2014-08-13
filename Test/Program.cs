using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globe.Api;

namespace GlobeApiConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string app_id       = "{{app_id}}";
            string app_secret   = "{{app_secret}}";

            var short_code = "{{short_code}}";

            var auth_code = "{{auth_code}}";

            var auth = GlobeApi.Auth(app_id, app_secret);

            Console.WriteLine("Login Url: ");
            Console.WriteLine(auth.getLoginUrl());
            Console.WriteLine("");

            Console.WriteLine("Get Access Token:");
            dynamic access_token = auth.getAccessToken(auth_code).access_token;
            Console.WriteLine(access_token);
            Console.WriteLine("");

            var sms = GlobeApi.Sms(short_code);

            Console.WriteLine("Send SMS Message:");
            dynamic smsResponse = sms.sendMessage(access_token, "{{subscriber}}", "Test Message");
            Console.WriteLine(smsResponse);
            Console.WriteLine("");

            var payment = GlobeApi.Payment(access_token, "{{subscriber}}");

            Console.WriteLine("Send Payment Request:");
            dynamic paymentResponse = payment.charge("0.00", "54630000006");
            Console.WriteLine(paymentResponse);

            Console.Read();
        }
    }
}
