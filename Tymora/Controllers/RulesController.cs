using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Console.WriteLine($"give rule {result}");
            return result;
        }
        [HttpPost("put/{rulename}")]
        public string PutRule(string rulename) {
            rulename = rulename.ToUpper();
            string rule_content = Request.Form["rule"];
            return _RuleServices.PutRule(rulename, rule_content);

        }
        [HttpPost("update/{rulename}")]
        public string UpdateRule(string rulename) {
            var col = Request.Form;
            Console.WriteLine("测试中文");
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("测试中文");
            string rule_content = col["rule"];
            System.Web.HttpUtility.UrlEncode(col["rule"], System.Text.Encoding.GetEncoding("UTF-8"));

            Console.WriteLine(rule_content);
            return _RuleServices.UpdateRule(rulename, rule_content);
        }
    }
}
