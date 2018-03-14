using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Tymora.Services;

namespace Tymora.Controllers {
    [Route("[controller]")]
    [EnableCors("TymoraCors")]
    public class RulesController : Controller {

        private IRuleServices _RuleServices;
        [HttpGet("get/rulelist")]
        public string GetRuleRulelist() {
            return _RuleServices.GetRulesList();
        }
        public RulesController(IRuleServices ruleServices) {
            _RuleServices = ruleServices;

        }
        [HttpGet("get/{rulename}")]
        public string  GetRule(string rulename) {
            rulename = rulename.ToUpper();
            var result = _RuleServices.LoadRule(rulename);
            return result;
        }
    }
}
