using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Inventarios;
using AppCocacolaNayMobiV2.Models.Inventarios;
using AppCocacolaNayMobiV2.ViewModels.Base;
using System.Windows.Input;
using System;

namespace AppCocacolaNayMobiV2.ViewModels.Inventarios
{
    public class FicVmConteoDetInventarioDetalle : FicViewModelBase
    {
        private zt_inventarios_conteos _zt_inventarios_conteos;

        private ICommand _addDelete;
        private ICommand _addRegresar;

        private IFicSrvNavigationConteoDetInventario _navigationService;
        private IFicSrvConteoDetInventario _sqliteService;

        public FicVmConteoDetInventarioDetalle(
            IFicSrvNavigationConteoDetInventario navigationService,
            IFicSrvConteoDetInventario sqliteService)
        {
            _navigationService = navigationService;
            _sqliteService = sqliteService;
        }//Fin constructor

        public zt_inventarios_conteos Zt_inventario_conteos_detalle
        {
            get { return _zt_inventarios_conteos; }
            set
            {
                _zt_inventarios_conteos = value;
                RaisePropertyChanged();
            }
        }//Fin zt_inventario_conteos

        public override void OnAppearing(object navigationContext)
        {
            var zt_inventario_conteosItem = navigationContext as zt_inventarios_conteos;

            if (zt_inventario_conteosItem != null)
            {
                Zt_inventario_conteos_detalle = zt_inventario_conteosItem;
            }

            base.OnAppearing(navigationContext);
        }//Fin OnAppearing

        public ICommand DeleteCommand
        {
            get { return _addDelete = _addDelete ?? new FicVmDelegateCommand(DeleteCommandExecute); }
        }

        public async void DeleteCommandExecute()
        {
            await _sqliteService.Remove_zt_inventarios_conteos(Zt_inventario_conteos_detalle);
            _navigationService.NavigateBack();
        }//Fin DeleteCommandExecute

        private void CancelCommandExecute()
        {
            _navigationService.NavigateBack();
        }//Fin cancelCommandExecute

        public ICommand CancelCommand
        {
            get { return _addRegresar = _addRegresar ?? new FicVmDelegateCommand(CancelCommandExecute); }
        }
    }//Fin clase
}
