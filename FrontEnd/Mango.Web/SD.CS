namespace Mango.Web
{
    public static class SD //Service details
    {

        public static string ProductAPIBase { get; set; }

        public static string ShoppingCartAPIBase { get; set; }

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
