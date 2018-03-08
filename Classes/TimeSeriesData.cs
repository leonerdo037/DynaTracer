using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TimeSeriesData
{
	public class Result
	{
		public Dictionary<string, List<List<string>>> dataPoints { get; set; }
		public string timeseriesId { get; set; }
		public string unit { get; set; }
		public Dictionary<string,string> entities { get; set; }
		public long resolutionInMillisUTC { get; set; }
		public string aggregationType { get; set; }
		public Dictionary<string, string> _entityRelations
		{
			get
			{
				Dictionary<string, string> Output = new Dictionary<string, string>();
				foreach(string key in dataPoints.Keys)
				{
					if (key.Contains(",")){
						if (Output.ContainsKey(Handler_String.SplitStr(key, ",", 1)))
						{
							Output[Handler_String.SplitStr(key, ",", 1)] = Handler_String.SplitStr(key, ",", 0);
						}
						else
						{
							Output.Add(Handler_String.SplitStr(key, ",", 1), Handler_String.SplitStr(key, ",", 0));
						}
					}
				}
				return Output;
			}
		}

		public Dictionary<string, List<string[]>> _Metrics
		{
			get
			{
				Dictionary<string, List<string[]>> Output = new Dictionary<string, List<string[]>>();
				// Iterating over Entities
				foreach (string key in dataPoints.Keys)
				{
					List<string[]> points = new List<string[]>();
					// Iterating over Data Points
					foreach (List<string> list in dataPoints[key])
					{
						string[] metrics = new string[] { list[0] , list[1] };
						points.Add(metrics);
					}
					Output.Add(key, points);
				}
				return Output;
			}
		}
		#endregion
	}

	public class RootObject
	{
		public Result result { get; set; }
	}
}
