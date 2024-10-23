# Mongorize

## Abstract

The purpose of this project is to provide a .NET library for integrating the **service/repository** pattern into a MongoDB database connection. This document serves as a guide to the key aspects of the project and how to use it properly.

## Goal

The service/repository pattern is widely used in the programming world, allowing a clear separation between the data access layer and the business logic layer. [This article](https://medium.com/@ankitpal181/service-repository-pattern-802540254019) explains the concept clearly and concisely.

This project aims to serve as both a starting point for applications and a valuable educational resource. It provides a clear representation and practical use of the pattern.

## SOLID Principles

The project has been developed with a strong focus on adhering to SOLID principles, structuring the code to ensure clarity, readability, maintainability, and extensibility over time. Great attention has also been paid to code cleanliness through tools like **StyleCop**, which ensures consistency and adherence to C# coding conventions.

The authors of this project strongly believe in these principles as the foundation for producing quality software.

## Getting Started

### Compile the library

#### Step 1

Clone the repository.

``` bash
git clone https://github.com/defra91/.NET-Mongorize.git
```

#### Step 2

Navigate to the Dockerfile for the compilation of the build image for the library.

``` bash
cd src
```

#### Step 3

Build the Docker image based on the located docker file.

``` bash
docker build -t dotnet-mongorize-compiler-image .
```

#### Step 4

Run the Docker container based on the compiled image.

``` bash
docker run --name dotnet-mongorize-compiler-container  dotnet-mongorize-compiler-image
```

### Run the demo project

Inside the `Demo` folder, you can find an example project that showcases how to use all the features of the **Mongorize** library, starting with well-defined sample entities.

#### Step 1

Navigate to the demo folder.

``` bash
cd .NET-mongorize/demo
```

#### Step 2

Compile the demo project.

``` bash
dotnet build
```

### Step 3

Create the `appsettings.json` file containing the mongo db configuration and place it inside the compiled demo project location (`.NET-mongorize/demo/bin/Debug/net8.0/appsettings.json`).

**here an example:**

``` json
{
    "MongoSettings": {
        "Connection": "mongodb://localhost:27017",
        "DatabaseName": "my-awesome-database",
        "LogQueries": true
    }
}
```

#### Step 4

Run the demo project.

``` bash
dotnet run
```

## Dockerfile

The project provides a `Dockerfile`, which is responsible for building the image used to compile the library. The image's sole purpose is to compile the library, so it can be integrated into a continuous integration pipeline.

## Unit tests

On the folder `test` you will find the collection of all the unit tests that have been written for the library.