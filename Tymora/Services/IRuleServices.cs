using Tymora.Models;

namespace Tymora.Services {
    public interface IRuleServices {
        string GetRulesList();
        string LoadRule(string rulename);
        string PutRule(string rulename, string rule_content);
        string UpdateRule(string rulename, string rule_content);
        string UpdateRuleById(int id,string rule_content);
    }
}