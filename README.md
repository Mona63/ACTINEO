# ACTINEO

ACTINEO is a simple microservice to Create, Read, Update, and Delete car ads written in C#. In this project, I strived to use onion architecture.
.
## Cloning

To clone the source code on your computer in order to build and test it, you can use `git` command-line interface.

```bash
git clone https://github.com/Mona63/ACTINEO.git
```

## Building

Before building ACTINEO, you need to have DotNet 5.0 installed on your computer. If you haven't, don't worry, please check [.net 5.0 download page](https://dotnet.microsoft.com/en-us/download/dotnet/5.0).

If you are using Visual Studio or JetBrains Rider, you can open the solution and build the project without extra headache. However, if you are a bash nerd here is an example.

```bash
cd ACTINEO
dotnet build
```

## Testing

I've written some Unit and Integration tests for this project. You can use your IDE or `dotnet` CLI to run them, here is an example that uses the command line.

```bash
dotnet test
```

## How to test the APIs?

ACTINEO provides live documentation for APIs via swagger. It's an easy way to explore the available APIs and execute them in a playground. To see the swagger page simply run the app using your favorite IDE or use the command line as follows. 

*Note* For development purposes I'm using .net default certificate, if you haven't already installed that use the command below.
```bash
dotnet dev-certs https --trust
```

```bash
dotnet run --project src\ACTINEO.Web\ACTINEO.Web.csproj
```

Now you can open https://localhost:5001/swagger/index.html in your browser.

## Technologies

Sqlite (https://www.sqlite.org/)

Asp.net (https://dotnet.microsoft.com/en-us/apps/aspnet)<br />
Entity Framework Core (https://docs.microsoft.com/en-us/ef/)

NUnit (https://nunit.org/)<br />
FluentAssertion	(https://fluentassertions.com/)

## Thanks

Thanks for taking the time and for reading the document. Please don't hesitate to contact me if you have further questions.