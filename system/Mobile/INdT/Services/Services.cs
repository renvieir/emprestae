using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;

namespace INdT.Services
{
    public class RequestArgs : EventArgs
    {
        public object result;
        public readonly bool failed;

        public RequestArgs(object result, bool failed = false)
        {
            this.result = result;
            this.failed = failed;
        }
    }

    public abstract class Services
    {
        public enum HttpMethod
        {
            GET
            , POST
            , PUT
            , DELETE
        }

        public enum ResponseType
        {
            HTTP_WEB_RESPONSE
            , JSON
            , XML
        }

        protected class RequestState
        {
            public WebRequest request;
            public HttpMethod httpMethod;
            public byte[] sendData;
            public EventHandler<RequestArgs> handler;
            public ResponseType responseType;
        }

        public bool allowAutoRedirect { get; set; }
        public static readonly string DEBUG_TAG = "INdT.Services.Services";

        public Services()
        {
            allowAutoRedirect = false;
        }

        public void get(Uri url, Dictionary<string, object> args, EventHandler<RequestArgs> handler = null, ResponseType responseType = ResponseType.HTTP_WEB_RESPONSE)
        {
            call(url, HttpMethod.GET, args, handler, responseType);
        }

        public void post(Uri url, Dictionary<string, object> args, EventHandler<RequestArgs> handler = null, ResponseType responseType = ResponseType.HTTP_WEB_RESPONSE)
        {
            call(url, HttpMethod.POST, args, handler, responseType);
        }

        public void put(Uri url, Dictionary<string, object> args, EventHandler<RequestArgs> handler = null, ResponseType responseType = ResponseType.HTTP_WEB_RESPONSE)
        {
            call(url, HttpMethod.PUT, args, handler, responseType);
        }

        public void delete(Uri url, Dictionary<string, object> args, EventHandler<RequestArgs> handler = null, ResponseType responseType = ResponseType.HTTP_WEB_RESPONSE)
        {
            call(url, HttpMethod.DELETE, args, handler, responseType);
        }

        private void call(Uri url, HttpMethod httpMethod, Dictionary<string, object> args, EventHandler<RequestArgs> handler, ResponseType responseType)
        {
            if (args != null && args.Keys.Count > 0 && httpMethod == HttpMethod.GET)
            {
                string urlString = url.ToString();
                url = new Uri(urlString + Util.encodeDictionaryToRESTQuery(args));
            }
            makeRequest(url, httpMethod, args, handler, responseType);
        }

        private void makeRequest(Uri url, HttpMethod httpMethod, Dictionary<string, object> args, EventHandler<RequestArgs> handler, ResponseType responseType)
        {
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = httpMethod.ToString();
            webRequest.AllowAutoRedirect = this.allowAutoRedirect;
            webRequest.AllowReadStreamBuffering = true;

            RequestState requestState = new RequestState();
            requestState.request = webRequest;
            requestState.httpMethod = httpMethod;
            requestState.handler = handler;
            requestState.responseType = responseType;

            switch (httpMethod)
            {
                case HttpMethod.GET:
                    break;
                case HttpMethod.POST:
                    requestState.sendData = buildPostData(args);
                    break;
                case HttpMethod.PUT:
                    requestState.sendData = buildPostData(args);
                    break;
                case HttpMethod.DELETE:
                    requestState.sendData = buildPostData(args);
                    break;
            }

            startRequest(requestState);
        }

        protected virtual byte[] buildPostData(Dictionary<string, object> args)
        {
            return Encoding.UTF8.GetBytes(Util.encodeJson(args));
            //return Encoding.UTF8.GetBytes(Util.encodeDictionaryToQuery(args, false));
        }

        protected virtual void startRequest(RequestState requestState)
        {
            try
            {
                HttpWebRequest request = requestState.request as HttpWebRequest;
                Util.debug(DEBUG_TAG, requestState.httpMethod.ToString() + " request: " + request.RequestUri.ToString());

                switch (requestState.httpMethod)
                {
                    case HttpMethod.GET:
                        request.BeginGetResponse(new AsyncCallback(getResponseCallback), requestState);
                        break;
                    case HttpMethod.POST:
                        request.BeginGetRequestStream(new AsyncCallback(setPostDataCallback), requestState);
                        break;
                    case HttpMethod.PUT:
                        request.BeginGetResponse(new AsyncCallback(getResponseCallback), requestState);
                        break;
                    case HttpMethod.DELETE:
                        request.BeginGetResponse(new AsyncCallback(getResponseCallback), requestState);
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                callRequestHandler(requestState, null, true);
            }
        }

        /// <summary>
        /// Realiza uma nova requisicao a partir do valor "Location" encontrado no header da resposta.
        /// </summary>
        /// <param name="requestState">
        /// A requisicao realizada
        /// </param>
        /// <param name="response">
        /// A resposta obtida
        /// </param>
        /// <remarks>
        /// Esse metodo e chamado quando uma resposta e obtida e o seu status e dado como Found (HTTP status 302).
        /// </remarks>
        protected virtual void onRedirectingResponseFound(RequestState requestState, HttpWebResponse response)
        {
            makeRequest(new Uri(response.Headers["Location"]), requestState.httpMethod, null, requestState.handler, requestState.responseType);
        }

        protected virtual void onResponseMovedPermanently(RequestState requestState, HttpWebResponse response)
        {
        }

        private void setPostDataCallback(IAsyncResult result)
        {
            RequestState state = (RequestState)result.AsyncState;
            HttpWebRequest request = (HttpWebRequest)state.request;
            byte[] sendData = state.sendData;

            Stream postStream = request.EndGetRequestStream(result);
            postStream.Write(sendData, 0, sendData.Length);
            postStream.Flush();
            postStream.Close();

            request.BeginGetResponse(new AsyncCallback(getResponseCallback), state);
        }

        private void getResponseCallback(IAsyncResult result)
        {
            RequestState requestState = (RequestState)result.AsyncState;
            HttpWebRequest request = (HttpWebRequest)requestState.request;

            if (!result.IsCompleted)
            {
                return;
            }

            if (request.HaveResponse)
            {
                bool failed = false;
                object responseData = null;
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        switch (requestState.responseType)
                        {
                            case ResponseType.JSON:
                                responseData = Util.decodeResponseToJson(response);
                                break;
                            case ResponseType.XML:
                                responseData = Util.decodeResponseToXML(response);
                                break;
                            case ResponseType.HTTP_WEB_RESPONSE:
                            default:
                                responseData = response;
                                break;
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.MovedPermanently)
                    {
                        onResponseMovedPermanently(requestState, response);
                        return;
                    }
                    else if (response.StatusCode == HttpStatusCode.Found)
                    {
                        onRedirectingResponseFound(requestState, response);
                        return;
                    }
                    else
                    {
                        failed = true;
                    }
                }
                catch
                {
                    failed = true;
                }
                callRequestHandler(requestState, responseData, failed);
            }
            else
            {
                callRequestHandler(requestState, null, true);
            }
        }

        protected virtual void callRequestHandler(RequestState requestState, object data, bool failed = false)
        {
            RequestArgs args = new RequestArgs(data, failed);
            if (requestState.handler != null)
            {
                requestState.handler(this, args);
            }
        }
    }
}
