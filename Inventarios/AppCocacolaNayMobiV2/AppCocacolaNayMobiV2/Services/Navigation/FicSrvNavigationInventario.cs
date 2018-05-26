using System;
using System.Collections.Generic;
using AppCocacolaNayMobiV2.ViewModels.Inventarios;
using AppCocacolaNayMobiV2.Views.Inventarios;
using AppCocacolaNayMobiV2.Interfaces.Navigation;
using Xamarin.Forms;

namespace AppCocacolaNayMobiV2.Services.Navigation
{
    public class FicSrvNavigationInventario : IFicSrvNavigationInventario
    {
        private IDictionary<Type, Type> viewModelRouting = new Dictionary<Type, Type>()
        {
            { typeof(FicVmConteoInventarioList),  typeof(FicViCpConteoInventarioList) },
            { typeof(FicVmConteoInventarioDetails ), typeof(FicViCpConteoInventarioDetails) },
            { typeof(FicVmConteoInventarioItem),   typeof(FicViCpConteoInventarioItem) },
            { typeof(FicVmConteoInventarioItemInsert),   typeof(FicViCpConteoInventarioItemInsert) },
            { typeof(FicVmConteoInventarioDelete),typeof(FicViCpConteoInventarioDelete)},
            { typeof(FicVmInventariosDetList), typeof(FicViCpInventariosDetList) },
            { typeof(FicVmInventariosDetItem), typeof(FicViCpInventariosDetItem) },
            { typeof(FicVmInventariosDetDetails), typeof(FicViCpInventariosDetDetails) },
            { typeof(FicVmConteoDetInventarioList), typeof(FicViConteoDetInventarioList)}
        };

        public void FicMetNavigateTo<TDestinationViewModel>(object navigationContext = null)
        {
            Type pageType = viewModelRouting[typeof(TDestinationViewModel)];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;

            if (page != null)
                Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void FicMetNavigateTo(Type destinationType, object navigationContext = null)
        {
            Type pageType = viewModelRouting[destinationType];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;

            if (page != null)
                Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void FicMetNavigateBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
