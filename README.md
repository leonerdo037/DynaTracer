## DynaTracer

  DynaTracer is a windows client, powered by the Dynatrace REST API. The client will automatically update on every new release. The basic classes used to initiate the API calls and store data, are provided in this repository. These classes can be used to consume the Dynatrace APIs; they are written in C# along with XML comments to accelerate development time.
  
### Download: [DynaTracer pre-alpha v1.1.1.0.zip](https://github.com/leonerdo037/DynaTracer/files/1951218/DynaTracer.pre-alpha.v1.1.1.0.zip)

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

* Download the package from the above link.
* Install the program by running the 'setup.exe' file.
* Create a new profile with your Dynatrace instance information and that's it.

#### Class Usage:

1. Download the 'Classes' folder and import them into your Visual Studio project.
2. All the API calls can be made by creating an object for the class 'Dynatrace API'.
