using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Planeaciones;
using AppCocacolaNayMobiV2.Models.Planeaciones;
using AppCocacolaNayMobiV2.ViewModels.Base;
using System.Windows.Input;

namespace AppCocacolaNayMobiV2.ViewModels.Planeaciones
{
    public class VmEvaCatFuentesDetalle : FicViewModelBase
    {
        private Eva_cat_fuentes_bibliograficas _eva_cat_fuentes;

        private ICommand _addDelete;
        private ICommand _addRegresar;

        private INavigationPlaneacion _navigationService;
        private ISrvPlaneacion _sqliteService;

        public VmEvaCatFuentesDetalle(
            INavigationPlaneacion navigationService,
            ISrvPlaneacion sqliteService)
        {
            _navigationService = navigationService;
            _sqliteService = sqliteService;
        }//Fin constructor

        public Eva_cat_fuentes_bibliograficas eva_cat_fuentes_detalle
        {
            get { return _eva_cat_fuentes; }
            set
            {
                _eva_cat_fuentes = value;
                RaisePropertyChanged();
            }
        }//Fin zt_inventario_conteos

        public override void OnAppearing(object navigationContext)
        {
            var eva_cat_fuentes_Item = navigationContext as Eva_cat_fuentes_bibliograficas;

            if (eva_cat_fuentes_Item != null)
            {
                eva_cat_fuentes_detalle = eva_cat_fuentes_Item;
            }

            base.OnAppearing(navigationContext);
        }//Fin OnAppearing

        public ICommand DeleteCommand
        {
            get { return _addDelete = _addDelete ?? new FicVmDelegateCommand(DeleteCommandExecute); }
        }

        public async void DeleteCommandExecute()
        {
            await _sqliteService.Remove_eva_cat_fuentes_bibliograficas(eva_cat_fuentes_detalle);
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
    }
}
