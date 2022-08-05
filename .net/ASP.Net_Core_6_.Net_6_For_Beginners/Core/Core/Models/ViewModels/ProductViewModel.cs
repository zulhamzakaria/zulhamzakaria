namespace Core.Models.ViewModels
{

    /*
        ViewModel reperesents data to be displayed inside the View
        ViewModel is for complex aplications or extra informations
        This ViewModel is used by CrudController, to control ProductEditor.cshtml, and managed by ViewModelFactory.cs
    */

    public class ProductViewModel
    {
        public Product Product { get; set; }
        public string Action { get; set; } = "Create";
        public bool ReadOnly { get; set; } = false;
        public string Theme { get; set; } = "primary";
        public bool ShowAction { get; set; } = true;
        public IEnumerable<Category> Categories { get; set; }
    }
}
