using System;
public class Data
{
    ulong _id;
    string _username;
    DateTime _date;
    public ulong Id
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }
    public string UserName
    {
        get
        {
            return _username;
        }
        set
        {
            _username = value;
        }
    }
    public DateTime Date
    {
        get
        {
            return _date;
        }
        set
        {
            _date = value;
        }
    }

}