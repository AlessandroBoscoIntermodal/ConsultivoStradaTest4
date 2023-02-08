using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ConsultivoStradaTest4.Models.CS;

namespace ConsultivoStradaTest4.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AbbinamentoTrattoreController : Controller
    {
        private ConsuntivoStradaleIMTContext _context;

        public AbbinamentoTrattoreController(ConsuntivoStradaleIMTContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var abbinamentotrattores = _context.AbbinamentoTrattores.Select(i => new {
                i.FkeyTrattore,
                i.FkeyAutista,
                i.FkeyGestore,
                i.DataInizio,
                i.DataFine,
                i.KeyDataInizio
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "FkeyTrattore", "KeyDataInizio" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(abbinamentotrattores, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new AbbinamentoTrattore();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.AbbinamentoTrattores.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.FkeyTrattore, result.Entity.KeyDataInizio });
        }

        [HttpPut]
        public async Task<IActionResult> Put(string key, string values) {
            var keys = JsonConvert.DeserializeObject<IDictionary>(key);
            var keyFkeyTrattore = Convert.ToString(keys["FkeyTrattore"]);
            var keyKeyDataInizio = Convert.ToInt32(keys["KeyDataInizio"]);
            var model = await _context.AbbinamentoTrattores.FirstOrDefaultAsync(item =>
                            item.FkeyTrattore == keyFkeyTrattore && 
                            item.KeyDataInizio == keyKeyDataInizio);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(string key) {
            var keys = JsonConvert.DeserializeObject<IDictionary>(key);
            var keyFkeyTrattore = Convert.ToString(keys["FkeyTrattore"]);
            var keyKeyDataInizio = Convert.ToInt32(keys["KeyDataInizio"]);
            var model = await _context.AbbinamentoTrattores.FirstOrDefaultAsync(item =>
                            item.FkeyTrattore == keyFkeyTrattore && 
                            item.KeyDataInizio == keyKeyDataInizio);

            _context.AbbinamentoTrattores.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(AbbinamentoTrattore model, IDictionary values) {
            string FKEY_TRATTORE = nameof(AbbinamentoTrattore.FkeyTrattore);
            string FKEY_AUTISTA = nameof(AbbinamentoTrattore.FkeyAutista);
            string FKEY_GESTORE = nameof(AbbinamentoTrattore.FkeyGestore);
            string DATA_INIZIO = nameof(AbbinamentoTrattore.DataInizio);
            string DATA_FINE = nameof(AbbinamentoTrattore.DataFine);
            string KEY_DATA_INIZIO = nameof(AbbinamentoTrattore.KeyDataInizio);

            if(values.Contains(FKEY_TRATTORE)) {
                model.FkeyTrattore = Convert.ToString(values[FKEY_TRATTORE]);
            }

            if(values.Contains(FKEY_AUTISTA)) {
                model.FkeyAutista = values[FKEY_AUTISTA] != null ? Convert.ToInt32(values[FKEY_AUTISTA]) : (int?)null;
            }

            if(values.Contains(FKEY_GESTORE)) {
                model.FkeyGestore = Convert.ToString(values[FKEY_GESTORE]);
            }

            if(values.Contains(DATA_INIZIO)) {
                model.DataInizio = Convert.ToDateTime(values[DATA_INIZIO]);
            }

            if(values.Contains(DATA_FINE)) {
                model.DataFine = values[DATA_FINE] != null ? Convert.ToDateTime(values[DATA_FINE]) : (DateTime?)null;
            }

            if(values.Contains(KEY_DATA_INIZIO)) {
                model.KeyDataInizio = Convert.ToInt32(values[KEY_DATA_INIZIO]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}