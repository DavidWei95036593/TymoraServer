using System;
using System.Collections.Generic;

namespace Tymora.Models{
    public partial class TymoraRules{
        public int Id { get; set; }
        public string Rule { get; set; }
        public string Creator { get; set; }
        public int? Father { get; set; }
        public string RuleContent { get; set; }
    }
}
