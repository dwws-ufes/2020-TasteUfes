using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Models
{
	public class Recipe : Entity
	{
	
	    
			
				[Required]
			
			
				[StringLength(256)]
			
			
			
			public string Name { get; set; }
	    
	
	    
			
				[Required]
			
			
			
			
			public int Servings { get; set; }
	    
	
	    
			
			
			
				// Adicione a regra UNIQUE via Fluent API
			
			
				[NotMapped]
			
			public NutritionFacts? NutritionFacts { get; set; }
	    
	
	
	
			
				/* !!! Caso essa entidade contenha mais de uma relação 1..1 entre Preparation e Recipe,
				o mapeamento delas deve ser feito através da Fluent API. */
				public Preparation Preparation { get; set; }
			
		
	
			
				[InverseProperty("Recipe")]
				public IEnumerable<Ingredient> Ingredients { get; set; }
			
		
	
			
				[ForeignKey("UsersId")]
				public User Users { get; set; }
				public Guid UsersId { get; set; }
			
		
	
	
	
	}
}