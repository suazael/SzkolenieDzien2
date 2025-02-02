﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P30AsyncAndAwait
{
    internal class WeatherForecastService
    {
        const string url = $"https://www.google.com/search?q=pogoda";
        const string regexTemplate = "<div class=\"BNeawe iBp4i AP7Wnd\">(-{0,1}\\d{1,3}).[CF]<\\/div>";

        public int GetTemperature(string city)
        {
            WebClient wc = new WebClient();
            string data = wc.DownloadString(url + " " + city);
            try
            {
                Regex rx = new Regex(regexTemplate, RegexOptions.IgnoreCase);
                Match match = rx.Match(data);
                string result = match.Groups[1].Value;
                return int.Parse(result);
            }
            catch (Exception)
            {
                throw new Exception("Error while parsing data");
            }

        }

        public async Task<int> GetTemperatureAsync(string city)
        {
            WebClient wc = new WebClient();
            string data = await wc.DownloadStringTaskAsync(url + " " + city);
            try
            {
                Regex rx = new Regex(regexTemplate, RegexOptions.IgnoreCase);
                Match match = rx.Match(data);
                string result = match.Groups[1].Value;
                return int.Parse(result);
            }
            catch (Exception)
            {
                throw new Exception("Error while parsing data");    
            }
        }

        public async Task<int> GetTemperatureAsync2(string city)
        {
            return await Task.Run(() =>
            {
                WebClient wc = new WebClient();
                string data = wc.DownloadString(url + " " + city);
                try
                {
                    Regex rx = new Regex(regexTemplate, RegexOptions.IgnoreCase);
                    Match match = rx.Match(data);
                    string result = match.Groups[1].Value;
                    return int.Parse(result);
                }
                catch (Exception)
                {
                    throw new Exception("Error while parsing data");
                }
            });
        }


        public async Task<int> GetTemperatureAsync3(string city)
        {
            using (WebClient wc = new WebClient())
            {
                string data = await wc.DownloadStringTaskAsync(url + " " + city);
                string result = await Task.Run(() =>
                {
                    Regex rx = new Regex(regexTemplate, RegexOptions.IgnoreCase);
                    Match match = rx.Match(data);
                    return match.Groups[1].Value;
                });

                return int.Parse(result);

            }
        }
          
        }
    }
 