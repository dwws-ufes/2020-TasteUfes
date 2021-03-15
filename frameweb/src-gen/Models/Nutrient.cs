using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Models
{
	public class Nutrient : Entity
	{
	
	    
			
				[Required]
			
			
				[StringLength(64)]
			
			
				// Adicione a regra UNIQUE via Fluent API
			
			
			public string Name { get; set; }
	    
	
	    
			
				[Required]
			
			
			
			
			public double EnergyPerGram { get; set; }
	    
	
	    
			
				[Required]
			
			
			
			
			public double DailyRecommendation { get; set; }
	    
	
	
	
			
				[InverseProperty("Nutrient")]
				public IEnumerable<NutritionFactsNutrients> NutritionFactsNutrients { get; set; }
			
		
	
	
	
	}
}