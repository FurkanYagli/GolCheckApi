namespace GolCheckApi.Models.DbSettings
{
    public class LaLigaPlayerDatabaseSettings
    {
        public string LaPlConnectionString { get; set; } = null!;

        public string LaPlDatabaseName { get; set; } = null!;

        public string LaPlCollectionName { get; set; } = null!;
    }
}
