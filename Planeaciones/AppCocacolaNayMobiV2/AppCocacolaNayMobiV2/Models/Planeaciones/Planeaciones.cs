using System;
using System.Collections.ObjectModel;
using SQLite;

namespace AppCocacolaNayMobiV2.Models.Planeaciones
{
   public class Eva_planeacion
        {
            [PrimaryKey, AutoIncrement]
            public int IdPlaneacion { get; set; }
            [MaxLength(20)]
            public string ReferenciaNorma { get; set; }
            [MaxLength(20)]
            public string Revision { get; set; }
            public bool Actual { get; set; }
            [MaxLength(20)]
            public string CompetenciaAsignatura { get; set; }
            [MaxLength(20)]
            public string AportacionPerfilEgreso { get; set; }
    }

        public class Eva_planeacion_temas
        {
            [PrimaryKey, AutoIncrement]
            public int IdTema { get; set; }
            [MaxLength(20)]
            public string DesTema { get; set; }
            [MaxLength(20)]
            public string Observaciones { get; set; }
            public int IdPlaneacion { get; set; }
        }

        public class Eva_planeacion_subtemas
        {
            [PrimaryKey, AutoIncrement]
            public int IdSubtema { get; set; }
            [MaxLength(255)]
            public string DesSubtema { get; set; }
            public int IdTema { get; set; }
        }

        public class Eva_planeacion_fuentes
        {
            public int IdFuente { get; set; }
            public int Prioridad { get; set; }
            public int IdPlaneacion { get; set; }
        }

        public class Eva_cat_fuentes_bibliograficas
        {
            [PrimaryKey, AutoIncrement]
            public int IdFuente { get; set; }
            [MaxLength(255)]
            public string DesFuenteCompleta { get; set; }
            public bool Activo { get; set; }
            [MaxLength(20)]
            public string NombreFuente { get; set; }
            [MaxLength(20)]
            public string Autor { get; set; }
            [MaxLength(20)]
            public string Editorial { get; set; }
            public int IdPlaneacion { get; set; }
        }

        public class Eva_cat_apoyos_didacticos
        {
            [PrimaryKey, AutoIncrement]
            public int IdApoyoDidactico { get; set; }
            [MaxLength(255)]
            public string DesApoyoDidactico { get; set; }
            public bool Activo { get; set; }
            public string FechaReg { get; set; }
            public int IdPlaneacion { get; set; }
        }
    }
