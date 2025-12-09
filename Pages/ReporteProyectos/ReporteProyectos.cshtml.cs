using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gestor_de_Proyectos_Académicos.BLL;
using System;
using System.ComponentModel.Design;
using Gestor_de_Proyectos_Académicos.Entidades;

namespace Gestor_de_Proyectos_Académicos.Pages.ReporteProyectos
{
    public class ReporteProyectosModel : PageModel
    {
        private readonly ReporteriaBLL reporteriaBLL = new ReporteriaBLL();
        public List<Reporte> ListaReporte { get; set; }

        public void OnGet(DateTime? fechaInicio, DateTime? fechaFin)
        {
            int idProfesor = HttpContext.Session.GetInt32("IdUsuario") ?? 0;
            int rol = HttpContext.Session.GetInt32("Rol") ?? 0;

            
            if (rol != 1)
            {
                ListaReporte = new List<Reporte>();
                return;
            }

            var filtros = new Reporte
            {
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };

            ListaReporte = reporteriaBLL.ObtenerReporteProyectos(filtros, idProfesor);
        }
    }

}
