using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using KissLog;
using FluentValidation;



namespace Services
{
	public class NutritionFactsService : EntityService<NutritionFacts>, INutritionFactsService
	{
		public NutritionFactsService(IUnitOfWork unitOfWork, INotificator notificator, ILogger logger)
			: base(unitOfWork, notificator, logger) { }
		
		
		
		
			

			
				
			

			public NutritionFacts Reduce(IEnumerable nutritionFacts)
			{
				throw new NotImplementedException();
			}
		
	}
}
