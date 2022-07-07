using Newtonsoft.Json;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;

namespace RestSharpTestCase
{
    [TestClass]
    public class Employee
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? salary { get; set; }
    }
    [TestClass]
    public class RESTSharp
    {
        RestClient? client;

        [TestMethod]
        public void OnCallingGetMethod_CompareCount_ShouldReturnEmployeeList()
        {
            client = new RestClient("http://localhost:4000");
            //Arrange
            RestRequest request = new RestRequest("/Employee", Method.Get);
            //Act
            RestResponse response = client.Execute(request);
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Employee> list = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(4, list.Count);
            foreach (Employee value in list)
            {
                Console.WriteLine("Id:-" + value.id + " " + "NAME:-" + value.name + " " + "SALARY:-" + value.salary);
            }
        }
        [TestMethod]
        public void OnPostingEmployeeData_AddtoJsonServer_ReturnRecentlyAddedData()
        {
            client = new RestClient("http://localhost:4000");
            //Arrange
            RestRequest request = new RestRequest("/Employee", Method.Post);
            var body = new Employee { name = "JAGADEESH", salary = "60000" };
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            //Act
            RestResponse response = client.Execute(request);
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Employee exp = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("JAGADEESH", exp.name);
            Assert.AreEqual("60000", exp.salary);
            Console.WriteLine(response.Content);
        }
        [TestMethod]
        public void OnPostingMultipleEmployees_AddToJsonServer_ReturnListOfAddedData()
        {
            client = new RestClient("http://localhost:4000");
            //Arrange
            List<Employee> list = new List<Employee>();
            list.Add(new Employee { name = "AMMA", salary = "25000" });
            list.Add(new Employee { name = "NANNA", salary = "60000" });
            list.Add(new Employee { name = "CHITTI", salary = "40000" });
            list.ForEach(body =>
            {
                RestRequest request = new RestRequest("/Employee", Method.Post);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                //Act
                RestResponse response = client.Execute(request);
                //Assert
                Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
                Employee exp = JsonConvert.DeserializeObject<Employee>(response.Content);
                Assert.AreEqual(body.name, exp.name);
                Assert.AreEqual(body.salary, exp.salary);
                Console.WriteLine(response.Content);
            });
        }
        [TestMethod]
        public void OnUpdatingEmployeeData_ShouldUpdateValueInJsonServer()
        {
            client = new RestClient("http://localhost:4000");
            //Arrange
            RestRequest request = new RestRequest("/Employee/4", Method.Put);
            List<Employee> list = new List<Employee>();
            Employee body = new Employee { name = "lakshmi", salary = "70000" };
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            //Act
            RestResponse response = client.Execute(request);
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Employee exp = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("lakshmi", exp.name);
            Assert.AreEqual("70000", exp.salary);
            Console.WriteLine(response.Content);
        }
        [TestMethod]
        public void OnDeletingEmployeeData_ShouldDeleteDataInJsonServer()
        {
            client = new RestClient("http://localhost:4000");
            //Arrange
            RestRequest request = new RestRequest("/Employee/15", Method.Delete);
            //Act
            RestResponse response = client.Execute(request);
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Console.WriteLine(response.Content);
        }

    }

}