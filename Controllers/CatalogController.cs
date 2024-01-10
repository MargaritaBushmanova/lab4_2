
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using lab4_2.Models;


namespace lab4_2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {

        private readonly ILogger<CatalogController> _logger;
        private readonly Catalog _catalog;

        public CatalogController(ILogger<CatalogController> logger)
        {
            _logger = logger;
            _logger.LogInformation("CatalogController создан");
            _catalog = new Catalog();
            _catalog.compositions.Add(new Composition("11", "22"));
            _catalog.KeepData_SQL(true);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Composition>> GetCompositions()
        {
            _logger.LogInformation("Метод GetCompositions был вызван.");
            return Ok(_catalog.compositions);
        }

        [HttpGet("search/{keyword}")]
        public ActionResult<IEnumerable<Composition>> SearchCompositions(string keyword)
        {
            
            var results = "";
            foreach (Composition item in _catalog.compositions)
            {
                if (item.ToString().Contains(keyword))
                {
                    results += item.ToString() + "\n";
                }
            }
            return Ok(results);
        }

        [HttpPost("add")]
        public ActionResult<string> AddComposition([FromBody] Composition composition)
        {
            _logger.LogInformation("Метод AddComposition был вызван.");
            _catalog.compositions.Add(composition);
            return Ok("Композиция успешно добавлена!");
        }

        [HttpDelete("delete/{id}")]
        public ActionResult<string> DeleteComposition(string id)
        { 

            var warning = "";

            foreach (Composition item in _catalog.compositions)
            {
                if (id == item.ToString())
                {
                    _catalog.compositions.Remove(item);
                    warning += "Композиция успешно удалена";
                    break;
                }
            }
            if (warning.Length == 0)
                warning += "Композиция для удаления не найдена!";
            return Ok(warning);
        }
    }


}
