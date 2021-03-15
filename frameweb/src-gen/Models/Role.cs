using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Models
{
	public class Role : Entity
	{
	
	    
			
				[Required]
			
			
				[StringLength(16)]
			
			
				// Adicione a regra UNIQUE via Fluent API
			
			
			public string Name { get; set; }
	    
	
	
	
			
				/* Crie uma classe intermediária entre User e Role com as seguintes propriedades:
				
				[ForeignKey("UsersId")]
		        public User Users { get; set; }
		        public Guid UsersId { get; set; }
		
		        [ForeignKey("RolesId")]
		        public Role Roles { get; set; }
		        public Guid RolesId { get; set; }
				
				Por fim substitua o tipo Object abaixo*/
				[InverseProperty("Roles")]
				public IEnumerable<Object> Users { get; set; }
			
		
	
	
	
	}
}