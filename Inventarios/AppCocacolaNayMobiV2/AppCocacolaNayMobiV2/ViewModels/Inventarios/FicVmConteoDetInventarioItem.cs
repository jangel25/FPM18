using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Inventarios;
using AppCocacolaNayMobiV2.Models.Inventarios;
using AppCocacolaNayMobiV2.ViewModels.Base;
using System.Windows.Input;
using System;
using Syncfusion.SfDataGrid.XForms;

namespace AppCocacolaNayMobiV2.ViewModels.Inventarios
{
    public class FicVmConteoDetInventarioItem : FicViewModelBase
    {

        public bool editar;

        private zt_inventarios_conteos _zt_inventarios_conteos;

        private ICommand _saveCommand;
        private ICommand _deleteCommand;
        private ICommand _cancelCommand;

        private IFicSrvNavigationConteoDetInventario _navigationService;
        private IFicSrvConteoDetInventario _sqliteService;

        public FicVmConteoDetInventarioItem(
            IFicSrvNavigationConteoDetInventario navigationService,
            IFicSrvConteoDetInventario sqliteService)
        {
            _navigationService = navigationService;
            _sqliteService = sqliteService;
        }//Fin constructor

        public zt_inventarios_conteos Zt_inventario_conteos
        {
            get { return _zt_inventarios_conteos; }
            set
            {
                _zt_inventarios_conteos = value;
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
            var zt_inventario_conteosItem = navigationContext as zt_inventarios_conteos;

            if (zt_inventario_conteosItem != null)
            {
                Zt_inventario_conteos = zt_inventario_conteosItem;
            }



            base.OnAppearing(navigationContext);
            //base.OnAppearing(navigationContext);
        }//Fin OnAppearing

        private async void SaveCommandExecute()
        {
            Zt_inventario_conteos.UsuarioReg = "Brian Alejandro Casas López";
            Zt_inventario_conteos.FechaReg = DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year;
            Zt_inventario_conteos.HoraReg = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            Console.WriteLine("VALOOOOOOR ");
            //Zt_inventario_conteos.IdInventario = Zt_inventario_det.IdInventario;

            await _sqliteService.Insert_zt_inventarios_conteos(Zt_inventario_conteos);
            _navigationService.NavigateBack();
        }//Fin SaveCommandExecute

        private async void DeleteCommandExecute()
        {
            await _sqliteService.Remove_zt_inventarios_conteos(Zt_inventario_conteos);
            _navigationService.NavigateBack();
        }//Fin DeleteCommandExecute

        private void CancelCommandExecute()
        {
            _navigationService.NavigateBack();
        }//Fin cancelCommandExecute
    }
}