using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Models
{
	public class PreparationStep : Entity
	{
	
	    
			
				[Required]
			
			
			
			
			public int Step { get; set; }
	    
	
	    
			
				[Required]
			
			
				[StringLength(2048)]
			
			
			
			public string Description { get; set; }
	    
	
	
	
			
				[ForeignKey("PreparationId")]
				public Preparation Preparation { get; set; }
				public Guid PreparationId { get; set; }
			
		
	
	
	
	}
}