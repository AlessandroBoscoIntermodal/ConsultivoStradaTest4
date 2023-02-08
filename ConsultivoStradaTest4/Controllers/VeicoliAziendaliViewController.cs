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
    public class VeicoliAziendaliViewController : Controller
    {
        private ConsuntivoStradaleIMTContext _context;

        public VeicoliAziendaliViewController(ConsuntivoStradaleIMTContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var veicoliaziendaliviews = _context.VeicoliAziendaliViews.Select(i => new {
                i.IdveicoloTarga,
                i.FkeyTipoVeicolo,
                i.TipoVeicolo,
                i.FkeyContainerDescrizione,
                i.Container
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdveicoloTarga" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(veicoliaziendaliviews, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new VeicoliAziendaliView();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.VeicoliAziendaliViews.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdveicoloTarga });
        }

        [HttpPut]
        public async Task<IActionResult> Put(string key, string values) {
            var model = await _context.VeicoliAziendaliViews.FirstOrDefaultAsync(item => item.IdveicoloTarga == key);
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
            var model = await _context.VeicoliAziendaliViews.FirstOrDefaultAsync(item => item.IdveicoloTarga == key);

            _context.VeicoliAziendaliViews.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(VeicoliAziendaliView model, IDictionary values) {
            string IDVEICOLO_TARGA = nameof(VeicoliAziendaliView.IdveicoloTarga);
            string FKEY_TIPO_VEICOLO = nameof(VeicoliAziendaliView.FkeyTipoVeicolo);
            string TIPO_VEICOLO = nameof(VeicoliAziendaliView.TipoVeicolo);
            string FKEY_CONTAINER_DESCRIZIONE = nameof(VeicoliAziendaliView.FkeyContainerDescrizione);
            string CONTAINER = nameof(VeicoliAziendaliView.Container);

            if(values.Contains(IDVEICOLO_TARGA)) {
                model.IdveicoloTarga = Convert.ToString(values[IDVEICOLO_TARGA]);
            }

            if(values.Contains(FKEY_TIPO_VEICOLO)) {
                model.FkeyTipoVeicolo = Convert.ToString(values[FKEY_TIPO_VEICOLO]);
            }

            if(values.Contains(TIPO_VEICOLO)) {
                model.TipoVeicolo = Convert.ToString(values[TIPO_VEICOLO]);
            }

            if(values.Contains(FKEY_CONTAINER_DESCRIZIONE)) {
                model.FkeyContainerDescrizione = Convert.ToInt32(values[FKEY_CONTAINER_DESCRIZIONE]);
            }

            if(values.Contains(CONTAINER)) {
                model.Container = Convert.ToString(values[CONTAINER]);
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