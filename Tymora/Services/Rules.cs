using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class RuleServices : IRuleServices {
        private TymoraContext tymoraContext;
        private Dictionary<string, string> _ruleDic = new Dictionary<string, string>();
        private List<RulePair> _rulePairs = new List<RulePair>();
        private int _totalRuleNumber = 0;
        public string GetRulesList() {
            var res = tymoraContext.TymoraRules.ToList();
            _rulePairs.Clear();
            foreach (var pair in res) {
                _rulePairs.Add(new RulePair(pair.Id.ToString(), pair.Rule));
            }
            return JsonConvert.SerializeObject(_rulePairs);
        }
        public string PutRule(string rulename, string ruleContent) {
            TymoraRules tymoraRules = CreateRuleWithJson(rulename, ruleContent);
            int ruleCount = tymoraContext.TymoraRules.Count(b => b.Rule.Equals(rulename));
            if (ruleCount != 0) {
                return "Can't create another rule named:" + rulename;
            }
            CreateRule(tymoraRules);
            return String.Format("Create Rule  {0} Successfully", rulename);
        }
        public string LoadRule(string ruleName) {
            var Rule = tymoraContext.TymoraRules.Single(b => b.Rule.Equals(ruleName));
            return Rule.RuleContent;
        }
        public void CreateRule(TymoraRules rule) {
            tymoraContext.TymoraRules.Add(rule);
        }
        public TymoraRules CreateRuleWithJson(string rulename, string rule_content) {
            TymoraRules tymora = new TymoraRules();
            tymora.Rule = rulename;
            tymora.Rule = rule_content;
            return tymora;
        }
        public string UpdateRule(TymoraRules tr,string ruleContent) {
            Console.WriteLine($"getting rules data:.{ruleContent}");
            JObject j = JsonConvert.DeserializeObject(ruleContent) as JObject;
            Console.WriteLine(JsonConvert.SerializeObject(tr));

            JObject prej = JsonConvert.DeserializeObject(tr.RuleContent) as JObject;
            if (!prej["rulename"].Equals(j["rulename"])) {
                tr.Rule = j["rulename"].ToString();
            }
            tr.RuleContent = ruleContent;
            
            try {
                tymoraContext.TymoraRules.Update(tr);
               tymoraContext.SaveChangesAsync();

            }
            catch (Exception) {
                return String.Format("Update Rule  {0} failed", tr.Rule);
            }
            return String.Format("Update Rule  {0} Successfully", tr.Rule);

        }
        public string UpdateRule(string rulename, string ruleContent) {
            TymoraRules tr = tymoraContext.TymoraRules.Where(b => b.Rule.Equals(rulename)).First<TymoraRules>();
            if (tr == null) {
                return String.Format("Update Rule  {0} failed No Such Rule", rulename);
            }
            return UpdateRule(tr,ruleContent);
        }

        public string UpdateRuleById(int id, string ruleContent) {
            var tr = tymoraContext.TymoraRules.Find(id);
      
            if (tr == null) {
                return String.Format("Update Rule  {0} failed No Such Rule Id",id );
            }
            return UpdateRule(tr, ruleContent);

        }

        public RuleServices(TymoraContext dbContext) {
            tymoraContext = dbContext;
        }
    }
}
