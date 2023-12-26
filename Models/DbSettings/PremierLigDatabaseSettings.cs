namespace GolCheckApi.Models.DbSettings
{
    public class PremierLigDatabaseSettings
    {
        public string PreLigConnectionString { get; set; } = null!;

        public string PreLigDatabaseName { get; set; } = null!;

        public string PreLigCollectionName { get; set; } = null!;
    }
}
