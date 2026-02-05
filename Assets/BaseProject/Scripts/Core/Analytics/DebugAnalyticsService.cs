using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BaseProject.Scripts.Core.Analytics
{
    public class DebugAnalyticsService : IAnalyticsService
    {
        public virtual void LogEvent(string eventName, Dictionary<string, object> parameters)
        {
            string payload = string.Join(", ",
                parameters.Select(p => $"{p.Key}:{p.Value}"));

            Debug.Log($"[Analytics] {eventName} | {payload}");
        }
    }
}