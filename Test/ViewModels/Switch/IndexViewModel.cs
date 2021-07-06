using System.ComponentModel;

namespace Test.ViewModels.Switch
{
    public class IndexViewModel
    {
        public PaginatedList<Item> Items { get; set; }

        public FilterModel Filter { get; set; }

        public class FilterModel
        {
            [DisplayName("Поиск")]
            public string Query { get; set; }

            [DisplayName("Этаж, на котором стоит")]
            public int? InstallationFloor { get; set; }
        }

        public class Item
        {
            [DisplayName("ID")]
            public int Id { get; set; }

            [DisplayName("Модель")]
            public string ModelName { get; set; }

            [DisplayName("Этаж, на котором стоит")]
            public int? InstallationFloor { get; set; }
        }
    }
}
