using CodePolice.CodeInspector.Models;
using System.Collections.Generic;

namespace CodePolice.Alerts
{
    public interface ICodebaseAlert
    {        
        ProjectDirectory Project { get; }
        string AlertTypeName { get; }
        List<string> AlertDetails { get; }
        CodebaseAlertPriority Priority { get; }
    }
}
