using AppCocacolaNayMobiV2.Interfaces.Inventarios;
using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Models.Inventarios;
using AppCocacolaNayMobiV2.ViewModels.Base;
using System.Windows.Input;

namespace AppCocacolaNayMobiV2.ViewModels.Inventarios
{
    public class FicVmInventariosDetDetails : FicViewModelBase
    {
        private zt_inventarios_det FicZt_inventarios_det_Item;
        public bool ActDelete;
        public bool ActDetails;

        private ICommand FicEditCommand;
        private ICommand FicDeleteCommand;
        private ICommand FicCancelCommand;

        private IFicSrvNavigationInventario FicLoSrvNavigationInventario;
        private IFicSrvConteoInventario FicLoSrvConteoInventario;

        public FicVmInventariosDetDetails(
            IFicSrvNavigationInventario FicPaSrvNavigationInventario,
            IFicSrvConteoInventario FicPaSrvConteoInventario)
        {
            FicLoSrvNavigationInventario = FicPaSrvNavigationInventario;
            FicLoSrvConteoInventario = FicPaSrvConteoInventario;
            ActDelete = false;
            ActDetails = true;
        }

        public zt_inventarios_det Item
        {
            get { return FicZt_inventarios_det_Item; }
            set
            {
                FicZt_inventarios_det_Item = value;
                RaisePropertyChanged();
            }
        }

        public ICommand FicMetEditCommand
        {
            get { return FicEditCommand = FicEditCommand ?? new FicVmDelegateCommand(EditCommandExecute); }
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
            var FicLoZt_inventarios_det = FicPaNavigationContext as zt_inventarios_det;

            if (FicLoZt_inventarios_det != null)
            {
                Item = FicLoZt_inventarios_det;
            }

            base.OnAppearing(FicPaNavigationContext);
        }

        private void EditCommandExecute()
        {
            FicLoSrvNavigationInventario.FicMetNavigateTo<FicVmInventariosDetItem>(FicZt_inventarios_det_Item);
        }

        private async void DeleteCommandExecute()
        {
            await FicLoSrvConteoInventario.FicMetRemoveInventarioDet(FicZt_inventarios_det_Item);
            FicLoSrvNavigationInventario.FicMetNavigateBack();
        }

        private void CancelCommandExecute()
        {
            FicLoSrvNavigationInventario.FicMetNavigateBack();
        }
    }
}
