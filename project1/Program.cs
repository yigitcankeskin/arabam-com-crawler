using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;


namespace project1
{
    public class Product
    {
       public String link;
       public String productTitle;
       public Double price;
    }
    class Program
    {
        static void Main(string[] args)
        {
            String baseUrl = "https://www.arabam.com/";
            String XpathBase = "//*[@id='wrapper']/div[2]/div[2]/div/div[2]/div/div[1]/div";
            List<Product> results = new List<Product>();    // İlanların link, başlık ve fiyatlarının listelenmesi için tanımlanmış 

            try
            {
                // Program hakkında bilgilendirme yazısının yazdırılması
                Entrence();

                // Anasayfadan Linklerin Alınması
                var links = GetLinks(baseUrl,XpathBase);

                // İlan Detaylarının Alınması ve Listeye Kaydedilmesi 
                foreach (var link in links)
                {
                    Console.Write("-");    // Websiteden veriler çekilirken ilerlemeyi göstermesi amaçlı yazılmıştır. 
                    System.Threading.Thread.Sleep(1500);   
                    results.Add(GetProductİnformations(link));
                }

                // İlan İsimleri ve Fiyatlarının Console ekranında gösterilmesi
                foreach(var product in results)
                {
                    Console.WriteLine("\n"+"İlan İsmi : " +product.productTitle+"  İlan Fiyatı : "+product.price);
 
                }

                // Tüm fiyatların ortalamasının ekranda gösterilmesi 
                Console.WriteLine("Fiyat Ortalaması : "+GetAvarageNumber(results));


                // İlan İsimlerinin ve Fiyatlarının Txt Belgesine kaydedilmesi
                SaveTxtFile(results);

            }
            catch(NullReferenceException e) 
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\nStackTrace : "+ e.StackTrace);
            }

        }


        private static void SaveTxtFile(List<Product> results)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\result.txt"))
                {
                    foreach (var result in results)
                    {
                        file.WriteLine("İlan İsmi : "+result.productTitle+ "\t\t İlan Fiyatı : "+result.price);
                    }
                }
                Console.WriteLine("Text belgesi başarıyla oluşturuldu.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private static Double GetAvarageNumber(List<Product> results)
        {
            try
            {
                Double avarageNumber = 0, count = 0;

                foreach (var product in results)
                {
                    avarageNumber += product.price;
                    count++;
                }
                return avarageNumber / count;
            }
            catch(Exception e) { throw e; }
           
        }
        private static Product GetProductİnformations(String url)
        {
            try {

                var baseUrl = "https://www.arabam.com" + url;
                var xPath = "//*[@id='wrapper']/div[2]/div[3]/div/div[1]";

                Product p = new Product(); // Link - İlan başlığı - Fiyat bilgilerinin olduğu object dosyasının yaratılması.

                HtmlAgilityPack.HtmlNodeCollection node = GetNode(baseUrl, xPath); // İstenen Node belegsinin çekilmesi


                // Çekilen node dosyasının htmlDocProduct'a yüklenmesi.
                var htmlDocProduct = new HtmlDocument();
                var html = node[0].InnerHtml;
                htmlDocProduct.LoadHtml(html);


                //İlan Başlığı
                var htmlNodeTitle = htmlDocProduct.DocumentNode.SelectNodes("//p");

                p.productTitle = htmlNodeTitle[0].InnerText;


                // İlan fiyatı
                var htmlNodePrice = htmlDocProduct.DocumentNode.SelectNodes("//*[@id='js-hook-for-observer-detail']/div[2]/div[1]/div/span");

                p.price = Int32.Parse(string.Join("", htmlNodePrice[0].InnerText.ToCharArray().Where(Char.IsDigit)));


                // İlan Linki
                p.link = baseUrl;


                return p;

            }
            catch (Exception e) { throw e; }
        }
        private static List<String> GetLinks(String url,String xPath)
        {
            try
            {
                var links = new List<String>(); // AnaSayfadaki ilanlarının linklerinin kaydedildiği liste


                HtmlAgilityPack.HtmlNodeCollection node = GetNode(url, xPath);

                var htmlDocProduct = new HtmlDocument();
                foreach (var product in node)
                {

                    var html = product.InnerHtml;
                    htmlDocProduct.LoadHtml(html);
                    var htmlNodes = htmlDocProduct.DocumentNode.SelectNodes("//div/div/div[1]/a").First().Attributes["href"].Value;
                    links.Add(htmlNodes);

                }
                return links;
            }
            catch(Exception e) { throw e; }
          

        }
        
        private static HtmlAgilityPack.HtmlNodeCollection GetNode(String url,String xPath)
        {
            try { 
            var baseUrl = url;
            HtmlWeb webSite = new HtmlWeb();
            HtmlDocument htmlDoc = webSite.Load(baseUrl); // Websiteden verilerin 
            var node = htmlDoc.DocumentNode.SelectNodes(xPath);  //Çekilen verilerin arasından istenilen verilerin ayrıştırılması

                if(node == null) // Veri Çekildi'mi çekilmedi'mi kontrolü 
                {
                    throw new Exception(" Hedeflenen Xpath yolunda herhangi bir eşleşme bulunmadı. Url'yi ve Xpath'i Kontrol ediniz.");
                }

            return node;
            }
            catch (Exception e) {
                throw e ;
            }
        }
        private static void Entrence()
        {
            Console.WriteLine("---  Arabam.com Anasayfa Vitrini İlanlarını Listeleme  --- ");
            Console.WriteLine("\n"+"İlanlar Listeleniyor...");

        }
    }
}