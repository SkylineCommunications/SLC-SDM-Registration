namespace Shared
{
	using System;
	using System.Text;
	using System.Text.RegularExpressions;

	internal class SdmVersion : IComparable<SdmVersion>, IEquatable<SdmVersion>
	{
		private static readonly Regex VersionRegex = new Regex(
			@"^(?<major>\d+)\.(?<minor>\d+)\.(?<patch>\d+)(?:\.(?<revision>\d+))?(?:-(?<prerelease>[0-9A-Za-z.-]+))?$",
			RegexOptions.Compiled);

		public SdmVersion(uint major, uint minor, uint patch, uint? revision = null, string suffix = null)
		{
			Major = major;
			Minor = minor;
			Patch = patch;
			Revision = revision;
			Suffix = suffix;
		}

		public uint Major { get; }

		public uint Minor { get; }

		public uint Patch { get; }

		public uint? Revision { get; }

		public string Suffix { get; }

		public static bool operator ==(SdmVersion left, SdmVersion right)
		{
			if (ReferenceEquals(left, right))
			{
				return true;
			}

			if (left is null || right is null)
			{
				return false;
			}

			return left.Equals(right);
		}

		public static bool operator !=(SdmVersion left, SdmVersion right)
		{
			return !(left == right);
		}

		public static bool operator <(SdmVersion left, SdmVersion right)
		{
			if (left is null)
			{
				return right != null;
			}

			return left.CompareTo(right) < 0;
		}

		public static bool operator <=(SdmVersion left, SdmVersion right)
		{
			if (left is null)
			{
				return true;
			}

			return left.CompareTo(right) <= 0;
		}

		public static bool operator >(SdmVersion left, SdmVersion right)
		{
			if (left is null)
			{
				return false;
			}

			return left.CompareTo(right) > 0;
		}

		public static bool operator >=(SdmVersion left, SdmVersion right)
		{
			if (left is null)
			{
				return right is null;
			}

			return left.CompareTo(right) >= 0;
		}

		public static SdmVersion FromString(string input)
		{
			if (!TryParse(input, out var version))
			{
				throw new ArgumentException("Version string must be in the format 'Major.Minor.Patch[.Revision][-PreRelease]'");
			}

			return version;
		}

		public static bool TryParse(string input, out SdmVersion version)
		{
			version = null;
			if (String.IsNullOrWhiteSpace(input))
			{
				return false;
			}

			var match = VersionRegex.Match(input);
			if (!match.Success)
			{
				return false;
			}

			if (!UInt32.TryParse(match.Groups["major"].Value, out var major))
			{
				return false;
			}

			if (!UInt32.TryParse(match.Groups["minor"].Value, out var minor))
			{
				return false;
			}

			if (!UInt32.TryParse(match.Groups["patch"].Value, out var patch))
			{
				return false;
			}

			uint? revision = null;
			if (match.Groups["revision"].Success &&
				UInt32.TryParse(match.Groups["revision"].Value, out var rev))
			{
				revision = rev;
			}

			string suffix = match.Groups["prerelease"].Success ? match.Groups["prerelease"].Value : null;

			version = new SdmVersion(major, minor, patch, revision, suffix);
			return true;
		}

		public int CompareTo(SdmVersion other)
		{
			if (other == null)
			{
				return 1;
			}

			if (Major != other.Major)
			{
				return Major.CompareTo(other.Major);
			}

			if (Minor != other.Minor)
			{
				return Minor.CompareTo(other.Minor);
			}

			if (Patch != other.Patch)
			{
				return Patch.CompareTo(other.Patch);
			}

			// Handle optional Revision: missing revision is considered less than present
			if (Revision == null && other.Revision != null) return -1;
			if (Revision != null && other.Revision == null) return 1;
			if (Revision != null && other.Revision != null)
			{
				return Revision.Value.CompareTo(other.Revision.Value);
			}

			// Handle PreRelease: absence > presence
			if (Suffix == null && other.Suffix != null) return 1;
			if (Suffix != null && other.Suffix == null) return -1;
			if (Suffix != null && other.Suffix != null)
			{
				return String.Compare(Suffix, other.Suffix, StringComparison.Ordinal);
			}

			return 0;
		}

		public bool Equals(SdmVersion other)
		{
			if (other == null)
			{
				return false;
			}

			return Major == other.Major &&
				Minor == other.Minor &&
				Patch == other.Patch &&
				Revision == other.Revision &&
				String.Equals(Suffix, other.Suffix, StringComparison.Ordinal);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			return Equals(obj as SdmVersion);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = (hash * 23) + Major.GetHashCode();
				hash = (hash * 23) + Minor.GetHashCode();
				hash = (hash * 23) + Patch.GetHashCode();
				hash = (hash * 23) + (Revision?.GetHashCode() ?? 0);
				hash = (hash * 23) + (Suffix?.GetHashCode() ?? 0);
				return hash;
			}
		}

		public override string ToString()
		{
			var builder = new StringBuilder(Convert.ToString(Major));
			builder.Append('.').Append(Minor).Append('.').Append(Patch);
			if (Revision.HasValue)
			{
				builder.Append('.').Append(Revision.Value);
			}

			if (!String.IsNullOrEmpty(Suffix))
			{
				builder.Append('-').Append(Suffix);
			}

			return builder.ToString();
		}
	}
}
