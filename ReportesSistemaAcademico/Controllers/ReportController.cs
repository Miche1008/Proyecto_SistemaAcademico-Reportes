using System;
using System.IO;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;

namespace ReportesSistemaAcademico.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult DescargarReporteEstudiantes()
        {
            ReportDocument rd = new ReportDocument();
            string ruta = Server.MapPath("~/Reports/ReporteEstudiantes.rpt");

            if (!System.IO.File.Exists(ruta))
                throw new Exception("No se encontró el archivo del reporte en: " + ruta);

            rd.Load(ruta);

            rd.Refresh();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf", "ReporteEstudiantes.pdf");
        }

        [HttpGet]
        [Route("DescargarReporteNotas")]
        public ActionResult DescargarReporteNotas(string correo)
        {
            if (string.IsNullOrEmpty(correo))
                return new HttpStatusCodeResult(400, "Debe proporcionar un correo válido.");

            ReportDocument rd = new ReportDocument();
            rd.Load(Server.MapPath("~/Reports/ReporteNotas.rpt"));

            rd.Refresh();

            rd.SetParameterValue("CorreoEstudiante", correo);

            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf", "ReporteNotas.pdf");
        }
    }
}