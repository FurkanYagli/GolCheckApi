namespace GolCheckApi.Models.DbSettings
{
    public class PrePlayerDatabaseSettings
    {
        public string PreConnectionString { get; set; } = null!;

        public string PreDatabaseName { get; set; } = null!;

        public string PreCollectionName { get; set; } = null!;
    }
}
