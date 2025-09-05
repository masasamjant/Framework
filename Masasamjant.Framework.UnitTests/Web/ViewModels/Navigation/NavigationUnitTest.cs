namespace Masasamjant.Web.ViewModels.Navigation
{
    public abstract class NavigationUnitTest : UnitTest
    {
        protected static IEnumerable<INavigationItem> GetNavigationItems()
        {
            var htmlAttributes = new Dictionary<string, object?>()
            {
                { "style", "color:blue;" }
            };
            var items = new List<NavigationItem>()
            {
                new NavigationItem(text: "Home", url: "http://Application/Home",  htmlAttributes: htmlAttributes, cssClass: "css", disabledCssClass: "disabled-css"),
                new NavigationItem(text: "About", url: "http://Application/About",  htmlAttributes: htmlAttributes, cssClass: "css", disabledCssClass: "disabled-css", enabled: false)
            };

            return items;
        }

        protected const string ExpectedUlNavigation = "<ul><li><a class=\"css\" href=\"http://Application/Home\" style=\"color:blue;\">Home</a></li><li><a class=\"css disabled-css\" href=\"http://Application/About\" style=\"color:blue;\">About</a></li></ul>";
        protected const string ExpectedDivNavigation = "<div><a class=\"css\" href=\"http://Application/Home\" style=\"color:blue;\">Home</a><a class=\"css disabled-css\" href=\"http://Application/About\" style=\"color:blue;\">About</a></div>";
    }
}
