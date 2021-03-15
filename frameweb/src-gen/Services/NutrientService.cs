using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using KissLog;
using FluentValidation;



namespace Services
{
	public class NutrientService : EntityService<Nutrient>, INutrientService
	{
		public NutrientService(IUnitOfWork unitOfWork, INotificator notificator, ILogger logger)
			: base(unitOfWork, notificator, logger) { }
		
		
		
		
	}
}
