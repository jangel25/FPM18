using System;
using System.Collections.Generic;
using System.Text;

namespace proyectoFinal.Models
{
    public class clasesModelo
    {
        public class eva_planeacion_subtemas
        {
            public int IdSubtema { get; set; }
            public int IdAsignatura { get; set; }
            public int IdTema { get; set; }
            public String DesSubtema { get; set; }
        }

        public class eva_planeacion_temas
        {
            public int IdAsignatura { get; set; }
            public int IdPlaneacion { get; set; }
            public int IdTema { get; set; }
            public String DesTema { get; set; }
            public String Observaciones { get; set; }
        }

        public class eva_planeacion
        {
            public int IdAsignatura { get; set; }
            public int IdPlaneacion { get; set; }
            public String ReferenciaNorma { get; set; }
            public String Revision { get; set; }
            public Char Actual { get; set; }
            public String CompetenciaAsignatura { get; set; }
            public String AportacionPerfilEgreso { get; set; }
        }

        public class eva_planeacion_fuentes
        {
            public int IdAsignatura { get; set; }
            public int IdPlaneacion { get; set; }
            public int IdFuente { get; set; }
            public int Prioridad { get; set; }
        }

        public class eva_planeacion_apoyos
        {
            public int IdAsignatura { get; set; }
            public int IdPlaneacion { get; set; }
            public int IdApoyoDidactico { get; set; }
        }

        public class eva_planeacion_temas_competencias
        {
            public int IdAsignatura { get; set; }
            public int IdPlaneacion { get; set; }
            public int IdTema { get; set; }
            public int IdCompetencia { get; set; }
            public String Observaciones { get; set; }
        }

        public class eva_planeacion_aprendizaje
        {
            public int IdAsignatura { get; set; }
            public int IdPlaneacion { get; set; }
            public int IdTema { get; set; }
            public int IdCompetencia { get; set; }
            public int IdActividadAprendizaje { get; set; }
        }

        public class eva_planeacion_ensenanza
        {
            public int IdAsignatura { get; set; }
            public int IdPlaneacion { get; set; }
            public int IdTema { get; set; }
            public int IdCompetencia { get; set; }
            public int IdActividadEnsenanza { get; set; }
            public DateTime FechaReg { get; set; }
            public DateTime FechaProgramada { get; set; }
            public DateTime FechaRealizada { get; set; }
        }

        public class eva_planeacion_criterios_evalua
        {
            public int IdAsignatura { get; set; }
            public int IdPlaneacion { get; set; }
            public int IdTema { get; set; }
            public int IdCompetencia { get; set; }
            public int IdCriterio { get; set; }
            public String DesCriterio { get; set; }
            public float Porcentaje { get; set; }
        }

        public class eva_planeacion_mejora_desempeno
        {
            public int IdAsignatura { get; set; }
            public int IdPlaneacion { get; set; }
            public int IdTema { get; set; }
            public int IdMejora { get; set; }
            public String DesMejora { get; set; }
        }

        public class eva_cat_fuentes_bibliograficas
        {
            public int IdFuente { get; set; }
            public String DesFuenteCompleta { get; set; }
            public Char Activo { get; set; }
            public String NombreFuente { get; set; }
            public String Autor { get; set; }
            public String Editorial { get; set; }
        }

        public class eva_cat_apoyos_didacticos
        {
            public int IdApoyoDidactico { get; set; }
            public Char Activo { get; set; }
            public DateTime FechaReg { get; set; }
        }

        public class eva_cat_actividades_aprendizaje
        {
            public int IdActividadAprendizaje { get; set; }
            public String DesActividadAprendizaje { get; set; }
            public Char Activo { get; set; }
            public DateTime FechaReg { get; set; }
        }

        public class eva_cat_actividades_ensenanza
        {
            public int IdActividadEnsenanza { get; set; }
            public String DesActividadEnsenanza { get; set; }
            public Char Activo { get; set; }
            public DateTime FechaReg { get; set; }
        }

        public class eva_planeacion_factores_evaluar
        {
            public int IdAsignatura { get; set; }
            public int IdPlaneacion { get; set; }
            public int IdTema { get; set; }
            public int IdCompetencia { get; set; }
            public int IdIndicador { get; set; }
            public int IdActividadAprendizaje { get; set; }
            public float Calificacion { get; set; }
            public int IdNivelDominio { get; set; }
            public int IdTipoEstatusCalificacion { get; set; }
            public int IdGenEstatusCalificacion { get; set; }
        }
    }
}
