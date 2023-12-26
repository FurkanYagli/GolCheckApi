namespace GolCheckApi.Models
{
    public class Return
    {
        public bool success { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }
    }
}
