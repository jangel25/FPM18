using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Planeaciones;
using AppCocacolaNayMobiV2.Models.Planeaciones;
using AppCocacolaNayMobiV2.ViewModels.Base;
using System.Windows.Input;

namespace AppCocacolaNayMobiV2.ViewModels.Planeaciones
{
    public class VmEvaPlanSubtemasItem : FicViewModelBase
    {
        public bool editar;

        private Eva_planeacion_subtemas _eva_planeacion_subtemas;

        private ICommand _saveCommand;
        private ICommand _deleteCommand;
        private ICommand _cancelCommand;

        private INavigationPlaneacion _navigationService;
        private ISrvPlaneacion _sqliteService;

        public VmEvaPlanSubtemasItem(
            INavigationPlaneacion navigationService,
            ISrvPlaneacion sqliteService)
        {
            _navigationService = navigationService;
            _sqliteService = sqliteService;
        }//Fin constructor

        public Eva_planeacion_subtemas eva_planeacion_subtemas_item
        {
            get { return _eva_planeacion_subtemas; }
            set
            {
                _eva_planeacion_subtemas = value;
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
            var eva_planeacion_subtemaItem = navigationContext as Eva_planeacion_subtemas;

            if (eva_planeacion_subtemaItem != null)
            {
                eva_planeacion_subtemas_item = eva_planeacion_subtemaItem;
            }

            base.OnAppearing(navigationContext);
            //base.OnAppearing(navigationContext);
        }//Fin OnAppearing

        private async void SaveCommandExecute()
        {
            await _sqliteService.Insert_eva_planeacion_subtemas(eva_planeacion_subtemas_item);
            _navigationService.NavigateBack();
        }//Fin SaveCommandExecute

        private async void DeleteCommandExecute()
        {
            await _sqliteService.Remove_eva_planeacion_subtemas(eva_planeacion_subtemas_item);
            _navigationService.NavigateBack();
        }//Fin DeleteCommandExecute

        private void CancelCommandExecute()
        {
            _navigationService.NavigateBack();
        }//Fin cancelCommandExecute
    }//Fin clase
}
