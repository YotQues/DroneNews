# DroneNews


## Hello Ori, and more friends at VOOM!
I prepared this home assignment for you as quickly as I could, aiming to maintain good code quality, considering my knowledge of your tech stack.

We have an ASP.NET server that connects to a remote SQL Server Database with EFCore as the backend. For the front end, we have a simple React.js (TypeScript) app with a custom hook to manage our backend calls, some MaterialUI, and some custom styles.

You will find some unit tests for the front end, as per one of the 'Mix and Match Bonuses'.

For the backend, I tested my code with logging and SwaggerUI/Postman.

Please feel free to contact me with any questions that may arise. For now, have a quick read to help you set up the project.

### Run With Visual Studio
```
1. Set Startup Project as DroneNews.Server and dronenews.client
2. Press the 'Start' button in the debug section
```

 ### Run Backend
 From the solution root directory
 ```sh
dotnet run --project="./DroneNews.Server/DroneNews.Server.csproj"
```
 
 ### Run Client
 From the solution root directory
 ```sh
cd ./dronenews.client
npm run dev
```

### Test Client
From the solution root directory
 ```sh
cd ./dronenews.client
npm run test
```
