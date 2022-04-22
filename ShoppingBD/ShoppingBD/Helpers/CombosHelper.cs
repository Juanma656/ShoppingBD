using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingBD.Data;

namespace ShoppingBD.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper (DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync()
        {
            List<SelectListItem> list = await _context.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem{Text = "[Seleccione una categoría...]", Value = "0"});

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(int stateId)
        {
            List<SelectListItem> list = await _context.Cities
                .Where(c => c.State.Id == stateId)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Seleccione una ciudad...]", Value = "0" });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCountriesAsync()
        {
            List<SelectListItem> list = await _context.Countries.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Seleccione una país...]", Value = "0" });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int countryId)
        {
            List<SelectListItem> list = await _context.States
                .Where(s => s.Country.Id == countryId)
                .Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Seleccione un Departamento/Estado...]", Value = "0" });

            return list;
        }
    }
}
