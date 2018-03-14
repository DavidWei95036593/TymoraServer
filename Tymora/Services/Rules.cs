using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Tymora.Models;

namespace Tymora.Services {
    class RulePair {
        public string rid;
        public string name;
        public RulePair(string rid, string name) {
            this.rid = rid;
            this.name = name;
        }

    }
    public class RuleServices :IRuleServices{
        private TymoraContext tymoraContext;
        private Dictionary<string, string> _ruleDic = new Dictionary<string, string>();
        private List<RulePair> _rulePairs   = new List<RulePair>();
        private int _totalRuleNumber = 0;
        public string GetRulesList() {
            return JsonConvert.SerializeObject(_rulePairs);
        }
        public string LoadRule(string ruleName) {
            var Rule = tymoraContext.TymoraRules.Single(b => b.Rule.Equals(ruleName));
            return Rule.RuleContent;
        }


        public void CreateRule(TymoraRules rule) {
            throw new NotImplementedException();
        }

        public RuleServices(TymoraContext dbContext) {
            tymoraContext = dbContext;
        }
    }
}
