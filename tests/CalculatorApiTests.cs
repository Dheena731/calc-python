using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CalculatorApiTests
{
    public class Tests
    {
        private HttpClient client;

        [SetUp]
        public void Setup()
        {
            client = new HttpClient();
            client.BaseAddress = new System.Uri("http://localhost:8000");
        }

        private async Task<double> CallEndpoint(string endpoint, double a, double b)
        {
            var query = $"?a={a}&b={b}";
            var response = await client.GetAsync(endpoint + query);
            response.EnsureSuccessStatusCode();

            var resultObj = System.Text.Json.JsonDocument.Parse(
                await response.Content.ReadAsStringAsync()
            );

            if (resultObj.RootElement.TryGetProperty("error", out var error))
            {
                throw new System.Exception($"API returned error: {error.GetString()}");
            }

            return resultObj.RootElement.GetProperty("result").GetDouble();
        }

        [Test]
        public async Task TestAdd()
        {
            var result = await CallEndpoint("/add", 3, 5);
            Assert.AreEqual(8, result);
        }

        [Test]
        public async Task TestSubtract()
        {
            var result = await CallEndpoint("/subtract", 10, 4);
            Assert.AreEqual(6, result);
        }

        [Test]
        public async Task TestMultiply()
        {
            var result = await CallEndpoint("/multiply", 6, 7);
            Assert.AreEqual(42, result);
        }

        [Test]
        public async Task TestDivide()
        {
            var result = await CallEndpoint("/divide", 20, 5);
            Assert.AreEqual(4, result);
        }

        [Test]
        public void TestDivideByZero()
        {
            Assert.ThrowsAsync<System.Exception>(async () =>
            {
                await CallEndpoint("/divide", 10, 0);
            });
        }

        [Test]
        public async Task TestPower()
        {
            var result = await CallEndpoint("/power", 2, 3);
            Assert.AreEqual(8, result);
        }

        [Test]
        public async Task TestModulo()
        {
            var result = await CallEndpoint("/modulo", 10, 3);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void TestModuloByZero()
        {
            Assert.ThrowsAsync<System.Exception>(async () =>
            {
                await CallEndpoint("/modulo", 10, 0);
            });
        }

        [Test]
        public async Task TestAverage()
        {
            var result = await CallEndpoint("/average", 10, 20);
            Assert.AreEqual(15, result);
        }
    }
}
