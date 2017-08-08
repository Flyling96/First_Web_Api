using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace Web_api_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Client app, wait for service");
                Console.ReadLine();
                //ReadArraySample().Wait();
                // ReadSampleXml().Wait();
                // AddSample().Wait();
                AddSample().Wait();
                // UpdateSample().Wait();
                // DeleteSample().Wait();
                Console.ReadLine();
                UpdateWithErrorSample().Wait();
          
                Console.ReadLine();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadLine();
            }

        }

        private static async Task DeleteSample()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:11825");

                HttpResponseMessage response =
                  await client.DeleteAsync("/api/BookChapters/42");

                response.EnsureSuccessStatusCode();

                await ReadArraySample();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task UpdateWithErrorSample()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://120.27.7.115:81");

                var updatedChapter = new BookChapter
                {
                    Title = "NotExisting",
                    Number = 88,
                    Pages = 50
                };

                await client.PutAsJsonAsync("/api/BookChapters/88", updatedChapter);

                await ReadArraySample();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private static async Task UpdateSample()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://120.27.7.115:81");

            var updatedChapter = new BookChapter
            {
                Title = "Visual Studio 2013",
                Number = 17,
                Pages = 50
            };

            await client.PutAsJsonAsync("/api/BookChapters/17", updatedChapter);

            await ReadArraySample();
        }


        private static async Task AddSample()
        {
            var newUser = new User
            {
                Account = "963452757",

                Password = "960513"

            };

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:12717");

            HttpContent content = new ObjectContent<User>(
              newUser, new JsonMediaTypeFormatter());

            HttpResponseMessage response =
              await client.PostAsync("/api/Account", content);

            response.EnsureSuccessStatusCode();

            await ReadString();
        }


        private static async Task ReadWithExtensionsSample()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:11825");
            HttpResponseMessage response =
              await client.GetAsync("/api/BookChapters/3");
            BookChapter chapter =
              await response.Content.ReadAsAsync<BookChapter>();

            Console.WriteLine("{0}. {1}", chapter.Number, chapter.Title);
        }


        private static async Task ReadSampleXml()
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:11825");
            client.DefaultRequestHeaders.Accept.Add(
              new MediaTypeWithQualityHeaderValue("application/xml"));

            string response = await client.GetStringAsync("/api/BookChapters/3");

            XElement chapter = XElement.Parse(response);

            Console.WriteLine("{0}", chapter);
        }

        private static async Task ReadString()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:12717");
            string response = await client.GetStringAsync("/api/Account");
            Console.WriteLine(response);
        }

        private static async Task ReadArraySample()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://120.27.7.115:81");
            string response = await client.GetStringAsync("/api/BookChapters/1");
            Console.WriteLine(response);
            var serializer = new JavaScriptSerializer();
            BookChapter[] chapters =
              serializer.Deserialize<BookChapter[]>(response);

            foreach (BookChapter chapter in chapters)
            {
                Console.WriteLine(chapter.Title);
            }
        }

    }
}
