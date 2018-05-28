using System.Collections.Generic;
using System.Threading.Tasks;
using AppCocacolaNayMobiV2.Models.Planeaciones;

namespace AppCocacolaNayMobiV2.Interfaces.Planeaciones
{
    public interface ISrvPlaneacion
    {
        //Eva_planeacion
        Task<IList<Eva_planeacion>> GetAll_eva_planeacion();
        Task Insert_eva_planeacion(Eva_planeacion eva_planeacion);
        Task Remove_eva_planeacion(Eva_planeacion eva_planeacion);

        //Eva_planeacion_temas
        Task<IList<Eva_planeacion_temas>> GetAll_eva_planeacion_temas();
        Task Insert_eva_planeacion_temas(Eva_planeacion_temas eva_planeacion_temas);
        Task Remove_eva_planeacion_temas(Eva_planeacion_temas eva_planeacion_temas);

        //Eva_planeacion_subtemas
        Task<IList<Eva_planeacion_subtemas>> GetAll_eva_planeacion_subtemas();
        Task Insert_eva_planeacion_subtemas(Eva_planeacion_subtemas eva_planeacion_subtemas);
        Task Remove_eva_planeacion_subtemas(Eva_planeacion_subtemas eva_planeacion_subtemas);

        //Eva_planeacion_fuentes
        //Task<IList<Eva_planeacion>> GetAll_eva_planeacion();
        Task Insert_eva_planeacion_fuentes(Eva_planeacion_fuentes eva_planeacion_fuentes);
        Task Remove_eva_planeacion_fuentes(Eva_planeacion_fuentes eva_planeacion_fuentes);

        //Eva_cat_fuentes_bibliograficas
        Task<IList<Eva_cat_fuentes_bibliograficas>> GetAll_eva_cat_fuentes_bibliograficas();
        Task Insert_eva_cat_fuentes_bibliograficas(Eva_cat_fuentes_bibliograficas eva_cat_fuentes_bibliograficas);
        Task Remove_eva_cat_fuentes_bibliograficas(Eva_cat_fuentes_bibliograficas eva_cat_fuentes_bibliograficas);

        //Eva_cat_apoyos_didacticos
        Task<IList<Eva_cat_apoyos_didacticos>> GetAll_eva_cat_apoyos_didacticos();
        Task Insert_eva_cat_apoyos_didacticos(Eva_cat_apoyos_didacticos eva_cat_apoyos_didacticos);
        Task Remove_eva_cat_apoyos_didacticos(Eva_cat_apoyos_didacticos eva_cat_apoyos_didacticos);
    }
}
