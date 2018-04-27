## DynaTracer

  DynaTracer is a windows client, powered by the Dynatrace REST API. The client will automatically update on every new release. The basic classes used to initiate the API calls and store data, are provided in this repository. These classes can be used to consume the Dynatrace APIs; they are written in C# along with XML comments to accelerate development time.
  
### Download: [DynaTracer Setup](https://github.com/leonerdo037/DynaTracer/releases/download/1.1.1.0/DynaTracer_Setup.exe)

#### Note: The setup will always install the latest version, releases are created only for tracking issues and changelogs.

### Features:

* Supported APIs: Problem, Timeseries and Topology.
* Supportes both Dynatrace SaaS and Managed deployment types.
* Easy options to search and fetch data on the environment.
* Ability to download data as CSV files.
* Graphical visualization of data using charts.
* Provides insight on the infrastructure.
* Secured with SSL and encryption.
* Automatically updates on new releases.

### Installation:

* Download the setup from the above link.
* Install the program by running the 'DynaTracer_Setup.exe' file.
* Create a new profile with your Dynatrace instance information and enjoy.

#### Class Usage:

1. Download the 'Classes' folder and import them into your Visual Studio project.
2. All the API calls can be made by creating an object for the class 'Dynatrace API'.
