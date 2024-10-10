using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GitHubActivity
{
    internal class GitHubEvent
    {
        public string Type { get; set; }
        public GitHubRepo Repo { get; set; }
        public DateTime createdAt { get; set; }
        
        public GitHubPayload Payload { get; set;}
    }
}
