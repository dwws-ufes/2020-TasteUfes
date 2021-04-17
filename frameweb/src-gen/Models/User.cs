using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Models
{
	public class User : Entity
	{
	
	    
			
				[Required]
			
			
				[StringLength(128)]
			
			
			
			public string FirstName { get; set; }
	    
	
	    
			
				[Required]
			
			
				[StringLength(128)]
			
			
			
			public string LastName { get; set; }
	    
	
	    
			
				[Required]
			
			
				[StringLength(16)]
			
			
				// Adicione a regra UNIQUE via Fluent API
			
			
			public string Username { get; set; }
	    
	
	    
			
				[Required]
			
			
			
			
			public string Password { get; set; }
	    
	
	    
			
				[Required]
			
			
			
			
			public string Email { get; set; }
	    
	
	
	
			
				/* Crie uma classe intermediária entre User e Role com as seguintes propriedades:
				
				[ForeignKey("UsersId")]
		        public User Users { get; set; }
		        public Guid UsersId { get; set; }
		
		        [ForeignKey("RolesId")]
		        public Role Roles { get; set; }
		        public Guid RolesId { get; set; }
				
				Por fim substitua o tipo Object abaixo */
				[InverseProperty("Users")]
				public IEnumerable<Object> Roles { get; set; }
			
		
	
			
				[InverseProperty("Users")]
				public IEnumerable<Recipe> Recipes { get; set; }
			
		
	
			
				/* !!! Caso essa entidade contenha mais de uma relação 1..1 entre User e Token,
				o mapeamento delas deve ser feito através da Fluent API. */
				[ForeignKey("TokenId")]
				public Token Token { get; set; }
				public Guid? TokenId { get; set; }
			
		
	
	
	
	}
}