using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Test.ViewModels.Switch
{
    public class CreateViewModel
    {
        [Required]
        [DisplayName("Модель")]
        public string ModelName { get; set; }

        [DisplayName("IP-адрес")]
        [RegularExpression(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.|$)){4}$", ErrorMessage = "Invalid IP address")]
        public string IpAddress { get; set; }

        [DisplayName("MAC-адрес")]
        [RegularExpression("^(([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2}))|(([0-9A-Fa-f]{4}[.]){2}([0-9A-Fa-f]{4}))$", ErrorMessage = "Invalid MAC address")]
        public string MacAddress { get; set; }

        [DisplayName("Управляющий VLAN")]
        [Range(1, 4094)]
        public int ManagementVlan { get; set; }

        [DisplayName("Серийный номер")]
        public string SerialNumber { get; set; }

        [DisplayName("Инвентарный номер")]
        public string InventoryNumber { get; set; }

        [DisplayName("Дата покупки")]
        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }

        [DisplayName("Дата установки")]
        [DataType(DataType.Date)]
        public DateTime? InstallationDate { get; set; }

        [DisplayName("Этаж, на котором стоит")]
        public int? InstallationFloor { get; set; }

        [DisplayName("Комментарий")]
        public string Comment { get; set; }
    }
}
