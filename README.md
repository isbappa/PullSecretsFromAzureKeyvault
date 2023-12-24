# Project Description

This project demonstrates how to securely load and retrieve secrets from Azure Key Vault into a .NET application. It includes a practical implementation for injecting configuration settings into `appsettings.json` before application startup and an efficient method to pull all stored secrets from Azure Key Vault.

## Prerequisites

	- An Azure subscription with an active Key Vault resource
	- Service Principal with access to Key Vault
	- .NET 8.0 SDK and a compatible IDE (like Visual Studio or VS Code)

## Getting Started

To use this project in your environment, follow these steps:

1. Clone the project repository from GitHub.
2. Open the solution in your preferred .NET IDE.
3. In appsettings.json, fill in the TenantId, ClientId, and ClientSecret under SpnConfiguration, and set your Key Vault endpoint in KeyVaultEndpoint.

## Running the code

1. Build the project to resolve dependencies.
2. Execute the application - either through the IDE or by navigating to the project directory and running `dotnet run`.
3. The application will authenticate with Azure Key Vault using the provided Service Principal credentials and load secrets into `appsettings.json`.
4. It will also enumerate and display all the secrets stored in the specified Azure Key Vault.

## Usage

This application is a template for managing application secrets in .NET using Azure Key Vault. It's ideal for projects requiring secure, centralized management of configuration data.

## Contributing

This project welcomes contributions and suggestions.
Feel free to submit a pull request if you find any bugs or have any suggestions for improvement.