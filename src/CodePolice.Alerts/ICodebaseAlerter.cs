using CodePolice.CodeInspector.Models;
using System.Collections.Generic;

namespace CodePolice.Alerts
{
    public interface ICodebaseAlerter
    {
        List<ICodebaseAlert> GenerateAlerts(Codebase codebase);
    }
}
