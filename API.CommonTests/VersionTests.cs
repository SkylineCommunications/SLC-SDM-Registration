namespace Skyline.DataMiner.SDM.Registration.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using FluentAssertions;

	using Shared;

	[TestClass]
	public class VersionTests
	{
		[TestMethod]
		[DataRow("1.0.0", 1u, 0u, 0u)]
		[DataRow("1.1.1", 1u, 1u, 1u)]
		[DataRow("1.1.2-suffix", 1u, 1u, 2u, null, "suffix")]
		[DataRow("1.2.1.0", 1u, 2u, 1u, 0u)]
		[DataRow("2.1.4.0-rc1", 2u, 1u, 4u, 0u, "rc1")]
		public void Version_Parsing_Success(string version, uint major, uint minor, uint patch, uint? revision = null, string? suffix = null)
		{
			// Act
			var result = default(SdmVersion);
			var act = () => result = SdmVersion.FromString(version);

			// Assert
			act.Should().NotThrow<ArgumentException>();
			result.Should().NotBeNull();
			result.Major.Should().Be(major);
			result.Minor.Should().Be(minor);
			result.Patch.Should().Be(patch);
			result.Revision.Should().Be(revision);
			result.Suffix.Should().Be(suffix);
			result.ToString().Should().Be(version);
		}
	}
}
