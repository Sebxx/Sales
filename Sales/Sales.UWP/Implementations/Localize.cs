[assembly: Xamarin.Forms.Dependency(typeof(Sales.UWP.Implementations.Localize))]
namespace Sales.UWP.Implementations
{
    using System.Globalization;
    using System.Threading;
    using Interfaces;

    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            return Thread.CurrentThread.CurrentUICulture;
        }

        public void SetLocale(CultureInfo ci)
        {
            throw new System.NotImplementedException();
        }
    }
}