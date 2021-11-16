using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;
using System.Diagnostics;
using System.Web;

namespace CryptoTracking_WPF
{
    class Scraper
    {
        private ObservableCollection<EntryModel> _entries = new ObservableCollection<EntryModel>();
        public ObservableCollection<EntryModel> Entries
        {
            get { return _entries; }
            set { _entries = value; }
        }

        public void ScrapeData(string page)
        {
            var web = new HtmlWeb();
            var doc = web.Load(page);

            var articles = doc.DocumentNode.SelectNodes("//*[@class = '_2mHoLKk1EmQ90Hj2VxwVKC _2cEwIPLxNCKyZSBxk7MyUQ Q0Cxwokka8qzW-qAyjdq6 pointer']");

            foreach(var article in articles)
            {
                var header = HttpUtility.HtmlDecode(article.SelectSingleNode(".//div[@class = 'text-left truncate _1hazOxgsUXq0rb-UgDZwNp _1GdBC6rgsSADLryaaGeEuX w8u1-Ks6zzfWwPQ23ywUj _36FIyjphKz71izCg1N-Uks']").InnerText);
                var amount = HttpUtility.HtmlDecode(article.SelectSingleNode(".//div[@class = 'text-right _1hazOxgsUXq0rb-UgDZwNp LNc8C7U5Q_4hVq8G7HQHa _36FIyjphKz71izCg1N-Uks overflow-visible']").InnerText);

                Debug.Print($"Name: {header}\nAmount: {amount}");

                _entries.Add(new EntryModel { Title = header, Description = amount });
            }
        }
    }
}
