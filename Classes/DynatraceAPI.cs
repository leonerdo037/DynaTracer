using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class APIOutput
{
	public enum APIResult
	{
		CertificateError,
		Success,
		UnknownError
	}

	public APIResult APIStatus { get; set; }
	public string RawData { get; set; }
	public dynamic OutputClass { get; set; }
}

public class DynatraceAPI
{
	public string Target { get; set; }
	public string EnvID { get; set; }
	public string Token { get; set; }

	/// <summary>
	/// The class which is used to initiate API requests to Dynatrace SaaS.
	/// </summary>
	/// <param name="environmentID">The Environemnt ID whcich can be identified by looking at your Dynatrace environment URL.
	/// <para>Example:  https://{id}.live.dynatrace.com/api/v1/ </para>
	/// </param>
	/// <param name="authToken">The Dynatrace API Token.
	/// <para>Can be created in dynatrace by navigating to Settings -> Integration -> Dynatrace API</para>
	/// </param>
	public DynatraceAPI(string environmentID, string authToken)
	{
		Target = string.Format("https://{0}.live.dynatrace.com/api/v1/", environmentID);
		Token = authToken;
	}

	/// <summary>
	/// The class which is used to initiate API requests to Dynatrace Managed.
	/// </summary>
	/// <param name="domain">The URL of the Dynatrace Managed Instance
	/// <para>Example:  https://owndomain </para>
	/// </param>
	/// <param name="environmentID">The Environemnt ID whcich can be identified by looking at your Dynatrace environment URL.
	/// <para>Example:  https://owndomain/e/{id}/api/v1/ </para>
	/// </param>
	/// <param name="authToken">The Dynatrace API Token.
	/// <para>Can be created in dynatrace by navigating to Settings -> Integration -> Dynatrace API</para>
	/// </param>
	public DynatraceAPI(string domain, string environmentID, string authToken)
	{
		Target = string.Format("{0}/e/{1}/api/v1", domain, environmentID);
		Token = authToken;
	}

	#region Problem API
	public APIOutput ProblemStatus(bool ignoreCertificate = false)
	{
		// Creating Variables
		RESTClient APIClient = new RESTClient(Target);
		APIOutput outputValue = new APIOutput();
		Dictionary<string, string> parameters = new Dictionary<string, string>
		{
			// Adding Parameters
			{ "Api-Token", Token }
		};
		// API Call
		string APIResponse = APIClient.GetRequest("/problem/status", URLParameters: parameters, IgnoreCertificate: ignoreCertificate);
		outputValue.RawData = APIResponse;
		// Processing Response
		if (APIResponse == "Certificate Error")
		{
			outputValue.APIStatus = APIResult.CertificateError;
		}
		else
		{
			outputValue.APIStatus = APIResult.Success;
			outputValue.OutputClass = JsonConvert.DeserializeObject<ProblemData.ProblemStatus.Root_Problemstatus>(APIResponse);
		}
		return outputValue;
	}

	public enum Filter_ProblemStatus
	{
		ALL,
		OPEN,
		CLOSED
	}

	public enum Filter_ProblemImpactLevel
	{
		ALL,
		APPLICATION,
		SERVICE,
		INFRASTRUCTURE
	}

	public enum Filter_ProblemRelativeTime
	{
		OneHour,
		TwoHours,
		SixHours,
		Day,
		Week,
		Month
	}

	public APIOutput ProblemFeed(bool ignoreCertificate = false,
								 Filter_ProblemStatus pStatus = Filter_ProblemStatus.ALL,
								 Filter_ProblemImpactLevel pImpactLevel = Filter_ProblemImpactLevel.ALL,
								 Filter_ProblemRelativeTime pRelativeTime = Filter_ProblemRelativeTime.OneHour)
	{
		// Creating Variables
		RESTClient APIClient = new RESTClient(Target);
		APIOutput outputValue = new APIOutput();
		Dictionary<string, string> parameters = new Dictionary<string, string>
		{
			// Adding Parameters
			{ "Api-Token", Token }
		};
		// Status
		if (pStatus != Filter_ProblemStatus.ALL)
		{
			parameters.Add("status", pStatus.ToString());
		}
		// Impact Level
		if (pImpactLevel != Filter_ProblemImpactLevel.ALL)
		{
			parameters.Add("impactLevel", pImpactLevel.ToString());
		}
		// Relative Time
		switch (pRelativeTime)
		{
			case Filter_ProblemRelativeTime.OneHour:
				parameters.Add("relativeTime", "hour");
				break;
			case Filter_ProblemRelativeTime.TwoHours:
				parameters.Add("relativeTime", "2hours");
				break;
			case Filter_ProblemRelativeTime.SixHours:
				parameters.Add("relativeTime", "6hours");
				break;
			case Filter_ProblemRelativeTime.Day:
				parameters.Add("relativeTime", "day");
				break;
			case Filter_ProblemRelativeTime.Week:
				parameters.Add("relativeTime", "week");
				break;
			case Filter_ProblemRelativeTime.Month:
				parameters.Add("relativeTime", "month");
				break;
			default:
				break;
		}
		// API Call
		string APIResponse = APIClient.GetRequest("/problem/feed", URLParameters: parameters, IgnoreCertificate: ignoreCertificate);
		outputValue.RawData = APIResponse;
		// Processing Response
		if (APIResponse == "Certificate Error")
		{
			outputValue.APIStatus = APIResult.CertificateError;
		}
		else
		{
			outputValue.APIStatus = APIResult.Success;
			outputValue.OutputClass = JsonConvert.DeserializeObject<ProblemData.ProblemFeed.Root_ProblemFeed>(APIResponse);
		}
		return outputValue;
	}

	public APIOutput ProblemDetails(long problemID, bool ignoreCertificate = false)
	{
		// Creating Variables
		RESTClient APIClient = new RESTClient(Target);
		APIOutput outputValue = new APIOutput();
		Dictionary<string, string> parameters = new Dictionary<string, string>
		{
			// Adding Parameters
			{ "Api-Token", Token }
		};
		// API Call
		string APIResponse = APIClient.GetRequest("/problem/details/" + problemID, URLParameters: parameters, IgnoreCertificate: ignoreCertificate);
		outputValue.RawData = APIResponse;
		// Processing Response
		if (APIResponse == "Certificate Error")
		{
			outputValue.APIStatus = APIResult.CertificateError;
		}
		else
		{
			outputValue.APIStatus = APIResult.Success;
			outputValue.OutputClass = JsonConvert.DeserializeObject<ProblemData.ProblemDetails.Root_ProblemDetails>(APIResponse);
		}
		return outputValue;
	}

	public APIOutput ProblemComments(long problemID, bool ignoreCertificate = false)
	{
		// Creating Variables
		RESTClient APIClient = new RESTClient(Target);
		APIOutput outputValue = new APIOutput();
		Dictionary<string, string> parameters = new Dictionary<string, string>
		{
			// Adding Parameters
			{ "Api-Token", Token }
		};
		// API Call
		string APIResponse = APIClient.GetRequest(string.Format("/problem/details/{0}/comments", problemID), URLParameters: parameters, IgnoreCertificate: ignoreCertificate);
		outputValue.RawData = APIResponse;
		// Processing Response
		if (APIResponse == "Certificate Error")
		{
			outputValue.APIStatus = APIResult.CertificateError;
		}
		else
		{
			outputValue.APIStatus = APIResult.Success;
			outputValue.OutputClass = JsonConvert.DeserializeObject<ProblemData.ProblemComments.Root_ProblemComments>(APIResponse);
		}
		return outputValue;
	}
	#endregion

	#region Topology API
	public APIOutput Hosts(bool ignoreCertificate = false,
								 string tag = "NA",
								 long startTimestamp = -1,
								 long endTimestamp = -1)
	{
		// Creating Variables
		RESTClient APIClient = new RESTClient(Target);
		APIOutput outputValue = new APIOutput();
		Dictionary<string, string> parameters = new Dictionary<string, string>
		{
			// Adding Parameters
			{ "Api-Token", Token }
		};
		// Tag
		if (tag != "NA")
		{
			parameters.Add("tag", tag);
		}
		// Start Time
		if (startTimestamp != -1)
		{
			parameters.Add("startTimestamp", startTimestamp.ToString());
		}
		// End Time
		if (endTimestamp != -1)
		{
			parameters.Add("endTimestamp", endTimestamp.ToString());
		}
		// API Call
		string APIResponse = APIClient.GetRequest("/entity/infrastructure/hosts", URLParameters: parameters, IgnoreCertificate: ignoreCertificate);
		outputValue.RawData = APIResponse;
		// Processing Response
		if (APIResponse == "Certificate Error")
		{
			outputValue.APIStatus = APIResult.CertificateError;
		}
		else
		{
			outputValue.APIStatus = APIResult.Success;
			outputValue.OutputClass = JsonConvert.DeserializeObject<List<TopologyData.Hosts.RootObject>>(APIResponse);
		}
		return outputValue;
	}
	#endregion

	#region Timeseries API
	public enum Filter_AggregationTypes
	{
		None,
		max,
		min,
		sum,
		avg,
		median,
		count
	}
	public enum Filter_TSRelativeTime
	{
		OneHour,
		TwoHours,
		SixHours,
		Day,
		Week,
		Month
	}
	public enum Filter_QueryMode
	{
		total,
		series
	}

	public APIOutput TimeSeries(string TID,
								 bool ignoreCertificate = false,
								 string EntityName = "",
								 Filter_AggregationTypes AType = Filter_AggregationTypes.None,
								 Filter_TSRelativeTime relTime = Filter_TSRelativeTime.OneHour,
								 Filter_QueryMode qmode = Filter_QueryMode.total)
	{
		// Creating Variables
		RESTClient APIClient = new RESTClient(Target);
		APIOutput outputValue = new APIOutput();
		Dictionary<string, string> parameters = new Dictionary<string, string>
		{
			// Adding Parameters
			{ "Api-Token", Token },
			{"timeseriesId", TID},
			{ "queryMode", qmode.ToString()}
		};
		// Aggregation Type
		if(AType != Filter_AggregationTypes.None)
		{
			parameters.Add("aggregationType", AType.ToString());
		}
		// Entity Name
		if (!string.IsNullOrEmpty(EntityName))
		{
			parameters.Add("entity", EntityName);
		}
		// Relative Time
		switch (relTime)
		{
			case Filter_TSRelativeTime.OneHour:
				parameters.Add("relativeTime", "hour");
				break;
			case Filter_TSRelativeTime.TwoHours:
				parameters.Add("relativeTime", "2hours");
				break;
			case Filter_TSRelativeTime.SixHours:
				parameters.Add("relativeTime", "6hours");
				break;
			case Filter_TSRelativeTime.Day:
				parameters.Add("relativeTime", "day");
				break;
			case Filter_TSRelativeTime.Week:
				parameters.Add("relativeTime", "week");
				break;
			case Filter_TSRelativeTime.Month:
				parameters.Add("relativeTime", "month");
				break;
			default:
				break;
		}
		// API Call
		string APIResponse = APIClient.GetRequest("/timeseries", URLParameters: parameters, IgnoreCertificate: ignoreCertificate);
		outputValue.RawData = APIResponse;
		// Processing Response
		if (APIResponse == "Certificate Error")
		{
			outputValue.APIStatus = APIResult.CertificateError;
		}
		else
		{
			outputValue.APIStatus = APIResult.Success;
			outputValue.OutputClass = JsonConvert.DeserializeObject<TimeSeriesData.RootObject>(APIResponse);
		}
		return outputValue;
	}
	#endregion
}