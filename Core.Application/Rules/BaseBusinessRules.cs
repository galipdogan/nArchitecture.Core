using System.Globalization;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace NArchitecture.Core.Application.Rules;

public abstract class BaseBusinessRules
{
    protected async Task ThrowLocalizedExceptionAsync(ILocalizationService localizationService, string sectionName, string messageKey, params object[] args)
    {
        string? message = await localizationService.GetLocalizedAsync(messageKey, sectionName);
        if (string.IsNullOrEmpty(message))
            message = messageKey;
            
        if (args != null && args.Length > 0)
        {
            message = string.Format(CultureInfo.InvariantCulture, message, args);
        }
        
        throw new BusinessException(message);
    }

    protected async Task EntityShouldExistWhenSelected<TEntity>(TEntity? entity, ILocalizationService localizationService, string sectionName, string messageKey)
    {
        if (entity == null)
            await ThrowLocalizedExceptionAsync(localizationService, sectionName, messageKey);
    }
}
