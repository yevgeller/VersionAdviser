﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VersionAdviser.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string EnteredVersion { get; set; }

        [BindProperty]
        public Version ProcessedVersion { get; set; }

        [BindProperty]
        public string Result { get; set; }

        [BindProperty]
        public List<Software> NewerVersions { get; set; }

        private readonly ILogger<IndexModel> _logger;

        private IEnumerable<Software> software;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            software = SoftwareManager.GetAllSoftware();
            NewerVersions = new List<Software>();
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            try
            {
                ProcessedVersion = new Version(EnteredVersion);
                NewerVersions = software.Where(x => Version.Parse(x.Version)
                    .CompareTo(ProcessedVersion) > 0)
                    .OrderBy(x=>Version.Parse(x.Version))
                    .ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(EnteredVersion), ex.Message);
            }
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

    /*
     * 
     * # Hi

Thanks for applying to Emergent Software! We have a small code challenge for you to complete before your interview. We’ll review it and use it as a source of conversation about coding, so be prepared to talk about why you approached the problem the way you did and how you could have done it differently.

Our goal with giving you homework is to allow you to write code in a low stress environment on your own computer. We realize we’re asking you to do this in your personal time, but we prefer this format over a long session of whiteboarding code in an interview.

You should not spend more than 4 hours on this challenge. If you reach the 4-hour mark and are not finished, just turn it in as-is and let us know what you would have done with more time.

# The Challenge

An imaginary company stores a list of software products they use. They have stored the name and version of each product. They have asked us to create a simple website where users can type in a version number and receive a list of software products that are greater than the version they entered. If the user enters an invalid version, they should be notified that the version is not valid.

The software versions are stored as a string in [semver](semver.org) format. For this exercise, we're only concerned with major version, minor version, and patch (no pre-release or build) in the format [major].[minor].[patch]. The major, minor, and patch will always be non-negative integers. You may see versions like “2”, “1.5”, or “2.12.4”. The period is only used as a separator and does not represent a decimal point – 1.5 does not mean one and a half.

- "2" == "2.0" == "2.0.0"
- "2" < "2.0.1"
- "2" < "2.1"
- "2.0.1" < "2.1.0"

The imaginary company stores the software list as a C# object (provided below) that you can simply drop into your code – no need to call a database or REST service. This list of software is just a sample, assume the company will eventually switch to getting list from a database with thousands of softwares.

This site will be publicly available, so user authentication will not be required.

# Software Product List

    
     * 
     */


}
