using System.Collections.Generic;
using UnityEngine;

public interface IAnalyticsService
{
    void LogEvent(string eventName, Dictionary<string, object> parameters);
}