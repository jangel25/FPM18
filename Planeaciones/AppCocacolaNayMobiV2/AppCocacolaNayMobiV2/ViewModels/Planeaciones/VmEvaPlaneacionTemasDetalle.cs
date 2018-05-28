﻿using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Planeaciones;
using AppCocacolaNayMobiV2.Models.Planeaciones;
using AppCocacolaNayMobiV2.ViewModels.Base;
using System.Windows.Input;

namespace AppCocacolaNayMobiV2.ViewModels.Planeaciones
{
    public class VmEvaPlaneacionTemasDetalle : FicViewModelBase
    {
        private Eva_planeacion_temas _eva_planeacion_temas;

        private ICommand _addDelete;
        private ICommand _addRegresar;

        private INavigationPlaneacion _navigationService;
        private ISrvPlaneacion _sqliteService;

        public VmEvaPlaneacionTemasDetalle(
            INavigationPlaneacion navigationService,
            ISrvPlaneacion sqliteService)
        {
            _navigationService = navigationService;
            _sqliteService = sqliteService;
        }//Fin constructor

        public Eva_planeacion_temas eva_planeacion_temas_detalle
        {
            get { return _eva_planeacion_temas; }
            set
            {
                _eva_planeacion_temas = value;
                RaisePropertyChanged();
            }
        }//Fin zt_inventario_conteos

        public override void OnAppearing(object navigationContext)
        {
            var eva_planeacion_temas_Item = navigationContext as Eva_planeacion_temas;

            if (eva_planeacion_temas_Item != null)
            {
                eva_planeacion_temas_detalle = eva_planeacion_temas_Item;
            }

            base.OnAppearing(navigationContext);
        }//Fin OnAppearing

        public ICommand DeleteCommand
        {
            get { return _addDelete = _addDelete ?? new FicVmDelegateCommand(DeleteCommandExecute); }
        }

        public async void DeleteCommandExecute()
        {
            await _sqliteService.Remove_eva_planeacion_temas(eva_planeacion_temas_detalle);
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
