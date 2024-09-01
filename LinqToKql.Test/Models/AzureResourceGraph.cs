using System.ComponentModel.DataAnnotations.Schema;

namespace LinqToKql.Test.Models
{
    /// <summary>
    /// Azure Resource Graph object subset of properties
    /// </summary>
    [Table("resources")]
    internal class AzureResource
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public Guid tenantId { get; set; }
        public string kind { get; set; }
        public string location { get; set; }
        public string resourceGroup { get; set; }
        public string subscriptionId { get; set; }

        public int instances { get; set; }

        public AzureResourceProperties properties { get; set; }
        public Dictionary<string,string> tags { get; set; }

        public DateTime dateCreated { get; set; }

    }

    /// <summary>
    /// Azure Resource Graph properties object subset
    /// </summary>
    internal class AzureResourceProperties
    {
        public string name { get; set; }
        public string state { get; set; }
        public bool enabled { get; set; }
    }
}
