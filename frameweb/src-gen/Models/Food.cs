using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Models
{
	public class Food : Entity
	{
	
	    
			
				[Required]
			
			
				[StringLength(256)]
			
			
				// Adicione a regra UNIQUE via Fluent API
			
			
			public string Name { get; set; }
	    
	
	
	
			
				[InverseProperty("Food")]
				public IEnumerable<Ingredient> Ingredients { get; set; }
			
		
	
			
				/* !!! Caso essa entidade contenha mais de uma relação 1..1 entre Food e NutritionFacts,
				o mapeamento delas deve ser feito através da Fluent API. */
				[ForeignKey("NutritionFactsId")]
				public NutritionFacts NutritionFacts { get; set; }
				public Guid? NutritionFactsId { get; set; }
			
		
	
	
	
	}
}