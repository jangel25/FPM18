using Autofac;
using AppCocacolaNayMobiV2.Services.Navigation;
using AppCocacolaNayMobiV2.Services.Inventarios;
using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Inventarios;
using AppCocacolaNayMobiV2.ViewModels.Inventarios;

namespace AppCocacolaNayMobiV2.ViewModels.Base
{
    public class FicViewModelLocator
    {
        private static IContainer FicContainer;

        public FicViewModelLocator()
        {
            var builder = new ContainerBuilder();

            // ViewModels
            //Control de Inventarios -------------------------
            builder.RegisterType<FicVmConteoInventarioList>();
            builder.RegisterType<FicVmConteoInventarioItem>();
            builder.RegisterType<FicVmConteoInventarioItemInsert>();
            builder.RegisterType<FicVmConteoInventarioDelete>();
            builder.RegisterType<FicVmConteoInventarioDetails>();
            builder.RegisterType<FicVmInventariosDetList>();
            builder.RegisterType<FicVmInventariosDetItem>();
            builder.RegisterType<FicVmInventariosDetDetails>();
            //Control del Conteo de Inventarios Det
            builder.RegisterType<FicVmConteoDetInventarioList>();
            builder.RegisterType<FicVmConteoDetInventarioItem>();
            builder.RegisterType<FicVmConteoDetInventarioDetalle>();

            //builder.RegisterType<FicListVmInventario>();
            //Catalogo de Almacenes --------------------------
            builder.RegisterType<FicVmAlmacenList>();
            builder.RegisterType<FicVmAlmacenItem>();
            builder.RegisterType<FicVmAlmacenEditar>();
            builder.RegisterType<FicVmAlmacenEliminar>();
            builder.RegisterType<FicVmAlmacenDetalle>();



            // Services
            //Control de Inventarios --------------------------------------------------------------------------------
            builder.RegisterType<FicSrvNavigationInventario>().As<IFicSrvNavigationInventario>().SingleInstance();
            builder.RegisterType<FicSrvConteoInventarioList>().As<IFicSrvConteoInventario>();
            //Control de Conteos de Inventarios Det
            builder.RegisterType<FicSrvNavigationConteoDetInventarios>().As<IFicSrvNavigationConteoDetInventario>().SingleInstance();
            builder.RegisterType<FicSrvConteoDetInventarioList>().As<IFicSrvConteoDetInventario>();
            //Catalogo de Almacenes ---------------------------------------------------------------------------------
            builder.RegisterType<FicSrvNavigationAlmacen>().As<IFicSrvNavigationAlmacen>().SingleInstance();
            builder.RegisterType<FicSrvCatAlmacenList>().As<IFicSrvCatAlmacen>();

            if (FicContainer != null)
            {
                FicContainer.Dispose();
            }

            FicContainer = builder.Build();
        }



        //-------------------- CONTROL DE INVENTARIOS ------------------------
        /*public FicListVmInventario FicLiVmInventario
        {
            get { return FicContainer.Resolve<FicListVmInventario>(); }
        }*/

        //FIC: se manda llamar desde el backend de la View de List
        public FicVmConteoInventarioList FicVmConteoInventarioList
        {
            get { return FicContainer.Resolve<FicVmConteoInventarioList>(); }
        }

        //FIC: se manda llamar desde el backend de la View de Item
        public FicVmConteoInventarioItem FicVmConteoInventarioItem
        {
            get { return FicContainer.Resolve<FicVmConteoInventarioItem>(); }
        }

        public FicVmConteoInventarioItemInsert FicVmConteoInventarioItemInsert
        {
            get { return FicContainer.Resolve<FicVmConteoInventarioItemInsert>(); }
        }

        public FicVmConteoInventarioDetails FicVmConteoInventarioDetails
        {
            get { return FicContainer.Resolve<FicVmConteoInventarioDetails>(); }
        }

        public FicVmConteoInventarioDelete FicVmConteoInventarioDelete
        {
            get { return FicContainer.Resolve<FicVmConteoInventarioDelete>(); }
        }

        public FicVmInventariosDetList FicVmInventariosDetList
        {
            get { return FicContainer.Resolve<FicVmInventariosDetList>(); }
        }

        public FicVmInventariosDetItem FicVmInventariosDetItem
        {
            get { return FicContainer.Resolve<FicVmInventariosDetItem>(); }
        }

        public FicVmInventariosDetDetails FicVmInventariosDetDetails
        {
            get { return FicContainer.Resolve<FicVmInventariosDetDetails>(); }
        }

        //----------------- CONTROL DE INVENTARIOS CONTEOS DET ---------------------
        public FicVmConteoDetInventarioList Zt_InvConteosListVM
        {
            get { return FicContainer.Resolve<FicVmConteoDetInventarioList>(); }
        }

        public FicVmConteoDetInventarioItem Zt_InvConteosItemVM
        {
            get { return FicContainer.Resolve<FicVmConteoDetInventarioItem>(); }
        }

        public FicVmConteoDetInventarioDetalle Zt_InvConteosDetalleVM
        {
            get { return FicContainer.Resolve<FicVmConteoDetInventarioDetalle>(); }
        }

        
        //-------------------- CATALOGO DE ALMACENES ---------------------------
        //FIC: se manda llamar desde el backend de la View de List
        public FicVmAlmacenList FicVmAlmacenList
        {
            get { return FicContainer.Resolve<FicVmAlmacenList>(); }
        }

        //FIC: se manda llamar desde el backend de la View de List
        public FicVmAlmacenItem FicVmAlmacenItem
        {
            get { return FicContainer.Resolve<FicVmAlmacenItem>(); }
        }

        //FIC: se manda llamar desde el backend de la View de List
        public FicVmAlmacenEditar FicVmAlmacenEditar
        {
            get { return FicContainer.Resolve<FicVmAlmacenEditar>(); }
        }

        //FIC: se manda llamar desde el backend de la View de List
        public FicVmAlmacenEliminar FicVmAlmacenEliminar
        {
            get { return FicContainer.Resolve<FicVmAlmacenEliminar>(); }
        }

        //FIC: se manda llamar desde el backend de la View de List
        public FicVmAlmacenDetalle FicVmAlmacenDetalle
        {
            get { return FicContainer.Resolve<FicVmAlmacenDetalle>(); }
        }
    }
}
