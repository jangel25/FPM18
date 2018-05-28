using Autofac;
using AppCocacolaNayMobiV2.Services.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.ViewModels.Planeaciones;
using AppCocacolaNayMobiV2.Interfaces.Planeaciones;
using AppCocacolaNayMobiV2.Services.Planeaciones;

namespace AppCocacolaNayMobiV2.ViewModels.Base
{
    public class FicViewModelLocator
    {
        private static IContainer FicContainer;

        public FicViewModelLocator()
        {
            var builder = new ContainerBuilder();

            // ViewModels
            //Planeaciones
            builder.RegisterType<VmEvaPlaneacionList>();
            builder.RegisterType<VmEvaPlaneacionItem>();
            builder.RegisterType<VmEvaPlaneacionDetalle>();

            //Planeaciones temas
            builder.RegisterType<VmEvaPlaneacionTemasList>();
            builder.RegisterType<VmEvaPlaneacionTemasItem>();
            builder.RegisterType<VmEvaPlaneacionTemasDetalle>();

            //Planeaciones subtemas
            builder.RegisterType<VmEvaPlanSubtemasList>();
            builder.RegisterType<VmEvaPlanSubtemasItem>();
            builder.RegisterType<VmEvaPlanSubtemasDetalle>();

            //Planeaciones fuentes bibliograficas
            builder.RegisterType<VmEvaCatFuentesList>();
            builder.RegisterType<VmEvaCatFuentesItem>();
            builder.RegisterType<VmEvaCatFuentesDetalle>();

            //Planeaciones apoyos didacticos
            builder.RegisterType<VmEvaCatApoyosList>();
            builder.RegisterType<VmEvaCatApoyosItem>();
            builder.RegisterType<VmEvaCatApoyosDetalle>();

            //Services
            builder.RegisterType<SrvNavigationPlaneaciones>().As<INavigationPlaneacion>().SingleInstance();
            builder.RegisterType<SrvPlaneaciones>().As<ISrvPlaneacion>();

            if (FicContainer != null)
            {
                FicContainer.Dispose();
            }

            FicContainer = builder.Build();
        }

        //EVA_PLANEACIONES
        public VmEvaPlaneacionList Eva_planeacionList
        {
            get { return FicContainer.Resolve<VmEvaPlaneacionList>(); }
        }

        public VmEvaPlaneacionItem Eva_planeacionItem
        {
            get { return FicContainer.Resolve<VmEvaPlaneacionItem>(); }
        }

        public VmEvaPlaneacionDetalle Eva_planeacionDetalle
        {
            get { return FicContainer.Resolve<VmEvaPlaneacionDetalle>(); }
        }

        //EVA_PLANEACIONES_TEMAS
        public VmEvaPlaneacionTemasList Eva_planeacion_temasList
        {
            get { return FicContainer.Resolve<VmEvaPlaneacionTemasList>(); }
        }

        public VmEvaPlaneacionTemasItem Eva_planeacion_temasItem
        {
            get { return FicContainer.Resolve<VmEvaPlaneacionTemasItem>(); }
        }

        public VmEvaPlaneacionTemasDetalle Eva_planeacion_temasDetalle
        {
            get { return FicContainer.Resolve<VmEvaPlaneacionTemasDetalle>(); }
        }

        //EVA_PLANEACIONES_SUBTEMAS
        public VmEvaPlanSubtemasList Eva_planeacion_subtemasList
        {
            get { return FicContainer.Resolve<VmEvaPlanSubtemasList>(); }
        }

        public VmEvaPlanSubtemasItem Eva_planeacion_subtemasItem
        {
            get { return FicContainer.Resolve<VmEvaPlanSubtemasItem>(); }
        }

        public VmEvaPlanSubtemasDetalle Eva_planeacion_subtemasDetalle
        {
            get { return FicContainer.Resolve<VmEvaPlanSubtemasDetalle>(); }
        }

        //EVA_CAT_FUENTES_BIBLIOGRAFICAS
        public VmEvaCatFuentesList Eva_cat_fuentesList
        {
            get { return FicContainer.Resolve<VmEvaCatFuentesList>(); }
        }

        public VmEvaCatFuentesItem Eva_cat_fuentesItem
        {
            get { return FicContainer.Resolve<VmEvaCatFuentesItem>(); }
        }

        public VmEvaCatFuentesDetalle Eva_cat_fuentesDetalle
        {
            get { return FicContainer.Resolve<VmEvaCatFuentesDetalle>(); }
        }

        //EVA_CAT_APOYOS_DIDACTICOS
        public VmEvaCatApoyosList Eva_cat_apoyosList
        {
            get { return FicContainer.Resolve<VmEvaCatApoyosList>(); }
        }

        public VmEvaCatApoyosItem Eva_cat_apoyosItem
        {
            get { return FicContainer.Resolve<VmEvaCatApoyosItem>(); }
        }

        public VmEvaCatApoyosDetalle Eva_cat_apoyosDetalle
        {
            get { return FicContainer.Resolve<VmEvaCatApoyosDetalle>(); }
        }
    }
}
