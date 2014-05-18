using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GatewayChurch.API.Models;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace GatewayChurch.API.Networking
{
    public class GatewayService : IGatewayService
    {

        private readonly IGateWayServiceConfiguration config;
        private readonly IGateWayAPICaller network;

        public GatewayService(IGateWayServiceConfiguration config, IGateWayAPICaller network)
        {
            this.config = config;
            this.network = network;
        }

        // Poor man's DI
        public GatewayService() 
            : this (new GateWayServiceConfiguration(), new GateWayAPICaller())
        {
            
        }


        public void GetAuthors(Action<ICollection<Speaker>, Exception> CallBack)
        {
            GetCollectionFromNetwork<Speaker>(config.Authors, CallBack, (authors) =>
            {
                ICollection<Speaker> speakers = authors.Select(a => new Speaker
                {
                    Id = (string)a["id"],
                    Name = (string)a["name"]
                }).ToList();
                return speakers;
            });
        }

        public void GetYears(Action<ICollection<string>, Exception> CallBack)
        {
            GetCollectionFromNetwork<string>(config.Years, CallBack, (years)=>
                {

                    ICollection<string> yearCollection = new List<string>();
                    foreach(string y in years)
                    {
                        yearCollection.Add(y);
                    }
                       // years.Select(a => (string)a["year"]).ToList();
                    return yearCollection;
                });
        }

        public void GetSeries(Action<ICollection<Series>, Exception> CallBack)
        {
            GetCollectionFromNetwork<Series>(config.Series, CallBack, (series) =>
                {
                    ICollection<Series> seriesCollection = series.Select(a => new Series
                    {
                        Author = (string)a["author"],
                       Count = (int) a ["total_sermons"],
                       Date = (DateTime) a["date"],
                        Id = (string) a["id"],
                      //  ImagePath = (string) a["image"],
                        Title = (string) a["title"]
                    }).ToList();

                    return seriesCollection;
                });
        }

        public void GetLastestSermons(Action<ICollection<Sermon>, Exception> CallBack)
        {
           GetCollectionFromNetwork<Sermon>(config.RecentSermons, CallBack, (sermons) =>
               {
                   return SermonsFromJson(sermons);
               });
        }

        public void GetSermonBySeries(string seriesID, Action<ICollection<Sermon>, Exception> CallBack)
        {
            GetCollectionFromNetwork<Sermon>(config.SermonsBySeriesId(seriesID), CallBack, (sermons) =>
                {
                    ICollection<Sermon> sermonCollection = SermonsFromJson(sermons);
                    return sermonCollection;
                });
        }

        private static ICollection<Sermon> SermonsFromJson(JArray sermons)
        {
            ICollection<Sermon> sermonCollection = sermons.Select(a => new Sermon()
            {
                AudioLength = (int)a["audio_length"],
                AudioPath = (string)a["audio"],
                Author = (string)a["author"],
                Date = (DateTime)a["date"],
                Description = (string)a["description"],
                Id = (string)a["id"],
                ImageId = (string)a["image_id"],
                ImagePath = (string)a["image"],
                Title = (string)a["title"],
                VideoLength = (int)a["video_length"],
                VideoPath = (string)a["video"]
            }).ToList();

            return sermonCollection;
        }

        private void GetCollectionFromNetwork<T>(string URL, Action<ICollection<T>, Exception> CallBack, Func<JArray, ICollection<T>> Parse)
        {
            network.CallNetwork(URL, (rawResult, ex) =>
            {
                JArray listArray = JArray.Parse(rawResult);

                if (ex.IsNotNull())
                {
                    CallBack(null, ex);
                }
                else
                {
                    ICollection<T> returnList = Parse(listArray);
                    CallBack(returnList, null);
                }
            });
        }
    }
}
