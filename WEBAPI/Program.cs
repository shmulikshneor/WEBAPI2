using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Crm.Sdk.Samples.HelperCode;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WEBAPI
{
    class Program
    {
        private HttpClient httpClient;
        static void Main(string[] args)
        {


            Program app = new Program();
            try
            {
                String[] arguments = Environment.GetCommandLineArgs();
                app.ConnectToCRM(arguments);
            }
            catch (System.Exception ex)
            {; }
            finally
            {
                if (app.httpClient != null)
                { app.httpClient.Dispose(); }
            }

        }
        private void ConnectToCRM(String[] cmdargs)
        {
            Configuration config = null;
            if (cmdargs.Length > 0)
                config = new FileConfiguration(cmdargs[0]);
            else
                config = new FileConfiguration(null);
            Authentication auth = new Authentication(config);
            httpClient = new HttpClient(auth.ClientHandler, true);
            httpClient.BaseAddress = new Uri(config.ServiceUrl + "api/data/v8.1/");
            httpClient.Timeout = new TimeSpan(0, 2, 0);
            httpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            httpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        private static void DisplayException(Exception ex)
        {
            Console.WriteLine("The application terminated with an error.");
            Console.WriteLine(ex.Message);
            while (ex.InnerException != null)
            {
                Console.WriteLine("\t* {0}", ex.InnerException.Message);
                ex = ex.InnerException;
            }
        }
    }
}
