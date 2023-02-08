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
    public class AutistiViewController : Controller
    {
        private ConsuntivoStradaleIMTContext _context;

        public AutistiViewController(ConsuntivoStradaleIMTContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var autistiviews = _context.AutistiViews.Select(i => new {
                i.IdAutista,
                i.Nominativo
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdAutista" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(autistiviews, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new AutistiView();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.AutistiViews.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdAutista });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.AutistiViews.FirstOrDefaultAsync(item => item.IdAutista == key);
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
        public async Task Delete(int key) {
            var model = await _context.AutistiViews.FirstOrDefaultAsync(item => item.IdAutista == key);

            _context.AutistiViews.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(AutistiView model, IDictionary values) {
            string ID_AUTISTA = nameof(AutistiView.IdAutista);
            string NOMINATIVO = nameof(AutistiView.Nominativo);

            if(values.Contains(ID_AUTISTA)) {
                model.IdAutista = Convert.ToInt32(values[ID_AUTISTA]);
            }

            if(values.Contains(NOMINATIVO)) {
                model.Nominativo = Convert.ToString(values[NOMINATIVO]);
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