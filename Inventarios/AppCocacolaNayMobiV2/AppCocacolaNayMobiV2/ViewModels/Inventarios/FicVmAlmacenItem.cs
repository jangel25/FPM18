using AppCocacolaNayMobiV2.Models.Inventarios;
using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Inventarios;
using AppCocacolaNayMobiV2.ViewModels.Base;
using System.Windows.Input;

namespace AppCocacolaNayMobiV2.ViewModels.Inventarios
{
    public class FicVmAlmacenItem : FicViewModelBase
    {

        private zt_cat_almacenes Fic_Zt_Cat_Almacenes_Item;

        private ICommand FicSaveCommand;
        //private ICommand FicDeleteCommand;
        private ICommand FicCancelCommand;

        private IFicSrvNavigationInventario FicLoSrvNavigationInventario;
        private IFicSrvCatAlmacen FicLoSrvCatAlmacenes;

        public FicVmAlmacenItem(
            IFicSrvNavigationInventario FicPaSrvNavigationInventario,
            IFicSrvCatAlmacen FicPaSrvCatAlmacen)
        {
            FicLoSrvNavigationInventario = FicPaSrvNavigationInventario;
            FicLoSrvCatAlmacenes = FicPaSrvCatAlmacen;
        }

        public zt_cat_almacenes Item
        {
            get { return Fic_Zt_Cat_Almacenes_Item; }
            set
            {
                Fic_Zt_Cat_Almacenes_Item = value;
                RaisePropertyChanged();
            }
        }

        public ICommand FicMetSaveCommand
        {
            get { return FicSaveCommand = FicSaveCommand ?? new FicVmDelegateCommand(SaveCommandExecute); }
        }

        /*public ICommand FicMetDeleteCommand
        {
            get { return FicDeleteCommand = FicDeleteCommand ?? new FicVmDelegateCommand(DeleteCommandExecute); }
        }*/

        public ICommand FicMetCancelCommand
        {
            get { return FicCancelCommand = FicCancelCommand ?? new FicVmDelegateCommand(CancelCommandExecute); }
        }

        public override void OnAppearing(object FicPaNavigationContext)
        {
            var FicLoZt_cat_almacenes = FicPaNavigationContext as zt_cat_almacenes;

            if (FicLoZt_cat_almacenes != null)
            {
                Item = FicLoZt_cat_almacenes;
            }

            base.OnAppearing(FicPaNavigationContext);
        }

        private async void SaveCommandExecute()
        {
            await FicLoSrvCatAlmacenes.FicMetInsertNewCatAlmacen(Item);
            FicLoSrvNavigationInventario.FicMetNavigateBack();
        }

        /*private async void DeleteCommandExecute()
        {
            await FicLoSrvCatAlmacenes.FicMetRemoveCatAlmacen(Item);
            FicLoSrvNavigationInventario.FicMetNavigateBack();
        }*/

        private void CancelCommandExecute()
        {
            FicLoSrvNavigationInventario.FicMetNavigateBack();
        }

    }
}