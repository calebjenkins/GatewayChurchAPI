using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using GatewayChurch.API;
using System.Threading.Tasks;

namespace GatewayChurch.API.Networking
{
    public class GateWayAPICaller : IGateWayAPICaller
    {

        private readonly static string request_body_used_for_each_api_call = @"<?xml version=""1.0"" encoding=""UTF-8"" ?><!DOCTYPE plist PUBLIC ""-//Apple//DTD PLIST 1.0//EN"" ""http://www.apple.com/DTDs/PropertyList-1.0.dtd""><plist version=""1.0""><dict><key>device_token</key><string><![CDATA[d5006c472c4dc0a7d05d0b879cc581ad]]></string></dict></plist>";
        public void CallNetwork(string URL, Action<string, Exception> CallBack)
        {
            CallBackWrapper wrapper = new CallBackWrapper(CallBack);
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(URL);

            wr.Method = HttpMethod.Post;
            wr.ContentType = HttpContentType.FormUrlEncoded;
            // wr.ContentLength = 397;
            wrapper.WebRequest = wr;
            wr.BeginGetRequestStream(requestStreamCallback, wrapper);
            // allDone.WaitOne();
        }

        private async void requestStreamCallback(IAsyncResult asynchronousResult)
        {
            CallBackWrapper wrapper = (CallBackWrapper)asynchronousResult.AsyncState;
            HttpWebRequest request = wrapper.WebRequest;
            Stream postStream = request.EndGetRequestStream(asynchronousResult);

            string postData = request_body_used_for_each_api_call;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            postStream.Write(byteArray, 0, postData.Length);
            //postStream.Close();
            wrapper.WebRequest = request;
            try
            {
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                Stream responseStream = response.GetResponseStream();
                string data;
                using (var reader = new StreamReader(responseStream))
                {
                    data = reader.ReadToEnd();
                }

                wrapper.CallBack(data, null);
            }
            catch (Exception ex)
            {
                wrapper.CallBack(null, ex);
            }
            // request.BeginGetResponse(responseCallback, wrapper);
        }

        private void responseCallback(IAsyncResult asynchronousResult)
        {
            CallBackWrapper wrapper = (CallBackWrapper)asynchronousResult.AsyncState;

            HttpWebRequest request = wrapper.WebRequest;
            
            
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);

            Stream streamResponse = response.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);
            string responseText = streamRead.ReadToEnd();

                wrapper.CallBack(responseText, null);


                streamRead.IfNotNullDispose();
                streamResponse.Flush();
                streamResponse.IfNotNullDispose();
                response.IfNotNullDispose();

            // streamResponse.Close();
           // streamRead.Close();
           // response.Close();
            // allDone.Set();
        }
    }
}
