using System;
using System.Collections.Generic;
using System.Linq;
using Hsf_Receitas.Data;
using Hsf_Receitas.Models;
using Hsf_Receitas.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hsf_Receitas.Controllers
{

    public class MedicacaoController : Controller
    {
        private readonly ILogger<MedicacaoController> _logger;

        public MedicacaoController(ILogger<MedicacaoController> logger)
        {
            _logger = logger;
        }

        [HttpPost]

        public IActionResult MedicationRegister(Medicacao novaMedicacao)
        {
            try
            {

                MedicacaoServices medServ = new MedicacaoServices();
                medServ.AddMedicacao(novaMedicacao);

                return Json(new { stats = "OK" });

            }
            catch (Exception e)
            {
                _logger.LogError("Erro ao adicionar medicamento!" + e.Message);
                return Json(new { stats = "INVALID", message = "Falha ao inserir medicamento!" });
            }
        }

        [HttpPost]
        public IActionResult DeleteMedication(int id, int recId)
        {

            try
            {
                HSFContext database = new HSFContext();
                MedicacaoServices medServ = new MedicacaoServices();

                var item = database.Medicamentos.Find(id);

                if (item != null)
                {
                    database.Medicamentos.Remove(item);
                    database.SaveChanges();
                }else {
                    return Json(new { stats = "INVALID"});
                }

                return Json(new { stats = "OK"});
                
            }
            catch (Exception e)
            {
                _logger.LogError("Erro ao medicamento do Receituário!" + e.Message);
                return Json(new { stats = "INVALID", message = "Falha ao excluir medicamento!" });
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}