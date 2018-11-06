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
        OData = 1,
        XML = 2,
        JSON = 3
    }

    public enum RequestMethod
    {
        Get = 0,
        Post = 1
    }

    public static class ContentType
    {
        public const string Json = "application/json";
        public const string Soap = "text/xml;charset=utf-8";
        public const string Xml = "text/xml";
    }

}
