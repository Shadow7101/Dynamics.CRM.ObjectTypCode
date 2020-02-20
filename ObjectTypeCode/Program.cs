namespace ObjectTypeCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var d = new Descobrir1();

            var x = d.Reterieve("contact");

            var y = d.GetEntityLogicalName(3);
        }
    }
}
