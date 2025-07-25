using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SnowflakeTestApp.Tests.Metadata
{
    /// <summary>
    /// Integration tests for the datasets metadata endpoints.
    /// These tests document the expected behavior and can be used to verify the endpoints manually.
    /// </summary>
    [TestClass]
    public class DataSetsMetadataEndpointIntegrationTest : BaseIntegrationTest
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            EnsureApplicationIsRunning();
        }

        /// <summary>
        /// Test the /$metadata.json/{dataset} endpoint with authentication
        /// </summary>
        [TestMethod]
        public async Task GetDataSetMetadataEndpoint_WithAuth_ReturnsOk()
        {
            var testToken = GetTestToken();
            HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {testToken}");

            var response = await HttpClient.GetAsync($"{BaseUrl}/$metadata.json/datasets");
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.IsFalse(string.IsNullOrEmpty(content), "Response content should not be empty");
        }

        /// <summary>
        /// Test the /$metadata.json/{dataset} endpoint without authentication
        /// </summary>
        [TestMethod]
        public async Task GetDataSetMetadataEndpoint_WithoutAuth_ReturnsOk()
        {
            var response = await HttpClient.GetAsync($"{BaseUrl}/$metadata.json/datasets");
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Test the /$metadata.json/{dataset} endpoint with non-existent dataset
        /// </summary>
        [TestMethod]
        public async Task GetDataSetMetadataEndpoint_WithNonExistentDataset_ReturnsNotFound()
        {
            var testToken = GetTestToken();
            HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {testToken}");

            var response = await HttpClient.GetAsync($"{BaseUrl}/$metadata.json/nonexistent");
            
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
} 
