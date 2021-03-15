using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Models
{
	public class NutritionFacts : Entity
	{
	
	    
			
				[Required]
			
			
			
			
			public double ServingSize { get; set; }
	    
	
	    
			
				[Required]
			
			
			
			
			public Measures ServingSizeUnit { get; set; }
	    
	
	    
			
				[Required]
			
			
			
			
			public double ServingEnergy { get; set; }
	    
	
	    
			
			
			
			
				[NotMapped]
			
			public double? DailyValue { get; set; }
	    
	
	
	
			
				[InverseProperty("NutritionFacts")]
				public IEnumerable<NutritionFactsNutrients> NutritionFactsNutrients { get; set; }
			
		
	
			
				/* !!! Caso essa entidade contenha mais de uma relação 1..1 entre Food e NutritionFacts,
				o mapeamento delas deve ser feito através da Fluent API. */
				public Food Food { get; set; }
			
		
	
	
	
	}
}