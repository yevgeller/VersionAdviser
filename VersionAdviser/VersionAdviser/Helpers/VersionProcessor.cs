using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VersionAdviser.Helpers
{
    public class VersionProcessor
    {
        private string _userProvidedVersion;
        public string ExceptionMessage { get; private set; }
        public Version SoftwareVersionToCompareWith { get; private set; }

        public string UserProvidedVersion
        {
            get => _userProvidedVersion;
            set
            {
                bool flag;
                int result = 0;
                flag = int.TryParse(value.ToString(), out result);
                if (flag)
                {
                    SoftwareVersionToCompareWith = new Version(result, 0, 0, 0);
                }
                else
                {
                    try
                    {
                        SoftwareVersionToCompareWith = new Version(value.ToString());
                    }
                    catch(Exception ex)
                    {
                        ExceptionMessage = ex.Message;
                        SoftwareVersionToCompareWith = new Version(0, 0, 0, 0);
                    }
                }
            }
        }

        IEnumerable<Software> software;

        public VersionProcessor()
        {
            SoftwareVersionToCompareWith = new Version(1, 0, 0, 0);
            software = SoftwareManager.GetAllSoftware();
        }

        public List<Software> NewerVersions()
        {
            return software.Where(x => Version.Parse(x.Version)
                    .CompareTo(SoftwareVersionToCompareWith) > 0)
                    .OrderBy(x => Version.Parse(x.Version))
                    .ToList();
        }
    }

    public class Software
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public override string ToString() => $"{Name}, ver. \"{Version}\"";
    }

    public static class SoftwareManager
    {
        public static IEnumerable<Software> GetAllSoftware()
        {
            return new List<Software>
            {
                new Software
                {
                    Name = "MS Word",
                    Version = "13.2.1"
                },
                new Software
                {
                    Name = "AngularJS",
                    Version = "1.7.1"
                },
                new Software
                {
                    Name = "Angular",
                    Version = "8.1.13"
                },
                new Software
                {
                    Name = "React",
                    Version = "0.0.5"
                },
                new Software
                {
                    Name = "Vue.js",
                    Version = "2.6"
                },
                new Software
                {
                    Name = "Visual Studio",
                    Version = "2017.0.1"
                },
                new Software
                {
                    Name = "Visual Studio",
                    Version = "2019.1"
                },
                new Software
                {
                    Name = "Visual Studio Code",
                    Version = "1.35"
                },
                new Software
                {
                    Name = "Blazor",
                    Version = "0.7"
                }
            };
        }
    }
}
