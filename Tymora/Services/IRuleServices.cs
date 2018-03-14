using Tymora.Models;

namespace Tymora.Services {
    public interface IRuleServices {
        string GetRulesList();
        string LoadRule(string rulename);
        void CreateRule(TymoraRules rule);
    }
}