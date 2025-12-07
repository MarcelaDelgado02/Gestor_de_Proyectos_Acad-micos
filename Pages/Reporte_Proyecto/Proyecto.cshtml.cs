using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.Entidades;
using Microsoft.AspNetCore.Authorization;
using Azure;

namespace Gestor_de_Proyectos_Académicos.Pages.Reporte_Proyecto
{
    [Authorize(Roles = "Profesor")]
    public class ProyectosModel : PageModel
    {

        

       
    }

}
