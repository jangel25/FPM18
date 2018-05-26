using AppCocacolaNayMobiV2.Interfaces.Inventarios;
using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Models.Inventarios;
using AppCocacolaNayMobiV2.ViewModels.Base;
using System.Windows.Input;

namespace AppCocacolaNayMobiV2.ViewModels.Inventarios
{
    public class FicVmInventariosDetItem : FicViewModelBase
    {
        private zt_inventarios_det Fic_Zt_inventarios_det_Item;

        private ICommand FicSaveCommand;
        private ICommand FicCancelCommand;

        private IFicSrvNavigationInventario FicLoSrvNavigationInventario;
        private IFicSrvConteoInventario FicLoSrvConteoInventario;

        public FicVmInventariosDetItem(
            IFicSrvNavigationInventario FicPaSrvNavigationInventario,
            IFicSrvConteoInventario FicPaSrvConteoInventario)
        {
            FicLoSrvNavigationInventario = FicPaSrvNavigationInventario;
            FicLoSrvConteoInventario = FicPaSrvConteoInventario;
        }

        public zt_inventarios_det Item
        {
            get { return Fic_Zt_inventarios_det_Item; }
            set
            {
                Fic_Zt_inventarios_det_Item = value;
                RaisePropertyChanged();
            }
        }

        public ICommand FicMetSaveCommand
        {
            get { return FicSaveCommand = FicSaveCommand ?? new FicVmDelegateCommand(SaveCommandExecute); }
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


        private async void SaveCommandExecute()
        {
            await FicLoSrvConteoInventario.FicMetInsertNewInventarioDet(Item);
            FicLoSrvNavigationInventario.FicMetNavigateBack();
        }
        private void CancelCommandExecute()
        {
            FicLoSrvNavigationInventario.FicMetNavigateBack();
        }
    }
}
