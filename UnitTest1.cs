﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            List<EmployeeModel> employeeList = JsonConvert.DeserializeObject<List<EmployeeModel>>(response.Content);
            Assert.AreEqual(5, employeeList.Count);
            foreach (EmployeeModel emp in employeeList)
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
            EmployeeModel employee = JsonConvert.DeserializeObject<EmployeeModel>(response.Content);
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
            List<EmployeeModel> employeeList = new List<EmployeeModel>();
            employeeList.Add(new EmployeeModel { Name = "Aditya", Salary = "9876541" });
            employeeList.Add(new EmployeeModel { Name = "Girimal", Salary = "6543210" });
            employeeList.Add(new EmployeeModel { Name = "Parvathi", Salary = "123456" });
            //Iterate the loop for each employee
            foreach (var emp in employeeList)
            {
                ///Initialize the request for POST to add new employee
                RestRequest request = new RestRequest("/Employees", Method.POST);
                JsonObject jsonObj = new JsonObject();
                jsonObj.Add("Name", emp.Name);
                jsonObj.Add("Salary", emp.Salary);

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

        //UC4: Ability to Update Salary in Employee Payroll JSON Server.

        [TestMethod]
        public void OnCallingPutAPI_ReturnEmployeeObject()
        {
            //Arrange
            //Initialize the request for PUT to add new employee
            RestRequest request = new RestRequest("/Employees/3", Method.PUT);
            JsonObject jsonObj = new JsonObject();
            jsonObj.Add("name", "Shraddha");
            jsonObj.Add("salary", "65000");
            //Added parameters to the request object such as the content-type and attaching the jsonObj with the request
            request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            EmployeeModel employee = JsonConvert.DeserializeObject<EmployeeModel>(response.Content);
            Assert.AreEqual("Shraddha", employee.Name);
            Assert.AreEqual("65000", employee.Salary);
            Console.WriteLine(response.Content);
        }
        //UC5: Ability to Delete Employee from Employee Payroll JSON Server.
        [TestMethod]
        public void OnCallingDeleteAPI_ReturnSuccessStatus()
        {
            //Arrange
            //Initialize the request for PUT to add new employee
            RestRequest request = new RestRequest("/Employees/4", Method.DELETE);

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Console.WriteLine(response.Content);
        }
    }


}

