using System.Windows.Input;
using AppCocacolaNayMobiV2.Models.Inventarios;
using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Inventarios;
using AppCocacolaNayMobiV2.ViewModels.Base;

namespace AppCocacolaNayMobiV2.ViewModels.Inventarios
{
    public class FicVmAlmacenDetalle : FicViewModelBase
    {
        private zt_cat_almacenes Fic_Zt_Cat_Almacenes_Item;

        private ICommand FicEditCommand;
        private ICommand FicCancelCommand;

        private IFicSrvNavigationAlmacen FicLoSrvNavigationAlmacen;
        private IFicSrvCatAlmacen FicLoSrvCatAlmacenes;

        public FicVmAlmacenDetalle(
            IFicSrvNavigationAlmacen FicPaSrvNavigationAlmacen,
            IFicSrvCatAlmacen FicPaSrvCatAlmacen)
        {
            FicLoSrvNavigationAlmacen = FicPaSrvNavigationAlmacen;
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

        public ICommand FicMetEditCommand
        {
            get { return FicEditCommand = FicEditCommand ?? new FicVmDelegateCommand(EditCommandExecute); }
        }

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

        private void EditCommandExecute()
        {
            if (Fic_Zt_Cat_Almacenes_Item != null)
            {
                FicLoSrvNavigationAlmacen.FicMetNavigateTo<FicVmAlmacenEditar>(Fic_Zt_Cat_Almacenes_Item);
            }
            Fic_Zt_Cat_Almacenes_Item = null;
        }

        private void CancelCommandExecute()
        {
            FicLoSrvNavigationAlmacen.FicMetNavigateBack();
        }
    }
}
