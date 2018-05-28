using AppCocacolaNayMobiV2.Interfaces.Navigation;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AppCocacolaNayMobiV2.ViewModels.Planeaciones;
using AppCocacolaNayMobiV2.Views.Planeaciones;

namespace AppCocacolaNayMobiV2.Services.Navigation
{
    public class SrvNavigationPlaneaciones : INavigationPlaneacion
    {
        private IDictionary<Type, Type> viewModelRouting = new Dictionary<Type, Type>()
        {
            { typeof(VmEvaPlaneacionList),  typeof(ViEvaPlaneacionList) },
            { typeof(VmEvaPlaneacionItem), typeof(ViEvaPlaneacionItem) },
            { typeof(VmEvaPlaneacionDetalle), typeof(ViEvaPlaneacionDetalle) },
            { typeof(VmEvaPlaneacionTemasList),  typeof(ViEvaPlaneacionTemasList) },
            { typeof(VmEvaPlaneacionTemasItem), typeof(ViEvaPlaneacionTemasItem) },
            { typeof(VmEvaPlaneacionTemasDetalle), typeof(ViEvaPlaneacionTemasDetalle) },
            { typeof(VmEvaPlanSubtemasList),  typeof(ViEvaPlanSubtemasList) },
            { typeof(VmEvaPlanSubtemasItem), typeof(ViEvaPlanSubtemasItem) },
            { typeof(VmEvaPlanSubtemasDetalle), typeof(ViEvaPlanSubtemasDetalle) },
            { typeof(VmEvaCatFuentesList),  typeof(ViEvaCatFuentesList) },
            { typeof(VmEvaCatFuentesItem), typeof(ViEvaCatFuentesItem) },
            { typeof(VmEvaCatFuentesDetalle), typeof(ViEvaCatFuentesDetalle) },
            { typeof(VmEvaCatApoyosList),  typeof(ViEvaCatApoyosList) },
            { typeof(VmEvaCatApoyosItem), typeof(ViEvaCatApoyosItem) },
            { typeof(VmEvaCatApoyosDetalle), typeof(ViEvaCatApoyosDetalle) }
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
