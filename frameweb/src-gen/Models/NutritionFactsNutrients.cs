using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Models
{
	public class NutritionFactsNutrients : Entity
	{
	
	    
			
				[Required]
			
			
			
			
			public double AmountPerServing { get; set; }
	    
	
	    
			
				[Required]
			
			
			
			
			public Measures AmountPerServingUnit { get; set; }
	    
	
	    
			
			
			
			
				[NotMapped]
			
			public double? DailyValue { get; set; }
	    
	
	
	
			
				[ForeignKey("NutritionFactsId")]
				public NutritionFacts NutritionFacts { get; set; }
				public Guid NutritionFactsId { get; set; }
			
		
	
			
				[ForeignKey("NutrientId")]
				public Nutrient Nutrient { get; set; }
				public Guid NutrientId { get; set; }
			
		
	
	
	
	}
}