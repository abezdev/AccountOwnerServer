//using System;

//namespace LoggerService //We are going to use it for the logger logic
//{ 
//    public class Class1
//    {
//    }
//}

using System.Collections.Generic;
using System;

public interface IAlertDAO
{
    public Guid AddAlert(DateTime time);
    public DateTime GetAlert(Guid id);
}
public class AlertService
{
    private readonly AlertDAO storage = new AlertDAO();
    private readonly IAlertDAO _alertDAO;
    public AlertService(IAlertDAO IAlertDAO)
    {
        _alertDAO = IAlertDAO;
    }

    public Guid RaiseAlert()
    {
        return _alertDAO.AddAlert(DateTime.Now);
    }

    public DateTime GetAlertTime(Guid id)
    {
        return _alertDAO.GetAlert(id);
    }
}

public class AlertDAO : IAlertDAO
{
    private readonly Dictionary<Guid, DateTime> alerts = new Dictionary<Guid, DateTime>();

    public Guid AddAlert(DateTime time)
    {
        Guid id = Guid.NewGuid();
        this.alerts.Add(id, time);
        return id;
    }

    public DateTime GetAlert(Guid id)
    {
        return this.alerts[id];
    }
}