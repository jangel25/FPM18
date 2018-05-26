using System;

namespace AppCocacolaNayMobiV2.Interfaces.Navigation
{
    public interface IFicSrvNavigationConteoDetInventario
    {
        void NavigateTo<TDestinationViewModel>(object navigationContext = null);

        void NavigateTo(Type destinationType, object navigationContext = null);

        void NavigateBack();
    }
}
