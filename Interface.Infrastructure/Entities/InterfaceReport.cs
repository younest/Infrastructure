namespace Interface.Infrastructure.Entities
{
    public class InterfaceReport
    {
        public string TableName { get; set; }

        public int Pending { get; set; }

        public int Successfully { get; set; }

        public int Failed { get; set; }

        public string LastDate { get; set; }
    }
}
