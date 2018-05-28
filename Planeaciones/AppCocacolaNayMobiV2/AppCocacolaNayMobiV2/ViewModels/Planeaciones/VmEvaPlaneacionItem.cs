using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Planeaciones;
using AppCocacolaNayMobiV2.Models.Planeaciones;
using AppCocacolaNayMobiV2.ViewModels.Base;
using System.Windows.Input;

namespace AppCocacolaNayMobiV2.ViewModels.Planeaciones
{
    public class VmEvaPlaneacionItem : FicViewModelBase
    {
        public bool editar;

        private Eva_planeacion _eva_planeacion;

        private ICommand _saveCommand;
        private ICommand _deleteCommand;
        private ICommand _cancelCommand;

        private INavigationPlaneacion _navigationService;
        private ISrvPlaneacion _sqliteService;

        public VmEvaPlaneacionItem(
            INavigationPlaneacion navigationService,
            ISrvPlaneacion sqliteService)
        {
            _navigationService = navigationService;
            _sqliteService = sqliteService;
        }//Fin constructor

        public Eva_planeacion eva_planeacion_item
        {
            get { return _eva_planeacion; }
            set
            {
                _eva_planeacion = value;
                RaisePropertyChanged();
            }
        }//Fin zt_inventario_conteos

        public ICommand SaveCommand
        {
            get { return _saveCommand = _saveCommand ?? new FicVmDelegateCommand(SaveCommandExecute); }
        }

        public ICommand DeleteCommand
        {
            get { return _deleteCommand = _deleteCommand ?? new FicVmDelegateCommand(DeleteCommandExecute); }
        }

        public ICommand CancelCommand
        {
            get { return _cancelCommand = _cancelCommand ?? new FicVmDelegateCommand(CancelCommandExecute); }
        }

        public override void OnAppearing(object navigationContext)
        {
            var eva_planeacion_Item = navigationContext as Eva_planeacion;

            if (eva_planeacion_Item != null)
            {
                eva_planeacion_item = eva_planeacion_Item;
            }



            base.OnAppearing(navigationContext);
            //base.OnAppearing(navigationContext);
        }//Fin OnAppearing

        private async void SaveCommandExecute()
        {
            await _sqliteService.Insert_eva_planeacion(eva_planeacion_item);
            _navigationService.NavigateBack();
        }//Fin SaveCommandExecute

        private async void DeleteCommandExecute()
        {
            await _sqliteService.Remove_eva_planeacion(eva_planeacion_item);
            _navigationService.NavigateBack();
        }//Fin DeleteCommandExecute

        private void CancelCommandExecute()
        {
            _navigationService.NavigateBack();
        }//Fin cancelCommandExecute
    }//Fin clase
}
