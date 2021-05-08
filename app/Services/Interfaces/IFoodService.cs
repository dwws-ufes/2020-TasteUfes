using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TasteUfes.Data.Interfaces;
using TasteUfes.Models;
using VDS.RDF;

namespace TasteUfes.Services.Interfaces
{
    public interface IFoodService : IEntityService<Food>
    {
        IEnumerable<Food> GetAllLD(string foodName);
        IGraph GetGraph(string foodUriPrefix);
    }
}