Building and Running the Application
1.Open the Solution:

Open the solution file (CategoriesProcessorWithOpenAi.sln) in Visual Studio.

2.Restore Packages:

In Visual Studio, restore the NuGet packages by right-clicking on the solution in Solution Explorer and selecting "Restore NuGet Packages".

3.Run the Application:

Provide API key in appsettings.json. My free tier has been exceeded.

4. Steps to provide API key:

Goto appsettings.json and find 
  "OpenAI": {
    "Key": ""
  }
Replace your API Key in above 'Key' property of 'OpenAPI' object.

Press F5 or click the "IIS Express" button to run the application. The API will be hosted at https://localhost:5001 (or a different port if configured).

API Documentation
The application uses Swagger for API documentation. Once the application is running, you can access the Swagger UI at https://localhost:5001/swagger.

Sample API Usage
POST /CategoryProcessor/GetPopularAttributes

Request Body:

[
  {
    "categoryName": "TVs",
    "subCategories": [
      { "categoryId": 80, "categoryName": "TVs" },
      { "categoryId": 948, "categoryName": "All-Weather TVs" }
    ]
  }
]

Running Unit Tests
Unit tests are written using xUnit, FakeItEasy, and FluentAssertions. To run the tests:

Open Test Explorer:

In Visual Studio, go to Test > Test Explorer.

Run Tests:

Click the "Run All" button in Test Explorer to run all the tests.
Response Body:

[
  {
    "categoryId": 80,
    "attributes": ["attribute1", "attribute2", "attribute3"]
  },
  {
    "categoryId": 948,
    "attributes": ["attribute4", "attribute5", "attribute6"]
  }
]