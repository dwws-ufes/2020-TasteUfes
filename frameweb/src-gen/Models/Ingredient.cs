using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Models
{
	public class Ingredient : Entity
	{
	
	    
			
				[Required]
			
			
			
			
			public double Quantity { get; set; }
	    
	
	    
			
				[Required]
			
			
			
			
			public Measures QuantityUnit { get; set; }
	    
	
	
	
			
				[ForeignKey("FoodId")]
				public Food Food { get; set; }
				public Guid FoodId { get; set; }
			
		
	
			
				[ForeignKey("RecipeId")]
				public Recipe Recipe { get; set; }
				public Guid RecipeId { get; set; }
			
		
	
	
	
	}
}