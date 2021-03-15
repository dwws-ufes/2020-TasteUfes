using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Models
{
	public class Preparation : Entity
	{
	
	    
			
				[Required]
			
			
			
			
			public TimeSpan PreparationTime { get; set; }
	    
	
	
	
			
				/* !!! Caso essa entidade contenha mais de uma relação 1..1 entre Preparation e Recipe,
				o mapeamento delas deve ser feito através da Fluent API. */
				[ForeignKey("RecipeId")]
				public Recipe Recipe { get; set; }
				public Guid? RecipeId { get; set; }
			
		
	
			
				[InverseProperty("Preparation")]
				public IEnumerable<PreparationStep> Steps { get; set; }
			
		
	
	
	
	}
}