<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#Environment">Environment</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
  </ol>
</details>


<!-- ABOUT THE PROJECT -->
## About The Project

### Built With

* [C#](https://dotnet.microsoft.com/pt-br/languages/csharp)
* [Visual Studio](https://visualstudio.microsoft.com/pt-br/)

<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

* .NET >= 8.0.0
* Docker (with compose tool)

### Environment

1. Sample of appsettings.json file
   ```json
   {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*",
      "Mongo": {
        "ConnectionString": "mongodb://localhost:27017",
        "DatabaseName": "xpchallenge"
      }
    }
   ```

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/MSaulo/xp-challenge.git
   ```

2. Run Docker with compose
   ```sh
   docker compose up
   ```

3. Restore NuGet Packages:
    ```sh
    dotnet restore
    ```

4. Build the Project:
    ```sh
    dotnet build
    ```

5. Run the Project:
    ```sh
    dotnet run --urls=http://localhost:5160
    ```

6. Access the Application: [http://localhost:5160](http://localhost:5160)