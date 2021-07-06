using System;

namespace Test.Models
{
    public class Switch
    {
        public int Id { get; set; }

        public string ModelName { get; set; }

        public string IpAddress { get; set; }

        public string MacAddress { get; set; }

        public int ManagementVlan { get; set; }

        public string SerialNumber { get; set; }

        public string InventoryNumber { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime? InstallationDate { get; set; }

        public int? InstallationFloor { get; set; }

        public string Comment { get; set; }
    }
}
