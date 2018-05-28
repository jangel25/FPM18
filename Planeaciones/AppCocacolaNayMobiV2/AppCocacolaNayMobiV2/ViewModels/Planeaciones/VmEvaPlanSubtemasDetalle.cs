using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Planeaciones;
using AppCocacolaNayMobiV2.Models.Planeaciones;
using AppCocacolaNayMobiV2.ViewModels.Base;
using System.Windows.Input;

namespace AppCocacolaNayMobiV2.ViewModels.Planeaciones
{
    public class VmEvaPlanSubtemasDetalle : FicViewModelBase
    {
        private Eva_planeacion_subtemas _eva_planeacion_subtemas;

        private ICommand _addDelete;
        private ICommand _addRegresar;

        private INavigationPlaneacion _navigationService;
        private ISrvPlaneacion _sqliteService;

        public VmEvaPlanSubtemasDetalle(
            INavigationPlaneacion navigationService,
            ISrvPlaneacion sqliteService)
        {
            _navigationService = navigationService;
            _sqliteService = sqliteService;
        }//Fin constructor

        public Eva_planeacion_subtemas eva_planeacion_subtemas_detalle
        {
            get { return _eva_planeacion_subtemas; }
            set
            {
                _eva_planeacion_subtemas = value;
                RaisePropertyChanged();
            }
        }//Fin zt_inventario_conteos

        public override void OnAppearing(object navigationContext)
        {
            var eva_planeacion_subtemas_Item = navigationContext as Eva_planeacion_subtemas;

            if (eva_planeacion_subtemas_Item != null)
            {
                eva_planeacion_subtemas_detalle = eva_planeacion_subtemas_Item;
            }

            base.OnAppearing(navigationContext);
        }//Fin OnAppearing

        public ICommand DeleteCommand
        {
            get { return _addDelete = _addDelete ?? new FicVmDelegateCommand(DeleteCommandExecute); }
        }

        public async void DeleteCommandExecute()
        {
            await _sqliteService.Remove_eva_planeacion_subtemas(eva_planeacion_subtemas_detalle);
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
