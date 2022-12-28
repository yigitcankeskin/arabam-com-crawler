# arabam-com-crawler
Scraping car details from arabam.com and saves as txt

Crawling Uygulaması

Bir C# Konsol uygulaması örneğidir.

Proje içerisinde Gerekli verilerin websitesinden çekilmesi amacıyla HtmlAgilityPack  Paketi kullanılmıştır.

Programda Arabam.com Web sayfasındaki vitrindeki ilanların İlan başlığı ve Fiyat bilgilerinin alınması , 
Ortalama Fiyat analizinin oluşturulması hedeflenmiştir.


Program Öncelikle HtmlAgilityPack Paketi ile Arabam.com Web sitesinin Vitrinindeki ilan Linklerini Xpath Yordamı ile bir listeye kaydeder.
Sonrasında Bu linklerin her birine giderek Gerekli Fiyat bilgisi ve ilan başlığını da aynı listeye Product objesi türünde ekler. ilanlar arasında Geçerli siteden olası Ip banlarını 
önlemek amaçlı her istekden önce 1.5 saniye beklemektedir. Tüm ilan Başlığı ve Fiyat bilgileri Console ekranına yazdırılır. 
Almış Olduğumuz bilgilerin tutulduğu Listedeki Fiyat bilgileri işlenerek ortalama fiyat bilgisine erişilir ve ekranda gösterilir. 
Son olarak tüm ilan isimlerinin ve fiyatlarının bulunduğu bir txt belgesi oluşturulur ve kaydedilir. 


Website'den olası İp banlarını önlemek amaçlı, Olabildiğince siteye minimum istek atmaya çalışılmış ve ardı ardına atılması gereken isteklerde 1.5 saniye gecikme eklenmştir.


Projenin doğru bir şekilde çalışabilmesi için Visual Studio programının Yönetici olarak çalıştırılması gerekmektedir. 
Aksi Takdirde Windowsun getirdiği önlemlerden dolayı Text Dosyası oluşturulamamaktadır.
