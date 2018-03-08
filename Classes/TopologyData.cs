using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public class TopologyData
{
	public class Hosts
	{
		public class Tag
		{
			public string context { get; set; }
			public string key { get; set; }
			public string _value
			{
				get
				{
					string output = "NA";
					try
					{
						output = Handler_String.SplitStr(key, "-", 1);
					}
					catch { }
					return output;
				}
			}
			#endregion
		}

		public class FromRelationships
		{
			public List<string> isNetworkClientOfHost { get; set; }
		}

		public class ToRelationships
		{
			public List<string> isProcessOf { get; set; }
			public List<string> isSiteOf { get; set; }
			public List<string> isNetworkClientOfHost { get; set; }
			public List<string> runsOn { get; set; }
		}

		public class AgentVersion
		{
			public int major { get; set; }
			public int minor { get; set; }
			public int revision { get; set; }
			public string sourceRevision { get; set; }
			public string timestamp { get; set; }
		}

		public class RootObject
		{
			public string entityId { get; set; }
			public string displayName { get; set; }
			public string discoveredName { get; set; }
			public long firstSeenTimestamp { get; set; }
			public long lastSeenTimestamp { get; set; }
			public List<Tag> tags { get; set; }
			public FromRelationships fromRelationships { get; set; }
			public ToRelationships toRelationships { get; set; }
			public string osType { get; set; }
			public string osArchitecture { get; set; }
			public string osVersion { get; set; }
			public string hypervisorType { get; set; }
			public List<string> ipAddresses { get; set; }
			public string bitness { get; set; }
			public int cpuCores { get; set; }
			public int logicalCpuCores { get; set; }
			public string cloudType { get; set; }
			public string monitoringMode { get; set; }
			public AgentVersion agentVersion { get; set; }
			public double consumedHostUnits { get; set; }
			public string azureVmName { get; set; }
			public string localHostName { get; set; }
			public string awsInstanceType { get; set; }
			public string awsInstanceId { get; set; }
			public List<string> awsSecurityGroup { get; set; }
			public string localIp { get; set; }
			public string publicIp { get; set; }
			public string amiId { get; set; }
			public string _firstSeenTimestamp
			{
				get { return Handler_String.EpochConverter(firstSeenTimestamp).ToString(); }
			}
			public string _lastSeenTimestamp
			{
				get { return Handler_String.EpochConverter(lastSeenTimestamp).ToString(); }
			}
			public string _agentVersion
			{
				get
				{
					try
					{
						return string.Format("{0}.{1}.{2}", agentVersion.major.ToString(), agentVersion.minor.ToString(), agentVersion.revision.ToString());
					}
					catch { return "NA"; }
				}
			}
			public string _agentRevision
			{
				get
				{
					try
					{
						return agentVersion.sourceRevision;
					}
					catch { return "NA"; }
				}
			}
			public string _agentTime
			{
				get
				{
					try
					{
						return agentVersion.timestamp;
					}
					catch { return "NA"; }
				}
			}
			public List<string> _tags
			{
				get
				{
					List<string> output = new List<string> { "NA" };
					try
					{
						if (tags.Count > 0)
						{
							output.Clear();
							foreach (Tag t in tags)
							{
								output.Add(Handler_String.SplitStr(t.key, "-", 0));
							}
						}
					}
					catch { output.Add("NA"); }
					return output;
				}
			}
			#endregion
		}
	}

	public class Service
	{
		public class Tag
		{
			public string context { get; set; }
			public string key { get; set; }
		}

		public class FromRelationships
		{
			public List<string> calls { get; set; }
			public List<string> runsOn { get; set; }
		}

		public class ToRelationships
		{
			public List<string> calls { get; set; }
		}

		public class SoftwareTechnology
		{
			public string type { get; set; }
			public string edition { get; set; }
			public string version { get; set; }
		}

		public class RootObject
		{
			public string entityId { get; set; }
			public string displayName { get; set; }
			public string discoveredName { get; set; }
			public long firstSeenTimestamp { get; set; }
			public long lastSeenTimestamp { get; set; }
			public List<Tag> tags { get; set; }
			public FromRelationships fromRelationships { get; set; }
			public ToRelationships toRelationships { get; set; }
			public string agentTechnologyType { get; set; }
			public List<string> serviceTechnologyTypes { get; set; }
			public string serviceType { get; set; }
			public List<SoftwareTechnology> softwareTechnologies { get; set; }
			public string webApplicationId { get; set; }
			public string webServerName { get; set; }
			public string contextRoot { get; set; }
			public string path { get; set; }
			public int? port { get; set; }
			public string customizedName { get; set; }
			public string webServiceName { get; set; }
			public string webServiceNamespace { get; set; }
			public string className { get; set; }
			public string databaseName { get; set; }
			public string databaseVendor { get; set; }
			public List<string> ipAddresses { get; set; }
		}
	}

	public class Processes
	{
		public class Tag
		{
			public string context { get; set; }
			public string key { get; set; }
		}

		public class FromRelationships
		{
			public List<string> isProcessOf { get; set; }
			public List<string> isInstanceOf { get; set; }
			public List<string> isNetworkClientOf { get; set; }
		}

		public class ToRelationships
		{
			public List<string> isNetworkClientOf { get; set; }
		}

		public class Metadata
		{
			public List<string> commandLineArgs { get; set; }
			public List<string> executables { get; set; }
			public List<string> javaMainClasses { get; set; }
			public List<string> ruleResult { get; set; }
			public List<string> executablePaths { get; set; }
		}

		public class SoftwareTechnology
		{
			public string type { get; set; }
			public string edition { get; set; }
			public string version { get; set; }
		}

		public class RootObject
		{
			public string entityId { get; set; }
			public string displayName { get; set; }
			public string discoveredName { get; set; }
			public long firstSeenTimestamp { get; set; }
			public long lastSeenTimestamp { get; set; }
			public List<Tag> tags { get; set; }
			public FromRelationships fromRelationships { get; set; }
			public ToRelationships toRelationships { get; set; }
			public Metadata metadata { get; set; }
			public List<SoftwareTechnology> softwareTechnologies { get; set; }
			public List<int> listenPorts { get; set; }
			public string bitness { get; set; }
		}
	}

	public class ProcessGroup
	{
		public class Tag
		{
			public string context { get; set; }
			public string key { get; set; }
		}

		public class FromRelationships
		{
			public List<string> isProcessOf { get; set; }
			public List<string> isInstanceOf { get; set; }
			public List<string> isNetworkClientOf { get; set; }
		}

		public class ToRelationships
		{
			public List<string> isNetworkClientOf { get; set; }
		}

		public class Metadata
		{
			public List<string> commandLineArgs { get; set; }
			public List<string> executables { get; set; }
			public List<string> javaMainClasses { get; set; }
			public List<string> ruleResult { get; set; }
			public List<string> executablePaths { get; set; }
		}

		public class SoftwareTechnology
		{
			public string type { get; set; }
			public string edition { get; set; }
			public string version { get; set; }
		}

		public class RootObject
		{
			public string entityId { get; set; }
			public string displayName { get; set; }
			public string discoveredName { get; set; }
			public long firstSeenTimestamp { get; set; }
			public long lastSeenTimestamp { get; set; }
			public List<Tag> tags { get; set; }
			public FromRelationships fromRelationships { get; set; }
			public ToRelationships toRelationships { get; set; }
			public Metadata metadata { get; set; }
			public List<SoftwareTechnology> softwareTechnologies { get; set; }
			public List<int> listenPorts { get; set; }
			public string bitness { get; set; }
		}
	}

	public class Application
	{
		public class FromRelationships
		{
			public List<string> calls { get; set; }
		}

		public class ToRelationships
		{
		}

		public class RootObject
		{
			public string entityId { get; set; }
			public string displayName { get; set; }
			public string customizedName { get; set; }
			public string discoveredName { get; set; }
			public long firstSeenTimestamp { get; set; }
			public long lastSeenTimestamp { get; set; }
			public List<object> tags { get; set; }
			public FromRelationships fromRelationships { get; set; }
			public ToRelationships toRelationships { get; set; }
			public string applicationType { get; set; }
			public string ruleAppliedMatchType { get; set; }
			public string ruleAppliedPattern { get; set; }
			public string applicationMatchTarget { get; set; }
		}
	}
}