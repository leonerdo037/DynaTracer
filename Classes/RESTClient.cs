using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DynaTracer.Windows
{
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class RESTClient
    {
        private string Target;
        private string UserName;
        private string Password;
        private bool isHttps = false;
        private bool BasicAuthRequired = false;
        private static bool hasValidCertificate;

        /// <summary>
        /// Initiates unauthenticated API requests.
        /// </summary>
        /// <param name="targetURL">The URL of the API server.
        /// <para>Example:  http(s)://DomainName.TopLevelDomain </para>
        /// </param>
        public RESTClient(string targetURL)
        {
            Target = targetURL;
            // Checking if url is secure
            var uri = new Uri(Target);
            var requestType = uri.Scheme;
            if (requestType == "https")
            {
                isHttps = true;
            }
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        /// <summary>
        /// Initiates API requests with Basic Authentication.
        /// </summary>
        /// <param name="targetURL">The URL of the API server.
        /// <para>Example:  http(s)://DomainName.TopLevelDomain </para>
        /// </param>
        /// <param name="userName">The username that is used basic authentication.</param>
        /// <param name="password">The password that is used for basic authentication.</param>
        public RESTClient(string targetURL, string userName, string password)
        {
            Target = targetURL;
            // Checking if url is secure
            var uri = new Uri(Target);
            var requestType = uri.Scheme;
            if (requestType == "https")
            {
                isHttps = true;
            }
            UserName = userName;
            Password = password;
            BasicAuthRequired = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        private HttpWebRequest CreateRequest(string target, HttpVerb requestType)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(target);
            // Checking if Basic Auth is required
            if (BasicAuthRequired)
            {
                webRequest.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(UserName + ":" + Password));
            }
            webRequest.Method = HttpVerb.GET.ToString();
            return webRequest;
        }

        private string CreateResponse(HttpWebRequest request)
        {
            string responseValue = string.Empty;
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    }
                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    responseValue = "Error code: {0}" + httpResponse.StatusCode;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        responseValue = reader.ReadToEnd();
                    }
                }
            }
            return responseValue;
        }

        /// <summary>
        /// Initiates a Get Request with the provided inputs.
        /// </summary>
        /// <param name="endPoint">The endpoint of the API call.
        /// <para>Example:  /rest/v2/getallresources </para>
        /// </param>
        /// <param name="headers">(Optional)A Dictionary with the request Headers.</param>
        /// <param name="URLParameters">(Optional)A Dictionary with the URL Parameters.</param>
        /// <param name="IgnoreCertificate">(Optional)Ignores SSL certificate validation.</param>
        public string GetRequest(string endPoint, Dictionary<string, string> headers = null, Dictionary<string, string> URLParameters = null, bool IgnoreCertificate = false)
        {
            // Getting Parameters
            if (URLParameters != null)
            {
                string parameters = "?";
                foreach (string parameter in URLParameters.Keys)
                {
                    parameters += parameter + "=" + URLParameters[parameter] + "&";
                }
                endPoint += parameters.Remove(parameters.Length - 1, 1);
            }
            HttpWebRequest currentRequest = CreateRequest(Target + endPoint, HttpVerb.GET);
            // Checking if connection is secure
            if (isHttps && IgnoreCertificate)
            {
                // Ignoring Certification
                currentRequest.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            }
            else if (isHttps && !IgnoreCertificate)
            {
                currentRequest.ServerCertificateValidationCallback += CertificateValidation;
                // Validating Certificate
                if (!hasValidCertificate)
                {
                    return "Certificate Error";
                }
            }
            // Getting Headers
            if (headers != null)
            {
                foreach (string header in headers.Keys)
                {
                    currentRequest.Headers[header] = headers[header];
                }
            }
            return CreateResponse(currentRequest);
        }

        // Certificate Check
        private static bool CertificateValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            Console.WriteLine(sender.ToString());
            Console.WriteLine(certificate.ToString());
            Console.WriteLine(chain.ToString());
            Console.WriteLine(sslPolicyErrors.ToString());
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                hasValidCertificate = true;
                return true;
            }
            else
            {
                hasValidCertificate = false;
                return false;
            }
        }
    }
}
