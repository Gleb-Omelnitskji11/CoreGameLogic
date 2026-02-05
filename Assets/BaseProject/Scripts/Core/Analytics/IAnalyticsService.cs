using System.Collections.Generic;

namespace BaseProject.Scripts.Core.Analytics
{
    public interface IAnalyticsService
    {
        void LogEvent(string eventName, Dictionary<string, object> parameters);
    }
}