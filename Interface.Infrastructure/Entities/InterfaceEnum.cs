namespace Interface.Infrastructure.Entities
{
    public enum Status
    {
        Failed = 0,
        Successfully = 1
    }

    public enum DataFormatter
    {
        SOAP = 0,
        XML = 1,
        JSON = 2
    }

    public enum RequestMethod
    {
        Get = 0,
        Post = 1,
        Put = 2,
        Delete = 3
    }

    public static class ContentType
    {
        public const string Json = "application/json";
        public const string Soap = "text/xml;charset=utf-8";
        public const string Xml = "text/xml";
    }

}
