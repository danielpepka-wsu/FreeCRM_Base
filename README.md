# FileGpt

FileGpt is an ASP.NET Core and Blazor-based application designed to manage, preview, and analyze files on the server. It offers real‐time updates via SignalR, dynamic plugin support, and a modern, responsive web interface built with Blazor WebAssembly. FileGpt is especially useful for administrators and developers who need to quickly inspect file contents and metadata, manage multi-tenant settings, and extend functionality through custom plugins.

## Features

- **File Management API**  
  Retrieve file lists, full file contents, and file metadata (such as line and character counts) using REST endpoints. Caching is implemented to improve performance.

- **Real-Time Updates**  
  Uses SignalR to push live notifications to clients when data changes occur.

- **Dynamic Plugin Architecture**  
  Load and integrate custom plugins at startup to extend or modify application behavior.

- **Blazor Client Interface**  
  A modern, responsive user interface built with Blazor WebAssembly, MudBlazor, Radzen, and BlazorBootstrap.

- **Entity Framework Core Integration**  
  Supports multiple database types (SQL Server, MySQL, SQLite, PostgreSQL, or in-memory) for data persistence and tenant management.

- **Multi-Tenant Support and Customization**  
  Provides configuration for tenant-specific settings (including themes, language localization, and module visibility) along with built-in authentication and authorization.

## Project Structure

- **FileGpt/**  
  Contains server-side code, including:
  - **Program.cs** – Application startup and configuration.
  - **Controllers/** – API controllers for file operations, data management, and SignalR hub interactions.
  - **Components/** – Razor components such as the main `App.razor`.

- **FileGpt.Client/**  
  Contains the Blazor WebAssembly client application:
  - **Program.cs** – Client bootstrap and service registration.
  - **DataModel.cs** – Shared data model for managing state and user interface updates.
  - **Layout/MainLayout.razor** – The primary layout for the client interface.
  - **Pages/FileGpt/Files.razor** – A dedicated page for file browsing, filtering, and previewing.

- **FileGpt.DataAccess/**  
  Implements data access using Entity Framework Core, including database connection configuration and schema management.

- **FileGpt.DataObjects/**  
  Houses data transfer objects and models used across the project (including specialized objects for file operations).

- **FileGpt.EFModels/**  
  Contains the Entity Framework Core database models and configurations.

## Getting Started

### Prerequisites

- [.NET 6 SDK (or later)](https://dotnet.microsoft.com/download)
- A supported database server (SQL Server, MySQL, PostgreSQL, SQLite, or the in-memory provider for testing)
- Node.js (if client-side development requires additional tooling)

### Installation

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/yourusername/FileGpt.git
   cd FileGpt
   ```

2. **Configure the Application:**

   Update your configuration (e.g., `appsettings.json`) with your connection string, database type, tenant settings, and any plugin configuration required by your environment.

3. **Database Setup (if applicable):**

   For persistent databases (such as SQL Server or MySQL), run the Entity Framework Core migrations to set up the database schema:

   ```bash
   dotnet ef database update --project FileGpt.EFModels
   ```

4. **Run the Application:**

   Launch the server using:

   ```bash
   dotnet run --project FileGpt
   ```

   Once running, access the application in your browser at the configured URL (e.g., [https://localhost:5001](https://localhost:5001)).

## Usage

- **File Explorer:**  
  Navigate to the `/files` page to use the file explorer. You can enter a directory path, apply filters (including file type, wildcard, and plain text filters), and view a summary of file contents and metadata.

- **Administration:**  
  Use the administration interface to manage tenants, users, settings, and plugins. Real-time updates and SignalR notifications ensure that changes are reflected immediately across connected clients.

- **Customizations:**  
  Customize the user interface with tenant-specific themes and language settings. The plugin architecture allows further extension of functionality without altering core code.

## Customization and Extensions

FileGpt supports a plugin architecture that makes it easy to add new features or modify existing behavior. Plugins are dynamically loaded during startup and are available through dependency injection. For more details on extending the application, please review the documentation and inline comments provided in the Plugins folder and associated code files.

## Contributing

Contributions are welcome! To contribute:

- Fork the repository.
- Create a new branch for your feature or bug fix.
- Ensure your changes adhere to industry best practices.
- Submit a pull request with detailed explanations of your modifications.

## License

This project is licensed under the [MIT License](LICENSE) (or your project’s actual license).

## Assumptions & Future Improvements

**Assumptions:**
- The application operates in a multi-tenant environment with configurable tenant settings.
- File paths provided to API endpoints are assumed to be valid and accessible on the server.

**Future Improvements:**
- **Enhance the Plugin System:** Simplify plugin development and integration.
- **Improve Error Handling:** Add robust logging and error management across both client and server.
- **Expand Real-Time Functionality:** Extend SignalR notifications to cover additional events.
- **UI Enhancements:** Offer more customization options for themes and language support.
- **Testing:** Increase coverage with comprehensive unit and integration tests.
