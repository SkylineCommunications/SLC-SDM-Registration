namespace Skyline.DataMiner.SDM.Registration
{
	using System.Linq;

	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.SDM.Registration.Exceptions;

	[AllowSdmMiddleware]
	public interface ISolutionRepository : IBulkRepository<SolutionRegistration>
	{
		/// <summary>
		/// Retrieves a solution registration by its string identifier.
		/// </summary>
		/// <param name="solutionId">The string identifier of the solution.</param>
		/// <returns>The matching <see cref="SolutionRegistration"/>.</returns>
		/// <exception cref="RegistrationNotFoundException">Thrown if no solution is found with the specified identifier.</exception>
		SolutionRegistration GetSolutionById(string solutionId);
	}

	internal partial class SolutionRegistrationDomRepository : ISolutionRepository
	{
		public SolutionRegistration GetSolutionById(string solutionId)
		{
			var solution = Read(SolutionRegistrationExposers.ID.Equal(solutionId)).FirstOrDefault();
			if (solution is null)
			{
				throw new RegistrationNotFoundException($"Solution Registration with ID: {solutionId} was not found.");
			}

			return solution;
		}
	}
}
