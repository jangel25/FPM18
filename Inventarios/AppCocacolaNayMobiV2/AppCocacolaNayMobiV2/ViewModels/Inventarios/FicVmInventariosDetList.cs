//using Acr.UserDialogs;
using AppCocacolaNayMobiV2.Interfaces.Inventarios;
using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Models.Inventarios;
using AppCocacolaNayMobiV2.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace AppCocacolaNayMobiV2.ViewModels.Inventarios
{
    public class FicVmInventariosDetList : FicViewModelBase
    {
        private ObservableCollection<zt_inventarios_det> FicOcZt_inventarios_det_Items;
        private zt_inventarios_det FicZt_inventarios_det_SelectedItem;
        
        private ICommand ficAddCommand;
        private ICommand ficEditCommand;
        private ICommand ficDetailsCommand;
        private ICommand ficDeleteCommand;
        private ICommand ficConteosCommand;

        private IFicSrvNavigationInventario FicLoSrvNavigationInventario;
        private IFicSrvConteoInventario FicLoSrvConteoInventario;

        public FicVmInventariosDetList(
            IFicSrvNavigationInventario ficPaSrvNavigationInventario,
            IFicSrvConteoInventario ficPaSrvConteoInventario)
        {
            FicLoSrvNavigationInventario = ficPaSrvNavigationInventario; 
            FicLoSrvConteoInventario = ficPaSrvConteoInventario;
        }

        public ObservableCollection<zt_inventarios_det> FicMetZt_inventarios_det_Items
        {
            get { return FicOcZt_inventarios_det_Items; }
            set
            {
                FicOcZt_inventarios_det_Items = value;
                RaisePropertyChanged();
            }
        }
        public zt_inventarios_det FicMetZt_inventarios_det_SelectedItem
        {
            get { return FicZt_inventarios_det_SelectedItem; }
            set
            {
                FicZt_inventarios_det_SelectedItem = value;
            }
        }

        public ICommand FicMetAddCommand
        {
            get { return ficAddCommand = ficAddCommand ?? new FicVmDelegateCommand(AddCommandExecute); }
        }

        public ICommand FicMetEditCommand
        {
            get { return ficEditCommand = ficEditCommand ?? new FicVmDelegateCommand(EditCommandExecute); }
        }

        public ICommand FicMetDetailsCommand
        {
            get { return ficDetailsCommand = ficDetailsCommand ?? new FicVmDelegateCommand(DetailsCommandExecute); }
        }

        public ICommand FicMetDeleteCommand
        {
            get { return ficDeleteCommand = ficDeleteCommand ?? new FicVmDelegateCommand(DeleteCommandExecute); }
        }

        public ICommand FicMetConteosCommand
        {
            get { return ficConteosCommand = ficConteosCommand ?? new FicVmDelegateCommand(ConteosCommandExecute); }
        }

        public override async void OnAppearing(object navigationContext)
        {
            var FicLoZt_inventarios = navigationContext as zt_inventarios;

            base.OnAppearing(navigationContext);
            //var result = await FicLoSrvConteoInventario.FicMetGetListInventariosDet();
            var result = await FicLoSrvConteoInventario.FicMetGetListInventariosDet(FicLoZt_inventarios);

            FicMetZt_inventarios_det_Items = new ObservableCollection<zt_inventarios_det>();
            foreach (var ficPaItem in result)
            {
                FicMetZt_inventarios_det_Items.Add(ficPaItem);
            }
            FicZt_inventarios_det_SelectedItem = null;
        }

        private void AddCommandExecute()
        {
            var ficZt_inventarios_det = new zt_inventarios_det();
            FicLoSrvNavigationInventario.FicMetNavigateTo<FicVmInventariosDetItem>(ficZt_inventarios_det);
        }
        private void EditCommandExecute()
        {
            if (FicZt_inventarios_det_SelectedItem != null)
            {
                FicLoSrvNavigationInventario.FicMetNavigateTo<FicVmInventariosDetItem>(FicZt_inventarios_det_SelectedItem);
            }
        }
        private void DetailsCommandExecute()
        {
            if (FicZt_inventarios_det_SelectedItem != null)
            {
                FicLoSrvNavigationInventario.FicMetNavigateTo<FicVmInventariosDetDetails>(FicZt_inventarios_det_SelectedItem);
            }
        }
        private void DeleteCommandExecute()
        {
            if (FicZt_inventarios_det_SelectedItem != null)
            {
                FicLoSrvNavigationInventario.FicMetNavigateTo<FicVmInventariosDetDetails>(FicZt_inventarios_det_SelectedItem);
            }
        }
        private void ConteosCommandExecute()
        {
            //FicLoSrvNavigationInventario.FicMetNavigateTo<FicVmInventariosConteosList>();
        }
    }
}
