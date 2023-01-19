# MoviesAPI
<li>Swagger</li>

     Swagger is a set of open-source tools that help developers design, build, document, and consume RESTful web services. 
     The most widely used tool in the Swagger ecosystem is the Swagger Editor, a web-based tool for designing, building, 
     and documenting RESTful APIs. 
     The Swagger specification, which is a part of the OpenAPI Initiative, is the foundation of the Swagger tooling. 
     The specification defines a format for describing the structure and operations of RESTful APIs.

<hr>
<li>Cors</li>

     CORS (Cross-Origin Resource Sharing) is a security feature implemented by web browsers. It controls whether a web 
     page can make requests to a different domain from the one that served the web page. This is done by adding HTTP
     headers to the server response that indicate which origins are allowed to access the server's resources.

     When a web page makes a request to a server, the browser sends an Origin header with the request, which indicates 
     the origin of the web page making the request. The server can then decide whether to allow the request by examining 
     the Origin header and comparing it to a list of allowed origins. If the origin is not on the list,the server sends 
     a response with an Access-Control-Allow-Origin header set to the origin of the web page, allowing the browser to
     make the request.

     CORS is often used to allow web pages to access resources on a different domain, such as a REST API. Without CORS,
     web pages would only be able to make requests to the same domain that served the web page, which would limit
     the functionality of web applications.

<hr>
<li>Add Database Model</li>
     
     you need to install some packages like: 
     1- Microsoft.EntityFrameworkCore
     2- Microsoft.EntityFrameworkCore.SqlServer => or the suitable package for the type you use 
     3- Microsoft.EntityFrameworkCore.Tools
     
     To connect your app with database you need to follow this steps:
     1- install the above packages
     2- create ApplicationDbContext and make it inherat from DbContext
     3- go to appsetting.json and add your ConnectionString 
     4- add your service in program.cs
