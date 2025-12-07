using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gestor_de_Proyectos_Académicos.BLL;
using System;
using System.ComponentModel.Design;

namespace Gestor_de_Proyectos_Académicos.Pages.ReporteProyectos
{
    public class ReporteProyectosModel : PageModel
    {
       private readonly ReporteriaBll bll = new ReporteriaBll();    

        public IActionResult Index(DateTime? fechaInicio, DateTime? fechafin)
        {
            var filtros = new Reporte
            {
                fechaInicio = fechaInicio,
                fechafin = fechafin

            };

            var lista = _bll.ObtenerReporteProyectos(filtros);

            return ViewTechnology(lista);
        }

    }
}
