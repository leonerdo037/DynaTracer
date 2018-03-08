using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public class ProblemData
{

	public class ProblemStatus
	{
		public string RawData;

		public class OpenProblemCounts
		{
			public int APPLICATION { get; set; }
			public int INFRASTRUCTURE { get; set; }
			public int SERVICE { get; set; }
		}

		public class Result
		{
			public int totalOpenProblemsCount { get; set; }
			public OpenProblemCounts openProblemCounts { get; set; }
		}

		public class Root_Problemstatus
		{
			public Result result { get; set; }
		}
	}

	public class ProblemFeed
	{
		public string RawData;

		public class RankedImpact
		{
			public string entityId { get; set; }
			public string entityName { get; set; }
			public string severityLevel { get; set; }
			public string impactLevel { get; set; }
			public string eventType { get; set; }
		}

		public class AffectedCounts
		{
			public int INFRASTRUCTURE { get; set; }
			public int SERVICE { get; set; }
			public int APPLICATION { get; set; }
		}

		public class RecoveredCounts
		{
			public int INFRASTRUCTURE { get; set; }
			public int SERVICE { get; set; }
			public int APPLICATION { get; set; }
		}

		public class TagsOfAffectedEntities
		{
			public string context { get; set; }
			public string key { get; set; }
		}

		public class Problem
		{
			public long id { get; set; }
			public long startTime { get; set; }
			public long endTime { get; set; }
			public string displayName { get; set; }
			public string impactLevel { get; set; }
			public string status { get; set; }
			public List<RankedImpact> rankedImpacts { get; set; }
			public AffectedCounts affectedCounts { get; set; }
			public RecoveredCounts recoveredCounts { get; set; }
			public List<TagsOfAffectedEntities> tagsOfAffectedEntities { get; set; }
			public string severityLevel { get; set; }
			public bool hasRootCause { get; set; }
			public string _startTime
			{
				get { return Handler_String.EpochConverter(startTime).ToString(); }
			}
			public string _endTime
			{
				get
				{
					if (endTime == -1)
					{
						return "Problem is still active";
					}
					else
					{
						return Handler_String.EpochConverter(endTime).ToString();
					}
				}
			}
			public string _openFor
			{
				get
				{
					if (endTime == -1)
					{
						return Handler_String.TimetoText(DateTime.Now - Handler_String.EpochConverter(startTime));
					}
					else
					{
						return Handler_String.TimetoText(Handler_String.EpochConverter(endTime) - Handler_String.EpochConverter(startTime));
					}
				}
			}
			public List<string> _tagsOfAffectedEntities
			{
				get
				{
					List<string> output = new List<string> { "NA" };
					try
					{
						if(tagsOfAffectedEntities.Count > 0)
						{
							output.Clear();
							foreach (TagsOfAffectedEntities tags in tagsOfAffectedEntities)
							{
								output.Add(Handler_String.SplitStr(tags.key, "-", 0));
							}
						}
					}
					catch { output.Add("NA"); }
					return output;
				}
			}
			#endregion
		}
		
		public class Monitored
		{
			public int INFRASTRUCTURE { get; set; }
			public int SERVICE { get; set; }
			public int APPLICATION { get; set; }
			public int _Total
			{
				get
				{
					return INFRASTRUCTURE + SERVICE + APPLICATION;
				}
			}
			#endregion
		}

		public class Result
		{
			public List<Problem> problems { get; set; }
			public Monitored monitored { get; set; }
		}

		public class Root_ProblemFeed
		{
			public Result result { get; set; }
		}
	}

	public class ProblemDetails
	{
		public string RawData;

		public class severities
		{
			public string context { get; set; }
			public dynamic value { get; set; }
			public string unit { get; set; }
		}

		public class RankedEvent
		{
			public long startTime { get; set; }
			public long endTime { get; set; }
			public string entityId { get; set; }
			public string entityName { get; set; }
			public string severityLevel { get; set; }
			public string impactLevel { get; set; }
			public string eventType { get; set; }
			public string status { get; set; }
			public List<severities> severities { get; set; }
			public bool isRootCause { get; set; }
			public string _startTime
			{
				get { return Handler_String.EpochConverter(startTime).ToString(); }
				set { }
			}
			public string _endTime
			{
				get
				{
					if (endTime == -1)
					{
						return "Problem is still active";
					}
					else
					{
						return Handler_String.EpochConverter(endTime).ToString();
					}
				}
			}
			public string _openFor
			{
				get
				{
					if (endTime == -1)
					{
						return Handler_String.TimetoText(DateTime.Now - Handler_String.EpochConverter(startTime));
					}
					else
					{
						return Handler_String.TimetoText(Handler_String.EpochConverter(endTime) - Handler_String.EpochConverter(startTime));
					}
				}
			}
			public List<string> _severities
			{
				get
				{
					List<string> output = new List<string> { "NA" };
					try
					{
						if (severities.Count > 0)
						{
							output.Clear();
							foreach (severities sevs in severities)
							{
								output.Add(sevs.value);
							}
						}
					}
					catch { output.Add("NA"); }
					return output;
				}
			}
			#endregion
		}

		public class TagsOfAffectedEntities
		{
			public string context { get; set; }
			public string key { get; set; }
		}

		public class Result
		{
			public string id { get; set; }
			public long startTime { get; set; }
			public long endTime { get; set; }
			public string displayName { get; set; }
			public string impactLevel { get; set; }
			public string status { get; set; }
			public string severityLevel { get; set; }
			public int commentCount { get; set; }
			public List<TagsOfAffectedEntities> tagsOfAffectedEntities { get; set; }
			public List<RankedEvent> rankedEvents { get; set; }
		}

		public class Root_ProblemDetails
		{
			public Result result { get; set; }
		}
	}

	public class ProblemComments
	{
		public string RawData;

		public class Comment
		{
			public long id { get; set; }
			public long createdAtTimestamp { get; set; }
			public string content { get; set; }
			public string userName { get; set; }
			public string context { get; set; }
			public string _createdAtTimestamp
			{
				get
				{
					if (createdAtTimestamp != 0)
					{
						return Handler_String.EpochConverter(createdAtTimestamp).ToString();
					}
					else
					{
						return "NA";
					}
				}
			}
			#endregion
		}

		public class Root_ProblemComments
		{
			public List<Comment> comments { get; set; }
			public List<string> _comments
			{
				get
				{
					List<String> output = new List<string> { "NA" };
					try
					{
						foreach (Comment currentC in comments)
						{
							output.Add(currentC.id.ToString());
						}
					}
					catch { output.Add("NA"); }
					return output;
				}
			}
			#endregion
		}
	}
	private void NotifyPropertyChange(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	public event PropertyChangedEventHandler PropertyChanged;
	#endregion
}