using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VersionAdviser.Helpers;

namespace VersionAdviser.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty, Display(Name = "Enter version number:")]
        public string EnteredVersion { get; set; }

        [BindProperty]
        public Version ProcessedVersion { get; set; }

        [BindProperty]
        public string Result { get; set; }

        [BindProperty]
        public List<Software> NewerVersions { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private VersionProcessor versionProcessor;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            versionProcessor = new VersionProcessor();
            NewerVersions = new List<Software>();
        }

        public void OnGet() { }

        public void OnPost()
        {
            versionProcessor.UserProvidedVersion = EnteredVersion;
            if (!String.IsNullOrEmpty(versionProcessor.ExceptionMessage))
            {
                ModelState.AddModelError("EnteredVersion", "Could not process input. Please make sure it's in X.Y.Z format");
            }
            else
            {
                NewerVersions = versionProcessor.NewerVersions();
            }
        }
    }
}
