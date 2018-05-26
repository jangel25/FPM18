using AppCocacolaNayMobiV2.Interfaces.Navigation;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AppCocacolaNayMobiV2.ViewModels.Inventarios;
using AppCocacolaNayMobiV2.Views.Inventarios;

namespace AppCocacolaNayMobiV2.Services.Navigation
{
    public class FicSrvNavigationConteoDetInventarios : IFicSrvNavigationConteoDetInventario
    {
        private IDictionary<Type, Type> viewModelRouting = new Dictionary<Type, Type>()
        {
            { typeof(FicVmConteoDetInventarioList),  typeof(FicViConteoDetInventarioList) },
            { typeof(FicVmConteoDetInventarioItem), typeof(FicViConteoDetInventarioItem) },
            { typeof(FicVmConteoDetInventarioDetalle), typeof(FicViConteoDetInventarioDetalle) },
            { typeof(FicVmConteoInventarioList), typeof(FicViCpConteoInventarioList)}
        };

        public void NavigateTo<TDestinationViewModel>(object navigationContext = null)
        {
            Type pageType = viewModelRouting[typeof(TDestinationViewModel)];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;

            if (page != null)
                Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void NavigateTo(Type destinationType, object navigationContext = null)
        {
            Type pageType = viewModelRouting[destinationType];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;

            if (page != null)
                Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void NavigateBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
