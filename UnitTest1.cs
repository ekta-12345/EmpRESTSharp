using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace EmployeePayrollProblem_RESTSharp
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            //Initialize the base URL to execute requests made by the instance
            client = new RestClient("http://localhost:3000");
        }
        private IRestResponse GetEmployeeList()
        {
            //Arrange
            //Initialize the request object with proper method and URL
            RestRequest request = new RestRequest("/Employees", Method.GET);
            //Act
            // Execute the request
            IRestResponse response = client.Execute(request);
            return response;
        }


        // UC1 Retrieve all employee details in json server

        [TestMethod]
        public void OnCallingGetAPI_ReturnEmployeeList()
        {
            IRestResponse response = GetEmployeeList();
            // Check if the status code of response equals the default code for the method requested
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            // Convert the response object to list of employees
            List<Employee> employeeList = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(5, employeeList.Count);
            foreach (Employee emp in employeeList)
            {
                Console.WriteLine("Id:- " + emp.Id + "\t" + "Name:- " + emp.Name + "\t" + "Salary:- " + emp.Salary);
            }
        }
        // UC2: Ability to add a new Employee to the EmployeePayroll JSON Server.
              

        [TestMethod]
        public void OnCallingPostAPI_ReturnEmployeeObject()
        {
            //Arrange
            ///Initialize the request for POST to add new employee
            RestRequest request = new RestRequest("/Employees", Method.POST);
            JsonObject jsonObj = new JsonObject();
            jsonObj.Add("name", "Pallavi");
            jsonObj.Add("salary", "150000");
            jsonObj.Add("id", "9");
            ///Added parameters to the request object such as the content-type and attaching the jsonObj with the request
            request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Employee employee = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Pallavi", employee.Name);
            Assert.AreEqual("150000", employee.Salary);
            Console.WriteLine(response.Content);
        }
        //UC3: Ability to add multiple Employee to  the EmployeePayroll JSON Server.
              
        /// </summary>
        [TestMethod]
        public void OnCallingPostAPIForAEmployeeListWithMultipleEMployees_ReturnEmployeeObject()
        {
            // Arrange
            List<Employee> employeeList = new List<Employee>();
            employeeList.Add(new Employee { Name = "Aditya", Salary = "9876541" });
            employeeList.Add(new Employee { Name = "Girimal", Salary = "6543210" });
            employeeList.Add(new Employee { Name = "Parvathi", Salary = "123456" });
            //Iterate the loop for each employee
            foreach (var emp in employeeList)
            {
                ///Initialize the request for POST to add new employee
                RestRequest request = new RestRequest("/Employees", Method.POST);
                JsonObject jsonObj = new JsonObject();
                jsonObj.Add("name", emp.Name);
                jsonObj.Add("salary", emp.Salary);
                ///Added parameters to the request object such as the content-type and attaching the jsonObj with the request
                request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);

                //Act
                IRestResponse response = client.Execute(request);

                //Assert
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                EmployeeModel employee = JsonConvert.DeserializeObject<EmployeeModel>(response.Content);
                Assert.AreEqual(emp.Name, employee.Name);
                Assert.AreEqual(emp.Salary, employee.Salary);
                Console.WriteLine(response.Content);
            }
        }
    }
 }

