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
    public class FicVmConteoInventarioDelete : FicViewModelBase
    {
        private zt_inventarios FicZt_inventarios_Item;
        public bool ActDelete;
      
       
        private ICommand FicDeleteCommand;
        private ICommand FicCancelCommand;

        private IFicSrvNavigationInventario FicLoSrvNavigationInventario;
        private IFicSrvConteoInventario FicLoSrvConteoInventario;

        public FicVmConteoInventarioDelete(
            IFicSrvNavigationInventario FicPaSrvNavigationInventario,
            IFicSrvConteoInventario FicPaSrvConteoInventario)
        {
            FicLoSrvNavigationInventario = FicPaSrvNavigationInventario;
            FicLoSrvConteoInventario = FicPaSrvConteoInventario;
            ActDelete = true;
            
        }

        public zt_inventarios Item
        {
            get { return FicZt_inventarios_Item; }
            set
            {
                FicZt_inventarios_Item = value;
                RaisePropertyChanged();
            }
        }

      

        public ICommand FicMetDeleteCommand
        {
            get { return FicDeleteCommand = FicDeleteCommand ?? new FicVmDelegateCommand(DeleteCommandExecute); }
        }

        public ICommand FicMetCancelCommand
        {
            get { return FicCancelCommand = FicCancelCommand ?? new FicVmDelegateCommand(CancelCommandExecute); }
        }

        public override void OnAppearing(object FicPaNavigationContext)
        {
            var FicLoZt_inventarios = FicPaNavigationContext as zt_inventarios;

            if (FicLoZt_inventarios != null)
            {
                Item = FicLoZt_inventarios;
            }

            base.OnAppearing(FicPaNavigationContext);
        }

      
        private async void DeleteCommandExecute()
        {
            await FicLoSrvConteoInventario.FicMetRemoveInventario(FicZt_inventarios_Item);
            FicLoSrvNavigationInventario.FicMetNavigateBack();
        }

        private void CancelCommandExecute()
        {
            FicLoSrvNavigationInventario.FicMetNavigateBack();
        }
    }
}
